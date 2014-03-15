using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCS;

namespace VCS
{
    //VCSBeam class - subclass of VCSBuildingElement class
    //This class defines common attributes and methods of a beam
    public class VCSBeam : VCSBuildingElement
    {
        //attributes

        //constructors
        public VCSBeam() : this("", "", "", "")
        {
        }

        public VCSBeam(string name, string mat, string dim_str, string type)
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
