using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//accessing database
using System.Data.Common;
using System.Data.OleDb;

using System.IO;
using VCS;


namespace VCS
{
    //VCSBuildingElementGroup class
    //This class stores a list of building elements and its type information
    public class VCSBuildingElementGroup : VCSObject
    {
        private string groupType;
        private List<VCSBuildingElement> group;

        //constructors
        public VCSBuildingElementGroup()
        {
            groupType = "";
            group = new List<VCSBuildingElement>();
        }

        public VCSBuildingElementGroup(string type)
        {
            groupType = type;
            group = new List<VCSBuildingElement>();
        }

        //Properties
        public string BuildingElementGroupType
        {
            get { return groupType; }
            set { groupType = value; }
        }

        public List<VCSBuildingElement> Group
        {
            get { return group; }
            set { group = value; }
        }

        //Methods

        //returns the toal workload quantity of the building element list
        public double getTotalWorkloadQuantity()
        {
            double result = 0.0;

            for (int i = 0; i < group.Count; i++)
            {
                for (int j = 0; j < group[i].ConstructionActivities.Count; j++)
                {
                    result += group[i].ConstructionActivities[j].TotalWorkloadQuantity;
                }
            }

            return result;
        }

        //returns total workload quantity completed to-the-moment
        public double getWorkloadQuantityComplete()
        {
            double result = 0.0;

            //for each building element
            for (int i = 0; i < group.Count; i++)
            {
                //for each construction activity of the current building element 
                for (int j = 0; j < group[i].ConstructionActivities.Count; j++)
                {
                    //if the activity is already completed, add the total workload quantity
                    if (group[i].ConstructionActivities[j].CurrentStatus == (int)VCS3DConstants.ActivityStatus.COMPLETED)
                        result += group[i].ConstructionActivities[j].TotalWorkloadQuantity;
                    //if the activity is in-progress, add the workload quantity that has been completed
                    else if (group[i].ConstructionActivities[j].CurrentStatus == (int)VCS3DConstants.ActivityStatus.INPROGRESS)
                        result += (group[i].ConstructionActivities[j].TotalWorkloadQuantity - group[i].ConstructionActivities[j].RemainingWorkloadQuantity);
                }
            }
            return result;
        }


        //add a new building element
        public void addBuildingElement(VCSBuildingElement newElement)
        {
            group.Add(newElement);
        }

        //returns the name of the indexed building element 
        public string getBuildingElementName(int index)
        {
            return group[index].Name;
        }

        //returns the total number of the building elements in the group (list)
        public int getNumberOfElement()
        {
            return group.Count;
        }

    }
}
