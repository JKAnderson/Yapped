using SoulsFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Yapped.Test
{
    public partial class FormMain : Form
    {
        private static Properties.Settings settings = Properties.Settings.Default;

        private string regulationPath;
        private List<ParamFile> paramFiles;
        private Dictionary<string, LayoutWrapper> layouts;

        public FormMain()
        {
            InitializeComponent();

            layouts = new Dictionary<string, LayoutWrapper>();
            foreach (string path in Directory.GetFiles("Layouts"))
            {
                string format = Path.GetFileNameWithoutExtension(path);
                layouts[format] = new LayoutWrapper(File.ReadAllText(path));
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                ofdRegulation.InitialDirectory = Path.GetDirectoryName(regulationPath);
            }
            catch { }

            if (ofdRegulation.ShowDialog() == DialogResult.OK)
            {
                regulationPath = ofdRegulation.FileName;
                LoadRegulation(regulationPath);
            }
        }

        private void LoadRegulation(string path)
        {
            BND4 bnd;
            try
            {
                bnd = Core.ReadRegulation(path);
            }
            catch (Exception ex)
            {
                ShowError($"Failed to load regulation file:\r\n\r\n{path}\r\n\r\n{ex}");
                return;
            }

            paramFiles = new List<ParamFile>();
            foreach (BND4.File file in bnd.Files)
            {
                if (Path.GetExtension(file.Name) == ".param")
                {
                    try
                    {
                        PARAM64 param = PARAM64.Read(file.Bytes);
                        paramFiles.Add(new ParamFile(Path.GetFileNameWithoutExtension(file.Name), param, layouts));
                    }
                    catch
                    {

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

        public class LayoutWrapper
        {
            public string Layout { get; set; }

            public LayoutWrapper(string layout)
            {
                Layout = layout;
            }
        }

        public class ParamFile
        {
            public string Name { get; set; }
            public PARAM64.Layout Layout;
            public PARAM64 Param;
            public List<PARAM64.Row> Rows { get; set; }

            public ParamFile(string name, PARAM64 param, Dictionary<string, LayoutWrapper> layouts)
            {
                Name = name;
                Param = param;
                string format = Param.ID;
                if (!layouts.ContainsKey(format))
                    layouts[format] = new LayoutWrapper("");

                try
                {
                    Layout = new PARAM64.Layout(layouts[format].Layout);
                    Param.SetLayout(Layout);
                    Rows = Param.Rows;
                }
                catch (Exception ex)
                {
                    Rows = new List<PARAM64.Row>();
                    ShowError($"Error in layout {format}, please try again.\r\n\r\n{ex}");
                }
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void dgvParams_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvParams.SelectedCells.Count > 0)
            {
                ParamFile paramFile = (ParamFile)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
                dgvRows.DataSource = paramFile;
                dgvRows.DataMember = "Rows";

                txtLayout.DataBindings.Clear();
                txtLayout.DataBindings.Add("Text", layouts[paramFile.Param.ID], "Layout");

                if (paramFile.Rows.Count == 0 || paramFile.Param.DetectedSize == paramFile.Layout.Size)
                    lblSizeWarning.Visible = false;
                else
                {
                    lblSizeWarning.Text = $"Warning: layout size (0x{paramFile.Layout.Size:X}) does not equal detected param size (0x{paramFile.Param.DetectedSize:X}).";
                    lblSizeWarning.Visible = true;
                }
            }
        }

        private void dgvRows_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRows.SelectedCells.Count > 0)
            {
                PARAM64.Row row = (PARAM64.Row)dgvRows.SelectedCells[0].OwningRow.DataBoundItem;
                dgvCells.DataSource = row;
                dgvCells.DataMember = "Cells";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ParamFile selectedParam = (ParamFile)dgvRows.DataSource;
            PARAM64.Row selectedRow = (PARAM64.Row)dgvCells.DataSource;

            LoadRegulation(regulationPath);

            dgvParams.ClearSelection();
            foreach (DataGridViewRow row in dgvParams.Rows)
                if (((ParamFile)row.DataBoundItem).Name == selectedParam.Name)
                    row.Cells[0].Selected = true;

            dgvRows.ClearSelection();
            foreach (DataGridViewRow row in dgvRows.Rows)
                if (((PARAM64.Row)row.DataBoundItem).ID == selectedRow.ID)
                    row.Cells[0].Selected = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ParamFile paramFile = (ParamFile)dgvRows.DataSource;
            string format = paramFile.Param.ID;
            LayoutWrapper layoutWrapper = layouts[format];
            Directory.CreateDirectory("Layouts");
            File.WriteAllText($"Layouts\\{format}.txt", layoutWrapper.Layout);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = "Yapped.Test " + Application.ProductVersion;
            Location = settings.WindowLocation;
            if (settings.WindowSize.Width >= MinimumSize.Width && settings.WindowSize.Height >= MinimumSize.Height)
                Size = settings.WindowSize;
            if (settings.WindowMaximized)
                WindowState = FormWindowState.Maximized;

            regulationPath = settings.RegulationPath;
            LoadRegulation(regulationPath);
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
        }

        private void dgvCells_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            PARAM64.Cell cell = (PARAM64.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
            if (cell.Type == "x32" && e.ColumnIndex == 1)
            {
                e.Value = $"0x{e.Value:X8}";
            }
        }
    }
}
