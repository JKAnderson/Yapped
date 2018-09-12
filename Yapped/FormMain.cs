using SoulsFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yapped
{
    public partial class FormMain : Form
    {
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
                dgvParams.ClearSelection();
                dgvParams.Rows[settings.SelectedParam].Selected = true;
                dgvRows.ClearSelection();
                dgvRows.Rows[settings.SelectedRow].Selected = true;
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
            settings.SelectedParam = dgvParams.SelectedCells[0].RowIndex;
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
                foreach (string path in Directory.GetFiles("Layouts", "*.txt"))
                {
                    string paramID = Path.GetFileNameWithoutExtension(path);
                    try
                    {
                        PARAM64.Layout layout = new PARAM64.Layout(File.ReadAllText(path));
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

        private void btnBrowse_Click(object sender, EventArgs e)
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

        private void dgvParams_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvParams.SelectedCells.Count > 0)
            {
                ParamFile paramFile = (ParamFile)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
                dgvRows.DataSource = paramFile;
                dgvRows.DataMember = "Rows";
            }
        }

        private void dgvRows_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRows.SelectedCells.Count > 0)
            {
                PARAM64.Row row = (PARAM64.Row)dgvRows.SelectedCells[0].OwningRow.DataBoundItem;
                List<PARAM64.Cell> cells = new List<PARAM64.Cell>();
                foreach (PARAM64.Cell cell in row.Cells)
                {
                    if (!cell.Type.StartsWith("dummy8"))
                        cells.Add(cell);
                }
                dgvCells.DataSource = cells;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
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

        private void dgvCells_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            PARAM64.Cell paramCell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
            DataGridViewCell dgvCell = dgvCells.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string type = paramCell.Type;
            string value = dgvCell.Value.ToString();

            if (type == "s8")
                paramCell.Value = sbyte.Parse(value);
            else if (type == "u8")
                paramCell.Value = byte.Parse(value);
            else if (type == "x8")
                paramCell.Value = Convert.ToByte(value, 16);
            else if (type == "s16")
                paramCell.Value = short.Parse(value);
            else if (type == "u16")
                paramCell.Value = ushort.Parse(value);
            else if (type == "x16")
                paramCell.Value = Convert.ToUInt16(value, 16);
            else if (type == "s32")
                paramCell.Value = int.Parse(value);
            else if (type == "u32")
                paramCell.Value = uint.Parse(value);
            else if (type == "x32")
                paramCell.Value = Convert.ToUInt32(value, 16);
            else if (type == "f32")
                paramCell.Value = float.Parse(value);
            else if (type == "b8" || type == "b32")
                paramCell.Value = bool.Parse(value);
            else if (type.StartsWith("fixstr"))
                paramCell.Value = value;
            else
                throw new NotImplementedException("Cannot convert cell type.");
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
    }
}
