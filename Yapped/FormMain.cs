using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Yapped
{
    public partial class FormMain : Form
    {
        private const string UPDATE_URL = "";
        private static Properties.Settings settings = Properties.Settings.Default;

        private string regulationPath;
        private Dictionary<string, PARAM64.Layout> layouts;

        public FormMain()
        {
            InitializeComponent();

            layouts = new Dictionary<string, PARAM64.Layout>();
            dgvParams.AutoGenerateColumns = false;
            dgvRows.AutoGenerateColumns = false;
            dgvCells.AutoGenerateColumns = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = "Yapped " + Application.ProductVersion;
            Location = settings.WindowLocation;
            if (settings.WindowSize.Width >= MinimumSize.Width && settings.WindowSize.Height >= MinimumSize.Height)
                Size = settings.WindowSize;
            if (settings.WindowMaximized)
                WindowState = FormWindowState.Maximized;

            regulationPath = settings.RegulationPath;

            if (LoadLayouts())
            {
                LoadRegulation();

                if (settings.SelectedParam >= dgvParams.Rows.Count)
                    settings.SelectedParam = 0;

                if (dgvParams.Rows.Count > 0)
                {
                    dgvParams.ClearSelection();
                    dgvParams.Rows[settings.SelectedParam].Selected = true;
                    dgvParams.CurrentCell = dgvParams.SelectedCells[0];
                }

                if (settings.SelectedRow >= dgvRows.Rows.Count)
                    settings.SelectedRow = 0;

                if (dgvRows.Rows.Count > 0)
                {
                    dgvRows.ClearSelection();
                    dgvRows.Rows[settings.SelectedRow].Selected = true;
                    dgvRows.CurrentCell = dgvRows.SelectedCells[0];
                }
            }
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
            if (dgvParams.SelectedCells.Count > 0)
                settings.SelectedParam = dgvParams.SelectedCells[0].RowIndex;
            if (dgvRows.SelectedCells.Count > 0)
                settings.SelectedRow = dgvRows.SelectedCells[0].RowIndex;
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
            BND4 bnd;
            try
            {
                bnd = Core.ReadRegulation(regulationPath);
            }
            catch (Exception ex)
            {
                ShowError($"Failed to load regulation file:\r\n\r\n{regulationPath}\r\n\r\n{ex}");
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
                                paramFiles.Add(new ParamFile(name, param, layout));
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

        public static void ShowError(string message)
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
            if (dgvParams.SelectedCells.Count > 0)
            {
                ParamFile paramFile = (ParamFile)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
                if (dgvCells.FirstDisplayedCell != null && dgvCells.Rows.Count > 0)
                    dgvCells.FirstDisplayedScrollingRowIndex = 0;

                dgvRows.DataSource = paramFile;
                dgvRows.DataMember = "Rows";
            }
        }

        private void dgvRows_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRows.SelectedCells.Count > 0)
            {
                int cellDisplayIndex = 0;
                if (dgvCells.FirstDisplayedCell != null)
                    cellDisplayIndex = dgvCells.FirstDisplayedScrollingRowIndex;

                PARAM64.Row row = (PARAM64.Row)dgvRows.SelectedCells[0].OwningRow.DataBoundItem;
                List<PARAM64.Cell> cells = new List<PARAM64.Cell>();
                foreach (PARAM64.Cell cell in row.Cells)
                {
                    if (!cell.Type.StartsWith("dummy8"))
                        cells.Add(cell);
                }
                dgvCells.DataSource = cells;

                if (dgvCells.Rows.Count > 0)
                    dgvCells.FirstDisplayedScrollingRowIndex = cellDisplayIndex;
            }
        }

        private void dgvCells_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            PARAM64.Cell paramCell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
            DataGridViewCell dgvCell = dgvCells.Rows[e.RowIndex].Cells[e.ColumnIndex];
            paramCell.Value = PARAM64.Layout.ParseParamValue(paramCell.Type, dgvCell.Value.ToString());
        }

        private void dgvCells_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 2)
                return;

            PARAM64.Cell paramCell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
            DataGridViewCell dgvCell = dgvCells.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string type = paramCell.Type;
            string value = e.FormattedValue.ToString();

            if (type == "s8")
            {
                if (!sbyte.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for signed byte.");
                }
            }
            else if (type == "u8")
            {
                if (!byte.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for unsigned byte.");
                }
            }
            else if (type == "x8")
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
            else if (type == "s16")
            {
                if (!short.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for signed short.");
                }
            }
            else if (type == "u16")
            {
                if (!ushort.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for unsigned short.");
                }
            }
            else if (type == "x16")
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
            else if (type == "s32")
            {
                if (!int.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for signed int.");
                }
            }
            else if (type == "u32")
            {
                if (!uint.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for unsigned int.");
                }
            }
            else if (type == "x32")
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
            else if (type == "f32")
            {
                if (!float.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for float.");
                }
            }
            else if (type == "b8" || type == "b32")
            {
                if (!bool.TryParse(value, out _))
                {
                    e.Cancel = true;
                    ShowError("Invalid value for bool.");
                }
            }
            else if (type.StartsWith("fixstr"))
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
                if (cell.Type == "x8")
                    e.Value = $"0x{e.Value:X2}";
                else if (cell.Type == "x16")
                    e.Value = $"0x{e.Value:X4}";
                else if (cell.Type == "x32")
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
            BND4 bnd = Core.ReadRegulation(regulationPath, out bool encrypted);
            foreach (BND4.File file in bnd.Files)
            {
                foreach (DataGridViewRow paramRow in dgvParams.Rows)
                {
                    ParamFile paramFile = (ParamFile)paramRow.DataBoundItem;
                    if (Path.GetFileNameWithoutExtension(file.Name) == paramFile.Name)
                        file.Bytes = paramFile.Param.Write();
                }
            }
            Core.WriteRegulation(regulationPath, encrypted, bnd);
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

        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParamFile paramFile = (ParamFile)dgvRows.DataSource;
            PARAM64.Row row = new PARAM64.Row(0, "", paramFile.Layout);
            paramFile.Param.Rows.Add(row);

            // Force refresh
            dgvRows.DataSource = null;
            dgvRows.DataSource = paramFile;
            dgvRows.DataMember = "Rows";
            dgvRows.ClearSelection();
            dgvRows.Rows[dgvRows.Rows.Count - 1].Selected = true;
            dgvRows.CurrentCell = dgvRows.SelectedCells[0];
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult choice = MessageBox.Show("Are you sure you want to delete this row?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (choice == DialogResult.Yes)
            {
                ParamFile paramFile = (ParamFile)dgvRows.DataSource;
                int rowIndex = dgvRows.SelectedCells[0].RowIndex;
                if (rowIndex == 0)
                    rowIndex = 1;
                PARAM64.Row row = (PARAM64.Row)dgvRows.SelectedCells[0].OwningRow.DataBoundItem;
                paramFile.Param.Rows.Remove(row);

                // Force refresh7
                dgvRows.DataSource = null;
                dgvRows.DataSource = paramFile;
                dgvRows.DataMember = "Rows";

                if (dgvRows.Rows.Count > 0)
                {
                    dgvRows.ClearSelection();
                    dgvRows.Rows[rowIndex - 1].Selected = true;
                    dgvRows.CurrentCell = dgvRows.SelectedCells[0];
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

            // Force refresh rows
            if (dgvRows.DataSource != null)
            {
                ParamFile paramFile = (ParamFile)dgvRows.DataSource;
                dgvRows.DataSource = null;
                dgvRows.DataSource = paramFile;
                dgvRows.DataMember = "Rows";
            }
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
    }
}
