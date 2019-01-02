using Semver;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CellType = SoulsFormats.PARAM64.CellType;

namespace Yapped
{
    public partial class FormMain : Form
    {
        private const string UPDATE_URL = "https://www.nexusmods.com/darksouls3/mods/306?tab=files";
        private static Properties.Settings settings = Properties.Settings.Default;

        private string regulationPath;
        private Dictionary<string, PARAM64.Layout> layouts;
        private BindingSource rowSource;
        private Dictionary<string, (int Row, int Cell)> dgvIndices;

        public FormMain()
        {
            InitializeComponent();
            layouts = new Dictionary<string, PARAM64.Layout>();
            rowSource = new BindingSource();
            dgvIndices = new Dictionary<string, (int Row, int Cell)>();
            dgvRows.DataSource = rowSource;
            dgvParams.AutoGenerateColumns = false;
            dgvRows.AutoGenerateColumns = false;
            dgvCells.AutoGenerateColumns = false;
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            Text = "Yapped " + Application.ProductVersion;
            Location = settings.WindowLocation;
            if (settings.WindowSize.Width >= MinimumSize.Width && settings.WindowSize.Height >= MinimumSize.Height)
                Size = settings.WindowSize;
            if (settings.WindowMaximized)
                WindowState = FormWindowState.Maximized;

            regulationPath = settings.RegulationPath;
            verifyDeletionsToolStripMenuItem.Checked = settings.VerifyRowDeletion;
            splitContainer2.SplitterDistance = settings.SplitterDistance2;
            splitContainer1.SplitterDistance = settings.SplitterDistance1;

            if (LoadLayouts())
            {
                LoadRegulation();

                foreach (Match match in Regex.Matches(settings.DGVIndices, @"[^,]+"))
                {
                    string[] components = match.Value.Split(':');
                    dgvIndices[components[0]] = (int.Parse(components[1]), int.Parse(components[2]));
                }

                if (settings.SelectedParam >= dgvParams.Rows.Count)
                    settings.SelectedParam = 0;

                if (dgvParams.Rows.Count > 0)
                {
                    dgvParams.ClearSelection();
                    dgvParams.Rows[settings.SelectedParam].Selected = true;
                    dgvParams.CurrentCell = dgvParams.SelectedCells[0];
                }
            }

            Octokit.GitHubClient gitHubClient = new Octokit.GitHubClient(new Octokit.ProductHeaderValue("Yapped"));
            try
            {
                Octokit.Release release = await gitHubClient.Repository.Release.GetLatest("JKAnderson", "Yapped");
                if (SemVersion.Parse(release.TagName) > Application.ProductVersion)
                {
                    updateToolStripMenuItem.Visible = true;
                }
            }
            // Oh well.
            catch { }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.WindowMaximized = WindowState == FormWindowState.Maximized;
            if (WindowState == FormWindowState.Normal)
            {
                settings.WindowLocation = Location;
                settings.WindowSize = Size;
            }
            else
            {
                settings.WindowLocation = RestoreBounds.Location;
                settings.WindowSize = RestoreBounds.Size;
            }

            settings.RegulationPath = regulationPath;
            settings.VerifyRowDeletion = verifyDeletionsToolStripMenuItem.Checked;
            settings.SplitterDistance2 = splitContainer2.SplitterDistance;
            settings.SplitterDistance1 = splitContainer1.SplitterDistance;

            if (dgvParams.SelectedCells.Count > 0)
                settings.SelectedParam = dgvParams.SelectedCells[0].RowIndex;

            // Force saving the dgv indices
            dgvParams.ClearSelection();

            var components = new List<string>();
            foreach (string key in dgvIndices.Keys)
                components.Add($"{key}:{dgvIndices[key].Row}:{dgvIndices[key].Cell}");
            settings.DGVIndices = string.Join(",", components);
        }

        private bool LoadLayouts()
        {
            if (!Directory.Exists("Layouts"))
            {
                ShowError("No Layouts directory found with application; params cannot be edited.");
                Close();
                return false;
            }
            else
            {
                foreach (string path in Directory.GetFiles("Layouts", "*.xml"))
                {
                    string paramID = Path.GetFileNameWithoutExtension(path);
                    try
                    {
                        PARAM64.Layout layout = PARAM64.Layout.ReadXMLFile(path);
                        layouts[paramID] = layout;
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Failed to load layout {paramID}.txt\r\n\r\n{ex}");
                    }
                }
                return true;
            }
        }

