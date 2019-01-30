using SoulsFormats;
using System;
using System.Collections.Generic;

namespace Yapped
{
    internal class ParamWrapper : IComparable<ParamWrapper>
    {
        public string Name { get; }

        public string Description { get; }

        public PARAM64 Param;

        public PARAM64.Layout Layout;

        public List<PARAM64.Row> Rows => Param.Rows;

        public ParamWrapper(string name, PARAM64 param, PARAM64.Layout layout, string description)
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
