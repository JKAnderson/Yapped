using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Yapped
{
    internal class ParamInfo
    {
        public static Dictionary<string, ParamInfo> ReadParamInfo(string path)
        {
            var result = new Dictionary<string, ParamInfo>();
            if (File.Exists(path))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                foreach (XmlNode node in xml.SelectNodes("params/param"))
                {
                    string name = node.Attributes["name"].InnerText;
                    bool hidden = bool.Parse(node.Attributes["hidden"]?.InnerText ?? "false");
                    string description = node.InnerText;

                    if (!string.IsNullOrEmpty(description))
                        result[name] = new ParamInfo(hidden, description);
                }
            }
            return result;
        }

        public bool Hidden;

        public string Description;

        private ParamInfo(bool hidden, string description)
        {
            Hidden = hidden;
            Description = description;
        }
    }
}