        private void LoadRegulation()
        {
            if (!File.Exists(regulationPath))
            {
                ShowError($"Regulation file not found:\r\n{regulationPath}\r\n\r\nPlease browse to Data0.bdt in your game directory from the File->Open menu.");
                return;
            }

            BND4 bnd;
            try
            {
                bnd = SFUtil.DecryptDS3Regulation(regulationPath);
            }
            catch (Exception ex)
            {
                ShowError($"Failed to load regulation file:\r\n{regulationPath}\r\n\r\n{ex}");
                return;
            }

            List<ParamFile> paramFiles = new List<ParamFile>();
            foreach (BND4.File file in bnd.Files)
            {
                if (Path.GetExtension(file.Name) == ".param")
                {
                    string name = Path.GetFileNameWithoutExtension(file.Name);
                    try
                    {
                        PARAM64 param = PARAM64.Read(file.Bytes);
                        if (layouts.ContainsKey(param.ID))
                        {
                            PARAM64.Layout layout = layouts[param.ID];
                            if (layout.Size == param.DetectedSize)
                            {
                                var paramFile = new ParamFile(name, param, layout);
                                paramFiles.Add(paramFile);
                                if (!dgvIndices.ContainsKey(paramFile.Name))
                                    dgvIndices[paramFile.Name] = (0, 0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Failed to load param file: {name}.param\r\n\r\n{ex}");
                    }
                }
            }

            paramFiles.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
            dgvParams.DataSource = paramFiles;
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public class ParamFile
        {
            public string Name { get; set; }
            public PARAM64 Param;
            public PARAM64.Layout Layout;
            public List<PARAM64.Row> Rows { get; set; }

            public ParamFile(string name, PARAM64 param, PARAM64.Layout layout)
            {
                Name = name;
                Param = param;
                Layout = layout;
                Param.SetLayout(layout);
                Rows = Param.Rows;
            }
        }

        private void dgvParams_SelectionChanged(object sender, EventArgs e)
        {
            if (rowSource.DataSource != null)
            {
                ParamFile paramFile = (ParamFile)rowSource.DataSource;
                (int Row, int Cell) indices = (0, 0);

                if (dgvRows.SelectedCells.Count > 0)
                    indices.Row = dgvRows.SelectedCells[0].RowIndex;
                else if (dgvRows.FirstDisplayedScrollingRowIndex >= 0)
                    indices.Row = dgvRows.FirstDisplayedScrollingRowIndex;

                if (dgvCells.FirstDisplayedScrollingRowIndex >= 0)
                    indices.Cell = dgvCells.FirstDisplayedScrollingRowIndex;

                dgvIndices[paramFile.Name] = indices;
            }

            rowSource.DataSource = null;
            dgvCells.DataSource = null;
            if (dgvParams.SelectedCells.Count > 0)
            {
                ParamFile paramFile = (ParamFile)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
                // Yes, I need to set this again every time because it gets cleared out when you null the DataSource for some stupid reason
                rowSource.DataMember = "Rows";
                rowSource.DataSource = paramFile;
                (int Row, int Cell) indices = dgvIndices[paramFile.Name];

                if (indices.Row >= dgvRows.RowCount)
                    indices.Row = dgvRows.RowCount - 1;

                if (indices.Row < 0)
                    indices.Row = 0;

                dgvIndices[paramFile.Name] = indices;
                dgvRows.ClearSelection();
                if (dgvRows.RowCount > 0)
                {
                    dgvRows.FirstDisplayedScrollingRowIndex = indices.Row;
                    dgvRows.Rows[indices.Row].Selected = true;
                }
            }
        }

        private void dgvRows_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRows.SelectedCells.Count > 0)
            {
                ParamFile paramFile = (ParamFile)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
                (int Row, int Cell) indices = dgvIndices[paramFile.Name];
                if (dgvCells.FirstDisplayedScrollingRowIndex >= 0)
                    indices.Cell = dgvCells.FirstDisplayedScrollingRowIndex;

                PARAM64.Row row = (PARAM64.Row)dgvRows.SelectedCells[0].OwningRow.DataBoundItem;
                dgvCells.DataSource = row.Cells.Where(cell => cell.Type != CellType.dummy8).ToArray();

                if (indices.Cell >= dgvCells.RowCount)
                    indices.Cell = dgvCells.RowCount - 1;

                if (indices.Cell < 0)
                    indices.Cell = 0;

                dgvIndices[paramFile.Name] = indices;
                if (dgvCells.RowCount > 0)
                    dgvCells.FirstDisplayedScrollingRowIndex = indices.Cell;
            }
        }

        private void dgvCells_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            PARAM64.Cell paramCell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
            DataGridViewCell dgvCell = dgvCells.Rows[e.RowIndex].Cells[e.ColumnIndex];
            paramCell.Value = PARAM64.Layout.ParseParamValue(paramCell.Type, dgvCell.Value.ToString(), CultureInfo.CurrentCulture);
        }

        private void dgvCells_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 2)
                return;

