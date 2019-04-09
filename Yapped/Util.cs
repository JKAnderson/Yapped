using SoulsFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CellType = SoulsFormats.PARAM64.CellType;

namespace Yapped
{
    internal static class Util
    {
        public static Dictionary<string, PARAM64.Layout> LoadLayouts(string directory)
        {
            var layouts = new Dictionary<string, PARAM64.Layout>();
            if (Directory.Exists(directory))
            {
                foreach (string path in Directory.GetFiles(directory, "*.xml"))
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
            }
            return layouts;
        }

        public static LoadParamsResult LoadParams(string paramPath, Dictionary<string, ParamInfo> paramInfo,
            Dictionary<string, PARAM64.Layout> layouts, bool hideUnusedParams)
        {
            if (!File.Exists(paramPath))
            {
                ShowError($"Parambnd not found:\r\n{paramPath}\r\nPlease browse to the Data0.bdt or parambnd you would like to edit.");
                return null;
            }

            var result = new LoadParamsResult();
            try
            {
                if (BND4.Is(paramPath))
                {
                    result.ParamBND = BND4.Read(paramPath);
                    result.Encrypted = false;
                }
                else
                {
                    result.ParamBND = SFUtil.DecryptDS3Regulation(paramPath);
                    result.Encrypted = true;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Failed to load parambnd:\r\n{paramPath}\r\n\r\n{ex}");
                return null;
            }

            result.ParamWrappers = new List<ParamWrapper>();
            foreach (BinderFile file in result.ParamBND.Files.Where(f => f.Name.EndsWith(".param")))
            {
                string name = Path.GetFileNameWithoutExtension(file.Name);
                if (paramInfo.ContainsKey(name))
                {
                    if (paramInfo[name].Blocked || paramInfo[name].Hidden && hideUnusedParams)
                        continue;
                }

                try
                {
                    PARAM64 param = PARAM64.Read(file.Bytes);
                    if (layouts.ContainsKey(param.ID))
                    {
                        PARAM64.Layout layout = layouts[param.ID];
                        if (layout.Size == param.DetectedSize)
                        {
                            string description = null;
                            if (paramInfo.ContainsKey(name))
                                description = paramInfo[name].Description;

                            var wrapper = new ParamWrapper(name, param, layout, description);
                            result.ParamWrappers.Add(wrapper);
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowError($"Failed to load param file: {name}.param\r\n\r\n{ex}");
                }
            }

            result.ParamWrappers.Sort();
            return result;
        }

        public static string ValidateCell(CellType type, string text)
        {
            if (type == CellType.s8)
            {
                if (!sbyte.TryParse(text, out _))
                {
                    return "Invalid value for signed byte.";
                }
            }
            else if (type == CellType.u8)
            {
                if (!byte.TryParse(text, out _))
                {
                    return "Invalid value for unsigned byte.";
                }
            }
            else if (type == CellType.x8)
            {
                try
                {
                    Convert.ToByte(text, 16);
                }
                catch
                {
                    return "Invalid value for hex byte.";
                }
            }
            else if (type == CellType.s16)
            {
                if (!short.TryParse(text, out _))
                {
                    return "Invalid value for signed short.";
                }
            }
            else if (type == CellType.u16)
            {
                if (!ushort.TryParse(text, out _))
                {
                    return "Invalid value for unsigned short.";
                }
            }
            else if (type == CellType.x16)
            {
                try
                {
                    Convert.ToUInt16(text, 16);
                }
                catch
                {
                    return "Invalid value for hex short.";
                }
            }
            else if (type == CellType.s32)
            {
                if (!int.TryParse(text, out _))
                {
                    return "Invalid value for signed int.";
                }
            }
            else if (type == CellType.u32)
            {
                if (!uint.TryParse(text, out _))
                {
                    return "Invalid value for unsigned int.";
                }
            }
            else if (type == CellType.x32)
            {
                try
                {
                    Convert.ToUInt32(text, 16);
                }
                catch
                {
                    return "Invalid value for hex int.";
                }
            }
            else if (type == CellType.f32)
            {
                if (!float.TryParse(text, out _))
                {
                    return "Invalid value for float.";
                }
            }
            else if (type == CellType.b8 || type == CellType.b32)
            {
                if (!bool.TryParse(text, out _))
                {
                    return "Invalid value for bool.";
                }
            }
            else if (type == CellType.fixstr || type == CellType.fixstrW)
            {
                // Don't see how you could mess this up
            }
            else
                throw new NotImplementedException("Cannot validate cell type.");

            return null;
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    internal class LoadParamsResult
    {
        public bool Encrypted { get; set; }

        public BND4 ParamBND { get; set; }

        public List<ParamWrapper> ParamWrappers { get; set; }
    }
}
