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
using GameType = Yapped.GameMode.GameType;

namespace Yapped
{
    public partial class FormMain : Form
    {
        private const string UPDATE_URL = "https://www.nexusmods.com/sekiro/mods/121?tab=files";
        private static Properties.Settings settings = Properties.Settings.Default;

        private string regulationPath;
        private BND4 regulation;
        private bool encrypted;
        private Dictionary<string, PARAM64.Layout> layouts;
        private BindingSource rowSource;
        private Dictionary<string, (int Row, int Cell)> dgvIndices;
        private string lastFindRowPattern, lastFindFieldPattern;

        public FormMain()
        {
            InitializeComponent();
            regulation = null;
            layouts = new Dictionary<string, PARAM64.Layout>();
            rowSource = new BindingSource();
            dgvIndices = new Dictionary<string, (int Row, int Cell)>();
            dgvRows.DataSource = rowSource;
            dgvParams.AutoGenerateColumns = false;
            dgvRows.AutoGenerateColumns = false;
            dgvCells.AutoGenerateColumns = false;
            lastFindRowPattern = "";
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            Text = "Yapped " + Application.ProductVersion;

            Location = settings.WindowLocation;
            if (settings.WindowSize.Width >= MinimumSize.Width && settings.WindowSize.Height >= MinimumSize.Height)
                Size = settings.WindowSize;
            if (settings.WindowMaximized)
                WindowState = FormWindowState.Maximized;

            toolStripComboBoxGame.ComboBox.DisplayMember = "Name";
            toolStripComboBoxGame.Items.AddRange(GameMode.Modes);
            var game = (GameType)Enum.Parse(typeof(GameType), settings.GameType);
            toolStripComboBoxGame.SelectedIndex = Array.FindIndex(GameMode.Modes, m => m.Game == game);

            regulationPath = settings.RegulationPath;
            hideUnusedParamsToolStripMenuItem.Checked = settings.HideUnusedParams;
            verifyDeletionsToolStripMenuItem.Checked = settings.VerifyRowDeletion;
            splitContainer2.SplitterDistance = settings.SplitterDistance2;
            splitContainer1.SplitterDistance = settings.SplitterDistance1;

            LoadParams();

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

            settings.GameType = ((GameMode)toolStripComboBoxGame.SelectedItem).Game.ToString();
            settings.RegulationPath = regulationPath;
            settings.HideUnusedParams = hideUnusedParamsToolStripMenuItem.Checked;
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

        private void LoadParams()
        {
            string resDir = GetResRoot();
            Dictionary<string, PARAM64.Layout> layouts = Util.LoadLayouts($@"{resDir}\Layouts");
            Dictionary<string, ParamInfo> paramInfo = ParamInfo.ReadParamInfo($@"{resDir}\ParamInfo.xml");
            var gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
            LoadParamsResult result = Util.LoadParams(regulationPath, paramInfo, layouts, gameMode, hideUnusedParamsToolStripMenuItem.Checked);

            if (result == null)
            {
                exportToolStripMenuItem.Enabled = false;
            }
            else
            {
                encrypted = result.Encrypted;
                regulation = result.ParamBND;
                exportToolStripMenuItem.Enabled = encrypted;
                foreach (ParamWrapper wrapper in result.ParamWrappers)
                {
                    if (!dgvIndices.ContainsKey(wrapper.Name))
                        dgvIndices[wrapper.Name] = (0, 0);
                }
                dgvParams.DataSource = result.ParamWrappers;
                toolStripStatusLabel1.Text = regulationPath;
            }
        }

        private void dgvParams_SelectionChanged(object sender, EventArgs e)
        {
            if (rowSource.DataSource != null)
            {
                ParamWrapper paramFile = (ParamWrapper)rowSource.DataSource;
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
                ParamWrapper paramFile = (ParamWrapper)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
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
                ParamWrapper paramFile = (ParamWrapper)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
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
            CellType type = paramCell.Type;
            string value = e.FormattedValue.ToString();

            string error = Util.ValidateCell(type, value);
            if (error != null)
            {
                Util.ShowError(error);
                e.Cancel = true;
            }
        }

        private void dgvCells_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                PARAM64.Cell cell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
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
            ofdRegulation.FileName = regulationPath;
            if (ofdRegulation.ShowDialog() == DialogResult.OK)
            {
                regulationPath = ofdRegulation.FileName;
                LoadParams();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (BinderFile file in regulation.Files)
            {
                foreach (DataGridViewRow paramRow in dgvParams.Rows)
                {
                    ParamWrapper paramFile = (ParamWrapper)paramRow.DataBoundItem;
                    if (Path.GetFileNameWithoutExtension(file.Name) == paramFile.Name)
                        file.Bytes = paramFile.Param.Write();
                }
            }

            var gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
            if (!File.Exists(regulationPath + ".bak"))
                File.Copy(regulationPath, regulationPath + ".bak");

            if (encrypted)
            {
                if (gameMode.Game == GameType.DarkSouls2)
                    Util.EncryptDS2Regulation(regulationPath, regulation);
                else if (gameMode.Game == GameType.DarkSouls3)
                    SFUtil.EncryptDS3Regulation(regulationPath, regulation);
                else
                    Util.ShowError("Encryption is only valid for DS2 and DS3.");
            }
            else
            {
                regulation.Write(regulationPath);
            }
            SystemSounds.Asterisk.Play();
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
                        File.Copy(regulationPath + ".bak", regulationPath, true);
                        LoadParams();
                        SystemSounds.Asterisk.Play();
                    }
                    catch (Exception ex)
                    {
                        Util.ShowError($"Failed to restore backup\r\n\r\n{regulationPath}.bak\r\n\r\n{ex}");
                    }
                }
            }
            else
            {
                Util.ShowError($"There is no backup to restore at:\r\n\r\n{regulationPath}.bak");
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
            CreateRow("Add a new row...");
        }

