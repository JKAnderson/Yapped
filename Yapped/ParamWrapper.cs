using SoulsFormats;
using System;
using System.Collections.Generic;

namespace Yapped
{
    internal class ParamWrapper : IComparable<ParamWrapper>
    {
        public string Name { get; }

        public string Description { get; }

        public PARAM Param;

        public PARAM.Layout Layout;

        public List<PARAM.Row> Rows => Param.Rows;

        public ParamWrapper(string name, PARAM param, PARAM.Layout layout, string description)
        {
            Name = name;
            Param = param;
            Layout = layout;
            Param.SetLayout(layout);
            Description = description;
        }

        public int CompareTo(ParamWrapper other) => Name.CompareTo(other.Name);
    }
}
