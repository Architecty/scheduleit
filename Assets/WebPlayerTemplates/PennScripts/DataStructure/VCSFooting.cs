using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VCS;

//VCS namespace for the object model used for the VCS3D
namespace VCS
{
    //VCSFooting class - subclass of VCSBuildingElement class
    //This class defines common attributes and methods of a footing 
    public class VCSFooting : VCSBuildingElement
    {
        //attributes


        //constructors
        public VCSFooting() : this("", "", "", "")
        {
        }

        public VCSFooting(string name, string mat, string dim_str, string type)
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