        private void duplicateRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRows.SelectedCells.Count == 0)
            {
                Util.ShowError("You can't duplicate a row without one selected!");
                return;
            }

            int index = dgvRows.SelectedCells[0].RowIndex;
            ParamWrapper wrapper = (ParamWrapper)rowSource.DataSource;
            PARAM64.Row oldRow = wrapper.Rows[index];
            PARAM64.Row newRow;
            if ((newRow = CreateRow("Duplicate a row...")) != null)
            {
                for (int i = 0; i < oldRow.Cells.Count; i++)
                {
                    newRow.Cells[i].Value = oldRow.Cells[i].Value;
                }
            }
        }

        private PARAM64.Row CreateRow(string prompt)
        {
            if (rowSource.DataSource == null)
            {
                Util.ShowError("You can't create a row with no param selected!");
                return null;
            }

            PARAM64.Row result = null;
            var newRowForm = new FormNewRow(prompt);
            if (newRowForm.ShowDialog() == DialogResult.OK)
            {
                long id = newRowForm.ResultID;
                string name = newRowForm.ResultName;
                ParamWrapper paramWrapper = (ParamWrapper)rowSource.DataSource;
                if (paramWrapper.Rows.Any(row => row.ID == id))
                {
                    Util.ShowError($"A row with this ID already exists: {id}");
                }
                else
                {
                    result = new PARAM64.Row(id, name, paramWrapper.Layout);
                    rowSource.Add(result);
                    paramWrapper.Rows.Sort((r1, r2) => r1.ID.CompareTo(r2.ID));

                    int index = paramWrapper.Rows.FindIndex(row => ReferenceEquals(row, result));
                    int displayedRows = dgvRows.DisplayedRowCount(false);
                    dgvRows.FirstDisplayedScrollingRowIndex = Math.Max(0, index - displayedRows / 2);
                    dgvRows.ClearSelection();
                    dgvRows.Rows[index].Selected = true;
                    dgvRows.Refresh();
                }
            }
            return result;
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
            bool replace = MessageBox.Show("If a row already has a name, would you like to skip it?\r\n" +
                "Click Yes to skip existing names.\r\nClick No to replace existing names.",
                "Importing Names", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

            string namesDir = $@"{GetResRoot()}\Names";
            List<ParamWrapper> paramFiles = (List<ParamWrapper>)dgvParams.DataSource;
            foreach (ParamWrapper paramFile in paramFiles)
            {
                if (File.Exists($@"{namesDir}\{paramFile.Name}.txt"))
                {
                    var names = new Dictionary<long, string>();
                    string nameStr = File.ReadAllText($@"{namesDir}\{paramFile.Name}.txt");
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
                        {
                            if (replace || row.Name == null || row.Name == "")
                                row.Name = names[row.ID];
                        }
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
                    Util.ShowError("Row ID must be a positive integer.\r\nEnter a valid number or press Esc to cancel.");
                    e.Cancel = true;
                }
            }
        }

        private void exportNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string namesDir = $@"{GetResRoot()}\Names";
            List<ParamWrapper> paramFiles = (List<ParamWrapper>)dgvParams.DataSource;
            foreach (ParamWrapper paramFile in paramFiles)
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
                    File.WriteAllText($@"{namesDir}\{paramFile.Name}.txt", sb.ToString());
                }
                catch (Exception ex)
                {
                    Util.ShowError($"Failed to write name file: {paramFile.Name}.txt\r\n\r\n{ex}");
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

        private void findRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var findForm = new FormFind("Find row with name...");
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                FindRow(findForm.ResultPattern);
            }
        }

        private void findNextRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindRow(lastFindRowPattern);
        }

        private void FindRow(string pattern)
        {
            if (rowSource.DataSource == null)
            {
                Util.ShowError("You can't search for a row when there are no rows!");
                return;
            }

            int startIndex = dgvRows.SelectedCells.Count > 0 ? dgvRows.SelectedCells[0].RowIndex + 1 : 0;
            List<PARAM64.Row> rows = ((ParamWrapper)rowSource.DataSource).Rows;
            int index = -1;

            for (int i = 0; i < rows.Count; i++)
            {
                if ((rows[(startIndex + i) % rows.Count].Name ?? "").ToLower().Contains(pattern.ToLower()))
                {
                    index = (startIndex + i) % rows.Count;
                    break;
                }
            }

            if (index != -1)
            {
                int displayedRows = dgvRows.DisplayedRowCount(false);
                dgvRows.FirstDisplayedScrollingRowIndex = Math.Max(0, index - displayedRows / 2);
                dgvRows.ClearSelection();
                dgvRows.Rows[index].Selected = true;
                lastFindRowPattern = pattern;
            }
            else
            {
                Util.ShowError($"No row found matching: {pattern}");
            }
        }

        private void gotoRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gotoForm = new FormGoto();
            if (gotoForm.ShowDialog() == DialogResult.OK)
            {
                if (rowSource.DataSource == null)
                {
                    Util.ShowError("You can't goto a row when there are no rows!");
                    return;
                }

                long id = gotoForm.ResultID;
                List<PARAM64.Row> rows = ((ParamWrapper)rowSource.DataSource).Rows;
                int index = rows.FindIndex(row => row.ID == id);

                if (index != -1)
                {
                    int displayedRows = dgvRows.DisplayedRowCount(false);
                    dgvRows.FirstDisplayedScrollingRowIndex = Math.Max(0, index - displayedRows / 2);
                    dgvRows.ClearSelection();
                    dgvRows.Rows[index].Selected = true;
                }
                else
                {
                    Util.ShowError($"Row ID not found: {id}");
                }
            }
        }

        private void findFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var findForm = new FormFind("Find field with name...");
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                FindField(findForm.ResultPattern);
            }
        }

        private void findNextFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindField(lastFindFieldPattern);
        }

        private void dgvParams_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var paramWrapper = (ParamWrapper)dgvParams.Rows[e.RowIndex].DataBoundItem;
                e.ToolTipText = paramWrapper.Description;
            }
        }

        private void dgvCells_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                var cell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
                e.ToolTipText = cell.Description;
            }
        }

        private void FindField(string pattern)
        {
            if (dgvCells.DataSource == null)
            {
                Util.ShowError("You can't search for a field when there are no fields!");
                return;
            }

            int startIndex = dgvCells.SelectedCells.Count > 0 ? dgvCells.SelectedCells[0].RowIndex + 1 : 0;
            var cells = (PARAM64.Cell[])dgvCells.DataSource;
            int index = -1;

            for (int i = 0; i < cells.Length; i++)
            {
                if ((cells[(startIndex + i) % cells.Length].Name ?? "").ToLower().Contains(pattern.ToLower()))
                {
                    index = (startIndex + i) % cells.Length;
                    break;
                }
            }

            if (index != -1)
            {
                int displayedRows = dgvCells.DisplayedRowCount(false);
                dgvCells.FirstDisplayedScrollingRowIndex = Math.Max(0, index - displayedRows / 2);
                dgvCells.ClearSelection();
                dgvCells.Rows[index].Selected = true;
                lastFindFieldPattern = pattern;
            }
            else
            {
                Util.ShowError($"No field found matching: {pattern}");
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fbdExport.SelectedPath = Path.GetDirectoryName(regulationPath);
            if (fbdExport.ShowDialog() == DialogResult.OK)
            {
                BND4 paramBND = new BND4
                {
                    BigEndian = false,
                    Compression = DCX.Type.DarkSouls3,
                    Extended = 0x04,
                    Flag1 = false,
                    Flag2 = false,
                    Format = Binder.Format.x74,
                    Timestamp = regulation.Timestamp,
                    Unicode = true,
                    Files = regulation.Files.Where(f => f.Name.EndsWith(".param")).ToList()
                };

                BND4 stayBND = new BND4
                {
                    BigEndian = false,
                    Compression = DCX.Type.DarkSouls3,
                    Extended = 0x04,
                    Flag1 = false,
                    Flag2 = false,
                    Format = Binder.Format.x74,
                    Timestamp = regulation.Timestamp,
                    Unicode = true,
                    Files = regulation.Files.Where(f => f.Name.EndsWith(".stayparam")).ToList()
                };

                string dir = fbdExport.SelectedPath;
                try
                {
                    paramBND.Write($@"{dir}\gameparam_dlc2.parambnd.dcx");
                    stayBND.Write($@"{dir}\stayparam.parambnd.dcx");
                }
                catch (Exception ex)
                {
                    Util.ShowError($"Failed to write exported parambnds.\r\n\r\n{ex}");
                }
            }
        }

        private string GetResRoot()
        {
            var gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
#if DEBUG
            return $@"..\..\..\..\dist\res\{gameMode.Directory}";
#else
            return $@"res\{gameMode.Directory}";
#endif
        }
    }
}
