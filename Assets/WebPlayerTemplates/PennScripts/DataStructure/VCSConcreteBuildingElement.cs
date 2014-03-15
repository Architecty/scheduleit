using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using VCS;

namespace VCS
{
    //VCSConcreteBuildingElement class - subclass of VCSBuildingElement
    //This class defines common attributes of concrete building elements
    public class VCSConcreteBuildingElement : VCSBuildingElement
    {
        //attributes
        private double formworkArea;
        private double rebarAmountLb;

        //constructors
        public VCSConcreteBuildingElement() : this("", "", "", "", 0.0, 0.0)
        {
        }

        public VCSConcreteBuildingElement(string name, string mat, string dim_str, string type, double initFormworkArea, double initRebarAmountLB)
        {
            this.Name = name;
            this.Material = mat;
            this.DimensionString = dim_str;
            this.BuildingElementType = type;
            this.FormworkArea = initFormworkArea;
            this.RebarAmountLb = initRebarAmountLB;
        }

        //properties
        [CategoryAttribute("Concrete Construction"), DescriptionAttribute("Total Formwork Area")]
        public double FormworkArea
        {
            get { return formworkArea; }
            set { formworkArea = value; }
        }

        [CategoryAttribute("Concrete Construction"), DescriptionAttribute("Total amount of rebar (lb)")]
        public double RebarAmountLb
        {
            get { return rebarAmountLb; }
            set { rebarAmountLb = value; }
        }

        //methods

    }
}
