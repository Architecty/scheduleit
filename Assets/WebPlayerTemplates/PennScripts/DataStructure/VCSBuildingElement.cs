using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


using VCS;

//VCS namespace for the object model used for the VCS3D 
namespace VCS
{
    //[DefaultPropertyAttribute("Building Element Properties")]
    //VCSBuildingElement class
    //This class defines common attributes of building elements such as walls and columns
    //Specific building elements are defined as a sub-class of VCSBuildingElement class
    public class VCSBuildingElement : VCSObject {
		
        //dimensions: string type
        private string dimensions_string;

        //building element type
        private string buildingElementType;

        //materials
        private string material;

        //geometric representation file name
        private VCSGeometry geometricRepresentation;

        //construction activities
        private List<VCSConstructionActivity> constructionActivities;

        //physical constraints
        private List<string> physicalConstraintList;

        //construction status
        private VCS3DConstants.ObjectConstructionStatus constructionStatus;

        //constructors
        public VCSBuildingElement() : this (0.0, 0.0, 0.0, "", "", null)
        {
        }

        public VCSBuildingElement(double dim_x, double dim_y, double dim_z, string dim_str, string mat, VCSGeometry geometry)
        {
            //x = dim_x; y = dim_y; z = dim_z; 
            dimensions_string = dim_str;
            material = mat;
            geometricRepresentation = geometry;
            constructionActivities = new List<VCSConstructionActivity>();
            physicalConstraintList = new List<string>();
            constructionStatus = VCS3DConstants.ObjectConstructionStatus.NOTSTARTED;
        }
		
		public VCSBuildingElement(string name, string dim_str, string mat)
        {
            this.Name = name;
            dimensions_string = dim_str;
            material = mat;
            geometricRepresentation = new VCSGeometry("name 1", Color.blue, false, false);
            constructionActivities = new List<VCSConstructionActivity>();
            physicalConstraintList = new List<string>();
            constructionStatus = VCS3DConstants.ObjectConstructionStatus.NOTSTARTED;
        }
		
        public VCSBuildingElement(string name, string dim_str, string mat, VCSGeometry geometry)
        {
            this.Name = name;
            dimensions_string = dim_str;
            material = mat;
            geometricRepresentation = geometry;
            constructionActivities = new List<VCSConstructionActivity>();
            physicalConstraintList = new List<string>();
            constructionStatus = VCS3DConstants.ObjectConstructionStatus.NOTSTARTED;
        }


        //Properties
        public VCS3DConstants.ObjectConstructionStatus ConstructionStatus
        {
            get { return constructionStatus; }
            set { constructionStatus = value; }
        }

        public List<string> PhysicalConstraintList
        {
            get { return physicalConstraintList; }
            set { physicalConstraintList = value; }
        }

        public List<VCSConstructionActivity> ConstructionActivities
        {
            get { return constructionActivities; }
            set { constructionActivities = value; }
        }

        /*
        //[CategoryAttribute("Dimensions"), DescriptionAttribute("X coordinate")]
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        //[CategoryAttribute("Dimensions"), DescriptionAttribute("Y coordinate")]
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        //[CategoryAttribute("Dimensions"), DescriptionAttribute("Z coordinate")]
        public double Z
        {
            get { return z; }
            set { z = value; }
        }
        */
		
		
        //[CategoryAttribute("Geometry"), TypeConverter(typeof(ExpandableObjectConverter))]
        public VCSGeometry GeometricRepresentation
        {
            get { return geometricRepresentation; }
            set { geometricRepresentation = value; }
        }

        //[CategoryAttribute("Dimensions"), DescriptionAttribute("string type dimensions")]
        public string DimensionString
        {
            get { return dimensions_string; }
            set { dimensions_string = value; }
        }
        
        //[CategoryAttribute("Construction"), DescriptionAttribute("Material")]
        public string Material
        {
            get { return material; }
            set { material = value; }
        }

        public string BuildingElementType
        {
            get { return buildingElementType; }
            set { buildingElementType = value; }
        }

        //methods
        public void addConstructionActivity(VCSConstructionActivity act)
        {
            constructionActivities.Add(act);
        }

        public void loadGeometryRepresentation(string filename)
        {
            
        }

        public VCSConstructionActivity getConstructionActivity(int index)
        {
            return constructionActivities[index];
        }

        public void removeConstructionActivity(int index)
        {
            constructionActivities.RemoveAt(index);
        }
       
    }
}

