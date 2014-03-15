using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using VCS;

namespace VCS
{
    //VCSConcreteFooting class - Subclass of VCSConcreteBuildingElement
    //This class defines concrete footing type 
    public class VCSConcreteFooting : VCSFooting 
    {
        //attributes
        private double formworkArea;
        private double rebarAmountLb;
        private double excavateCY; //cubic yard quantity for excavation
        private double placeCY;    //cubic yard quantity for placing

        //constructors
        public VCSConcreteFooting() : this ("", "", "", "", 0.0, 0.0, 0.0, 0.0)
        {

        }

        public VCSConcreteFooting(string fullname, string mat, string dim, string type, double excCY, double plaCY, double initFormworkArea, double initRebarAmountLB)
        {
            this.Name = fullname;
            this.Material = mat;
            this.DimensionString = dim;
            this.BuildingElementType = type;

            this.FormworkArea = initFormworkArea;
            this.RebarAmountLb = initRebarAmountLB;
            
            this.excavateCY = excCY;
            this.placeCY = plaCY;

        }

        //properties
        [CategoryAttribute("Concrete Footing"), ReadOnly(true), Browsable(true), DescriptionAttribute("Total Formwork Area")]
        public double FormworkArea
        {
            get { return formworkArea; }
            set { formworkArea = value; }
        }

        [CategoryAttribute("Concrete Footing"), ReadOnly(true), Browsable(true), DescriptionAttribute("Total amount of rebar (lb)")]
        public double RebarAmountLb
        {
            get { return rebarAmountLb; }
            set { rebarAmountLb = value; }
        }

        [CategoryAttribute("Concrete Footing"), ReadOnly(true), Browsable(true), DescriptionAttribute("Necessary Excavate Cubic Yard")]
        public double ExcavateCY
        {
            get { return excavateCY; }
            set { excavateCY = value; }
        }

        [CategoryAttribute("Concrete Footing"), ReadOnly(true), Browsable(true), DescriptionAttribute("Necessary Place Cubic Yard")]
        public double PlaceCY
        {
            get { return placeCY; }
            set { placeCY = value; }
        }

        //methods

    }
}
