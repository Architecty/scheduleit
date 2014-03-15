/* COPY RIGHT 
 * Copyright © 2010 Sang Hoon Lee, Lorne Leonard, Dragana Nikolic, and John I. Messner
 * Computer-Integrated Construction Program
 * Department of Architectural Engineering
 * Pennsylvania State University
 *
 * Contact: 
 * Sang Hoon Lee: shlatpsu@gmail.com
 * Dragana Nikolic: dragana@psu.edu
 * John Messner: jmessner@engr.psu.edu
 *
 * This program is developed as a outcome of the research project, 
 * "Virtual Construction Simulator 3D: A Simulation Game for Construction Engineering Education"
 * supported by National Science Foundation. 
 * 
 * This program is distributed under the GNU General Public License (see below), 
 * provided that the following conditions are met: 
 * 1. A program that uses this source code must be open source and shared with other developers and
 * researchers for further development of construction simulation. 
 * 2. This source code must not be used to create commercial products.
 * 
 * The authors also want to receive any program and its source code that is built upon this source code 
 * so that this program can be updated with additional features
 * If you do not agree, do not download, install, use, modify this program.
 *
 *
 **** GNU General Public License ***
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *** End of GNU General Public License ***
 */

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


using VCS;

//VCS namespace for the object model used for the VCS3D 
namespace VCS
{
    //VCSConstructionActivity class - Subclass of VCSObject
    //This class defines a construction activity
    public class VCSConstructionActivity : VCSObject
    {
        //attributes
        private int sequenceNumber; //activity sequence number
        private string constructionMethod;  //construction method that the user selected for the activity
        private int currentStatus;  //status: not started, in-progress, complete
        private double asPlannedDuration; //days
        private double asBuiltDuration; //days
        private int priority;
        
        private string crewType;    //the type of crew allocated to the activity based on the selected method
        private int crewSize;       //the number of crew team
        private int plannedCrewSize;
        
        public List<VCSCrew> crewList;  //a list of crew. 
        public List<VCSHumanResource> humanResourceList;            //a list of human resources allocated to the activity during simulation
        public List<VCSEquipmentResource> equipmentResourceList;    //a list of equipment resources allocated to the activity during simulation


        private VCSBuildingElementGroup buildingElementGroup;   //building element group of the construction activity
        private string buildingElementType; //the type of the building element associated to the construction activity
        
        private string predecessorsString;  //predecessors in the string format
        public List<int> successorList;     //list of successors of the activity. The next activity list when this activity is completed
        public List<int> predecessorList;   //list of predecessors of the activity. The predecessors that need to be completed to start this activity

        private double totalWorkloadQuantity;   //workload quantity of the activity 
        private double remainingWorkloadQuantity;   //remaining workload quantity during simulation

        //Critical Path Method Parameters
        private double asPlannedEST;    //as-planned earliest starting time
        private double asPlannedEET;    //as-planned earliest ending time
        private double asPlannedLST;    //as-planned latest starting time
        private double asPlannedLET;    //as-planned latest ending time
        private double asBuiltEST;      //as-built earliest starting time
        private double asBuiltEET;      //as-built earliest ending time
        private double asBuiltLST;      //as-built latest starting time
        private double asBuiltLET;      //as-built latest ending time

        //construction simulation related
        private bool isPassedPhysicalConstraints;
        private bool isResourcesAssigned;
        private bool isScheduledToStartToday;

        //helper attributes
        private bool isFlaggedOnce;

        //constructors
        public VCSConstructionActivity() //: this (0, "", 0.0, 0.0, "", 0, new List<int>(), new VCSBuildingElementGroup(), "", "", 0.0)
        {
            sequenceNumber = 0;
            constructionMethod = "";
            asPlannedDuration = 0.0;
            asBuiltDuration = 0.0;
            priority = 0;

            crewType = "";
            crewSize = 1;
            plannedCrewSize = 1;
            crewList = new List<VCSCrew>();

            humanResourceList = new List<VCSHumanResource>();
            equipmentResourceList = new List<VCSEquipmentResource>();

            predecessorList = new List<int>();
            successorList = new List<int>();
            buildingElementGroup = new VCSBuildingElementGroup();

            buildingElementType = "";
            predecessorsString = "";
            totalWorkloadQuantity = 0.0;

            asPlannedEST = 0.0;
            asBuiltEST = 0.0;

            currentStatus = (int) VCS3DConstants.ActivityStatus.NOTSTARTED;
            isPassedPhysicalConstraints = false;
            isResourcesAssigned = false;
            isScheduledToStartToday = false;

            isFlaggedOnce = false;
        }

