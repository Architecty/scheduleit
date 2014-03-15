using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using VCS;


namespace VCS
{
    //VCSConcreteSlab class - Subclass of VCSConcreteBuildingElement
    //This class defines concrete slab type
    //this class only contains attributes for the current version of VCS3D - Pavilion project
    //need more development for using other applications
    //for example, there is only one slab in the Pavilion project and it need excavation 
    public class VCSConcreteSlab : VCSSlab//: VCSConcreteBuildingElement
    {
        //attributes
        private double formworkArea;
        private double rebarAmountLb;
        private double excavateCY;  //cubic yard quantity for excavation
        private double placeCY;     //cubic yard quantity for placing

        //constructors
        public VCSConcreteSlab() : this("", "", "", "", 0.0, 0.0, 0.0, 0.0)
        {
        }

        public VCSConcreteSlab(string name, string mat, string dim_str, string type, double initFormworkArea, double initRebarAmountLB, double excCY, double plaCY)
        {
            this.Name = name;
            this.Material = mat;
            this.DimensionString = dim_str;
            this.BuildingElementType = type;
            this.FormworkArea = initFormworkArea;
            this.RebarAmountLb = initRebarAmountLB;
            this.ExcavateCY = excCY;
            this.PlaceCY = plaCY;

        }

        //properties
        [CategoryAttribute("Concrete Slab"), DescriptionAttribute("Total Formwork Area")]
        public double FormworkArea
        {
            get { return formworkArea; }
            set { formworkArea = value; }
        }

        [CategoryAttribute("Concrete Slab"), DescriptionAttribute("Total amount of rebar (lb)")]
        public double RebarAmountLb
        {
            get { return rebarAmountLb; }
            set { rebarAmountLb = value; }
        }

        [CategoryAttribute("Concrete Slab"), DescriptionAttribute("Necessary Excavate Cubic Yard")]
        public double ExcavateCY
        {
            get { return excavateCY; }
            set { excavateCY = value; }
        }

        [CategoryAttribute("Concrete Slab"), DescriptionAttribute("Necessary Place Cubic Yard")]
        public double PlaceCY
        {
            get { return placeCY; }
            set { placeCY = value; }
        }

        //methods

    }
}