            PARAM64.Cell paramCell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
            DataGridViewCell dgvCell = dgvCells.Rows[e.RowIndex].Cells[e.ColumnIndex];
            CellType type = paramCell.Type;
            string value = e.FormattedValue.ToString();

            if (type == CellType.s8)
            {
                if (!sbyte.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for signed byte.");
                }
            }
            else if (type == CellType.u8)
            {
                if (!byte.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for unsigned byte.");
                }
            }
            else if (type == CellType.x8)
            {
                try
                {
                    Convert.ToByte(value, 16);
                }
                catch
                {
                    e.Cancel = true;
                    ShowError("Invalid value for hex byte.");
                }
            }
            else if (type == CellType.s16)
            {
                if (!short.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for signed short.");
                }
            }
            else if (type == CellType.u16)
            {
                if (!ushort.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for unsigned short.");
                }
            }
            else if (type == CellType.x16)
            {
                try
                {
                    Convert.ToUInt16(value, 16);
                }
                catch
                {
                    e.Cancel = true;
                    ShowError("Invalid value for hex short.");
                }
            }
            else if (type == CellType.s32)
            {
                if (!int.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for signed int.");
                }
            }
            else if (type == CellType.u32)
            {
                if (!uint.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for unsigned int.");
                }
            }
            else if (type == CellType.x32)
            {
                try
                {
                    Convert.ToUInt32(value, 16);
                }
                catch
                {
                    e.Cancel = true;
                    ShowError("Invalid value for hex int.");
                }
            }
            else if (type == CellType.f32)
            {
                if (!float.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for float.");
                }
            }
            else if (type == CellType.b8 || type == CellType.b32)
            {
                if (!bool.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for bool.");
                }
            }
            else if (type == CellType.fixstr || type == CellType.fixstrW)
            {
                // Don't see how you could mess this up
            }
            else
                throw new NotImplementedException("Cannot validate cell type.");
        }

        private void dgvCells_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            PARAM64.Cell cell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
            if (e.ColumnIndex == 2)
            {
                if (cell.Type == CellType.x8)
                    e.Value = $"0x{e.Value:X2}";
                else if (cell.Type == CellType.x16)
                    e.Value = $"0x{e.Value:X4}";
                else if (cell.Type == CellType.x32)
                    e.Value = $"0x{e.Value:X8}";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ofdRegulation.InitialDirectory = Path.GetDirectoryName(regulationPath);
            }
            catch
            {
                ofdRegulation.InitialDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\DARK SOULS III\Game";
            }

