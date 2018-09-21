using Newtonsoft.Json;
using OfficeOpenXml;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Yapped.Dump
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnDump_Click(object sender, EventArgs e)
        {
            BND4 bnd;
            try
            {
                bnd = Core.ReadRegulation(txtRegulation.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load regulation:\r\n\r\n{txtRegulation.Text}\r\n\r\n{ex}");
                return;
            }

            var package = new ExcelPackage();

            foreach (BND4.File file in bnd.Files)
            {
                if (Path.GetExtension(file.Name) == ".param")
                {
                    PARAM64 param = PARAM64.Read(file.Bytes);
                    string layoutPath = $"Layouts\\{param.ID}.xml";

                    txtStatus.AppendText(file.Name + "\r\n");

                    var worksheet = package.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(file.Name));

                    PARAM64.Layout layout;
                    if (File.Exists(layoutPath))
                    {
                        layout = PARAM64.Layout.ReadXMLFile(layoutPath);
                        if (layout.Size != param.DetectedSize)
                            layout = new PARAM64.Layout();
                    }
                    else
                    {
                        layout = new PARAM64.Layout();
                    }

                    param.SetLayout(layout);
                    List<PARAM64.Row> rows = param.Rows;

                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Translated";
                    int columnCount = 3;
                    foreach (PARAM64.Layout.Entry lv in layout)
                        if (lv.Type != "dummy8")
                            worksheet.Cells[1, ++columnCount].Value = lv.Name;

                    for (int i = 0; i < rows.Count; i++)
                    {
                        PARAM64.Row row = rows[i];
                        worksheet.Cells[i + 2, 1].Value = row.ID;
                        worksheet.Cells[i + 2, 2].Value = row.Name;
                        worksheet.Cells[i + 2, 3].Value = "<translated name>";
                        columnCount = 3;

                        foreach (PARAM64.Cell cell in row.Cells)
                        {
                            string type = cell.Type;
                            if (type != "dummy8")
                            {
                                var range = worksheet.Cells[i + 2, ++columnCount];
                                if (type == "f32")
                                    range.Value = (double)(float)cell.Value;
                                else if (type.StartsWith("fixstr"))
                                    range.Value = (string)cell.Value;
                                else if (type == "b8" || type == "b32")
                                {
                                    bool b = (bool)cell.Value;
                                    range.Value = b.ToString();
                                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    range.Style.Fill.BackgroundColor.SetColor(b ? Color.LightGreen : Color.LightPink);
                                }
                                else
                                    range.Value = cell.Value;
                            }
                        }
                    }

                    worksheet.Row(1).Style.Font.Bold = true;
                    worksheet.Column(1).Style.Font.Bold = true;
                    worksheet.View.FreezePanes(2, 4);
                    //worksheet.DefaultColWidth = 10;
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                }
            }

            FileInfo f = new FileInfo(Path.Combine(txtOutput.Text, "dump.xlsx"));
            package.SaveAs(f);
        }
    }
}