        public VCSConstructionActivity(int number, string method, double asPlannedDurationValue, double asBuiltDurationValue, string crewValue, int crewSizeValue, List<int> predecessorsList, VCSBuildingElementGroup buildingElementGroupValue, string BEType, string predecessorsSTR, double quantity)
        {
            sequenceNumber = number;
            constructionMethod = method;
            asPlannedDuration = asPlannedDurationValue;
            asBuiltDuration = asBuiltDurationValue;
            priority = 0;
            
            crewType = crewValue;
            crewSize = crewSizeValue;
            crewList = new List<VCSCrew>();
            humanResourceList = new List<VCSHumanResource>();
            equipmentResourceList = new List<VCSEquipmentResource>();

            predecessorList = predecessorsList;
            buildingElementGroup = buildingElementGroupValue;
            buildingElementType = BEType;
            predecessorsString = predecessorsSTR;
            totalWorkloadQuantity = quantity;

            currentStatus = (int) VCS3DConstants.ActivityStatus.NOTSTARTED;
            isPassedPhysicalConstraints = false;
            isResourcesAssigned = false;
            isScheduledToStartToday = false;

            isFlaggedOnce = false;
        }

        //properties
        public bool IsFlaggedOnce
        {
            get { return isFlaggedOnce; }
            set { isFlaggedOnce = value; }
        }

        public int PlannedCrewSize
        {
            get { return plannedCrewSize; }
            set { plannedCrewSize = value; }
        }

        public bool IsPassedPhysicalConstraints
        {
            get { return isPassedPhysicalConstraints; }
            set { isPassedPhysicalConstraints = value; }
        }

        public bool IsResourcesAssigned
        {
            get { return isResourcesAssigned; }
            set { isResourcesAssigned = value; }
        }

        public bool IsScheduledToStartToday
        {
            get { return isScheduledToStartToday; }
            set { isScheduledToStartToday = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public List<VCSCrew> CrewList
        {
            get { return crewList; }
            set { crewList = value; }
        }

        public int CurrentStatus
        {
            get { return currentStatus; }
            set { currentStatus = value; }
        }

        public double RemainingWorkloadQuantity
        {
            get { return remainingWorkloadQuantity; }
            set { remainingWorkloadQuantity = value; }
        }

        public double AsPlannedEST
        {
            get { return asPlannedEST; }
            set { asPlannedEST = value; }
        }

        public double AsPlannedEET
        {
            get { return asPlannedEET; }
            set { asPlannedEET = value; }
        }

        public double AsPlannedLST
        {
            get { return asPlannedLST; }
            set { asPlannedLST = value; }
        }

        public double AsPlannedLET
        {
            get { return asPlannedLET; }
            set { asPlannedLET = value; }
        }

        public double AsBuiltEST
        {
            get { return asBuiltEST; }
            set { asBuiltEST = value; }
        }

        public double AsBuiltEET
        {
            get { return asBuiltEET; }
            set { asBuiltEET = value; }
        }

        public double AsBuiltLST
        {
            get { return asBuiltLST; }
            set { asBuiltLST = value; }
        }

        public double AsBuiltLET
        {
            get { return asBuiltLET; }
            set { asBuiltLET = value; }
        }

        public double TotalWorkloadQuantity
        {
            get { return totalWorkloadQuantity; }
            set { totalWorkloadQuantity = value; }
        }

        public string PredecessorsString
        {
            get { return predecessorsString; }
            set { predecessorsString = value; }
        }

        public string BuildingElementType
        {
            get { return buildingElementType; }
            set { buildingElementType = value; }
        }

        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        public string ConstructionMethod
        {
            get { return constructionMethod; }
            set { constructionMethod = value; }
        }

        public double AsPlannedDuration
        {
            get { return asPlannedDuration; }
            set { asPlannedDuration = value; }
        }

        public double AsBuiltDuration
        {
            get { return asBuiltDuration; }
            set { asBuiltDuration = value; }
        }

        public string CrewType
        {
            get { return crewType; }
            set { crewType = value; }
        }

        public int CrewSize
        {
            get { return crewSize; }
            set { crewSize = value; }
        }

        public VCSBuildingElementGroup BuildingElementGroup
        {
            get { return buildingElementGroup; }
            set { buildingElementGroup = value; }
        }
    }
}
