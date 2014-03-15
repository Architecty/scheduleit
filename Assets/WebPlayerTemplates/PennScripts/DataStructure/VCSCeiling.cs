using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCS;

namespace VCS
{
    //VCSCeiling class - subclass of VCSBuildingElement
    //this class defines attributes of Ceiling
    //more ceiling-specific attributes need to be defined.
    public class VCSCeiling : VCSBuildingElement
    {
        //attributes


        //constructors
        public VCSCeiling() : this("", "", "", "")
        {

        }

        public VCSCeiling(string name, string mat, string dim_str, string type)
        {
            this.Name = name;
            this.Material = mat;
            this.DimensionString = dim_str;
            this.BuildingElementType = type;
        }

        //Properties


        //Methods

    }
}
