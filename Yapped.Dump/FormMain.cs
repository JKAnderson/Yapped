using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
                    string layoutPath = $"Layouts\\{param.ID}.txt";

                    txtStatus.AppendText(file.Name + "\r\n");

                    var worksheet = package.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(file.Name));

                    PARAM64.Layout layout;
                    if (File.Exists(layoutPath))
                    {
                        layout = new PARAM64.Layout(File.ReadAllText(layoutPath));
                        if (layout.Size != param.DetectedSize)
                            layout = new PARAM64.Layout("");
                    }
                    else
                    {
                        layout = new PARAM64.Layout("");
                    }

                    param.SetLayout(layout);
                    List<PARAM64.Row> rows = param.Rows;

                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Translated";
                    int columnCount = 3;
                    foreach (PARAM64.Layout.Entry lv in layout)
                        if (!lv.Type.StartsWith("dummy8"))
                            worksheet.Cells[1, ++columnCount].Value = lv.Name;

                    for (int i = 0; i < rows.Count; i++)
                    {
                        PARAM64.Row row = rows[i];
                        worksheet.Cells[i + 2, 1].Value = row.ID;
                        worksheet.Cells[i + 2, 2].Value = row.Name;
                        worksheet.Cells[i + 2, 3].Value = MSTranslate(row.Name);
                        columnCount = 3;

                        foreach (PARAM64.Cell cell in row.Cells)
                        {
                            string type = cell.Type;
                            if (!type.StartsWith("dummy8"))
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

            StringBuilder sb = new StringBuilder();
            foreach (string key in translated.Keys)
            {
                sb.AppendLine(key);
                sb.AppendLine(translated[key]);
            }
            File.WriteAllText("out.txt", sb.ToString());
        }

        private static Regex gtJsonExtract = new Regex(@"\[""(.+?)(?<!\\)"","".+?(?<!\\)"",null,null,(0|1|2|3)\]");

        private static Dictionary<string, string> translated = new Dictionary<string, string>();

        private static AzureAuthToken authToken = new AzureAuthToken("cc69c4fc76864e9da8b1fb95474bf7dc");

        static string host = "https://api.cognitive.microsofttranslator.com";
        static string path = "/translate?api-version=3.0";
        static string params_ = "&from=ja&to=en";

        static string uri = host + path + params_;

        // NOTE: Replace this example key with a valid subscription key.
        static string key = "cc69c4fc76864e9da8b1fb95474bf7dc";

        private static string MSTranslate(string text)
        {
            if (text == null || text.Trim().Length == 0)
                return text;

            text = text.Trim();
            if (translated.ContainsKey(text))
                return translated[text];

            System.Object[] body = new System.Object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = client.SendAsync(request).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<Dictionary<string, List<Dictionary<string, string>>>>>(responseBody);
                var translations = result[0]["translations"];
                var translation = translations[0]["text"];

                translated[text] = translation;
                return translation;
            }
        }
    }
}
