using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCS;

namespace VCS
{
    //VCSColumn class - subclass of VCSBuildingElement class
    //This class defines a column for construction simulation
    public class VCSColumn : VCSBuildingElement
    {
        //attributes


        //constructors
        public VCSColumn() : this("", "", "", "")
        {
        }

        public VCSColumn(string name, string mat, string dim_str, string type)
        {
            this.Name = name;
            this.Material = mat;
            this.DimensionString = dim_str;
            this.BuildingElementType = type;
        }

        //properties


        //methods
    }
}