            if (ofdRegulation.ShowDialog() == DialogResult.OK)
            {
                regulationPath = ofdRegulation.FileName;
                LoadRegulation();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BND4 bnd = SFUtil.DecryptDS3Regulation(regulationPath);
            foreach (BND4.File file in bnd.Files)
            {
                foreach (DataGridViewRow paramRow in dgvParams.Rows)
                {
                    ParamFile paramFile = (ParamFile)paramRow.DataBoundItem;
                    if (Path.GetFileNameWithoutExtension(file.Name) == paramFile.Name)
                        file.Bytes = paramFile.Param.Write();
                }
            }

            if (!File.Exists(regulationPath + ".bak"))
                File.Copy(regulationPath, regulationPath + ".bak");
            SFUtil.EncryptDS3Regulation(regulationPath, bnd);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(regulationPath + ".bak"))
            {
                DialogResult choice = MessageBox.Show("Are you sure you want to restore the backup?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (choice == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(regulationPath);
                        File.Move(regulationPath + ".bak", regulationPath);
                        LoadRegulation();
                        SystemSounds.Asterisk.Play();
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Failed to restore backup\r\n\r\n{regulationPath}.bak\r\n\r\n{ex}");
                    }
                }
            }
            else
            {
                ShowError($"There is no backup to restore at:\r\n\r\n{regulationPath}.bak");
            }
        }

        private void exploreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Path.GetDirectoryName(regulationPath));
            }
            catch
            {
                SystemSounds.Hand.Play();
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(UPDATE_URL);
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParamFile paramFile = (ParamFile)rowSource.DataSource;
            PARAM64.Row row = new PARAM64.Row(paramFile.Rows.Max(r => r.ID) + 1, "", paramFile.Layout);
            rowSource.Add(row);
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRows.SelectedCells.Count > 0)
            {
                DialogResult choice = DialogResult.Yes;
                if (verifyDeletionsToolStripMenuItem.Checked)
                    choice = MessageBox.Show("Are you sure you want to delete this row?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (choice == DialogResult.Yes)
                {
                    int rowIndex = dgvRows.SelectedCells[0].RowIndex;
                    rowSource.RemoveAt(rowIndex);

                    // If you remove a row it automatically selects the next one, but if you remove the last row
                    // it doesn't automatically select the previous one
                    if (rowIndex == dgvRows.RowCount)
                    {
                        if (dgvRows.RowCount > 0)
                            dgvRows.Rows[dgvRows.RowCount - 1].Selected = true;
                        else
                            dgvCells.DataSource = null;
                    }
                }
            }
        }

        private void importNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ParamFile> paramFiles = (List<ParamFile>)dgvParams.DataSource;
            foreach (ParamFile paramFile in paramFiles)
            {
                if (File.Exists($"Names\\{paramFile.Name}.txt"))
                {
                    var names = new Dictionary<long, string>();
                    string nameStr = File.ReadAllText($"Names\\{paramFile.Name}.txt");
                    foreach (string line in Regex.Split(nameStr, @"\s*[\r\n]+\s*"))
                    {
                        if (line.Length > 0)
                        {
                            Match match = Regex.Match(line, @"^(\d+) (.+)$");
                            long id = long.Parse(match.Groups[1].Value);
                            string name = match.Groups[2].Value;
                            names[id] = name;
                        }
                    }

                    foreach (PARAM64.Row row in paramFile.Param.Rows)
                    {
                        if (names.ContainsKey(row.ID))
                            row.Name = names[row.ID];
                    }
                }
            }

            dgvRows.Refresh();
        }

        private void dgvRows_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // ID
            if (e.ColumnIndex == 0)
            {
                bool parsed = int.TryParse((string)e.FormattedValue, out int value);
                if (!parsed || value < 0)
                {
                    ShowError("Row ID must be a positive integer.\r\nEnter a valid number or press Esc to cancel.");
                    e.Cancel = true;
                }
            }
        }

        private void exportNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ParamFile> paramFiles = (List<ParamFile>)dgvParams.DataSource;
            foreach (ParamFile paramFile in paramFiles)
            {
                StringBuilder sb = new StringBuilder();
                foreach (PARAM64.Row row in paramFile.Param.Rows)
                {
                    string name = (row.Name ?? "").Trim();
                    if (name != "")
                    {
                        sb.AppendLine($"{row.ID} {name}");
                    }
                }

                try
                {
                    File.WriteAllText($"Names\\{paramFile.Name}.txt", sb.ToString());
                }
                catch (Exception ex)
                {
                    ShowError($"Failed to write name file: {paramFile.Name}.txt\r\n\r\n{ex}");
                    break;
                }
            }
        }

        private void dgvCells_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                DataGridViewRow row = dgvCells.Rows[e.RowIndex + i];
                PARAM64.Cell paramCell = (PARAM64.Cell)row.DataBoundItem;
                if (paramCell.Type == CellType.b8 || paramCell.Type == CellType.b32)
                {
                    // Value
                    row.Cells[2] = new DataGridViewCheckBoxCell();
                }
            }
        }
    }
}
