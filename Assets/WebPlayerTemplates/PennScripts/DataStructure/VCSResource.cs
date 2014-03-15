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
    //VCSResource class - subclass of VCSObject class
    //This class defines common attributes of resources used for VCS3D simulation
    //Current version has VCSHumanResource class and VCSEquipmentResource as subclasses
    public class VCSResource : VCSObject
    {
        //attributes
        private double hourlyCost;  //hourly cost  = daily cost / daily work hour (default: 8)
        private double dailyCost;   //daily cost
        
        private double dailyOutput; //daily output

        //construction simulation-related attributes
        private string resourceType;
        private bool isRecruited;                       //true if the resource is recruited for the specific work day
        private bool isWorking;                         //true if the resource is actually working 
        private bool isAssignedToActivity;              //true if the resource is assigned to a specific activity by the user
        private string assignedActivityName;            //the name of the activity that the resource is assigned to
        private string constructionMethodForActivity;   //construction method of the activity that the resource is assigned to
        private string targetBuildingElementGroup;      //the building element group name that the construction activity belongs to

        //constructors
        public VCSResource() : this ("", 0.0, 0.0, 0.0)
        {

        }

        public VCSResource(string type, double hourlycost, double dailycost, double dailyoutput)
        {
            resourceType = type;
            hourlyCost = hourlycost;
            dailyCost = dailycost;
            dailyOutput = dailyoutput;
            isRecruited = false;
            isWorking = false;
            isAssignedToActivity = false;
            assignedActivityName = "";
            constructionMethodForActivity = "";
            targetBuildingElementGroup = "";
        }

        //properties
        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("A list of Target Building Elements")]
        public string TargetBuildingElementGroup
        {
            get { return targetBuildingElementGroup; }
            set { targetBuildingElementGroup = value; }
        }

        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("Construction Method For Activity")]
        public string ConstructionMethodForActivity
        {
            get { return constructionMethodForActivity; }
            set { constructionMethodForActivity = value; }
        }

        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("The name of the activity the resource is assigned to")]
        public string AssignedActivityName
        {
            get { return assignedActivityName; }
            set { assignedActivityName = value; }
        }

        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("True when the resource is assigned to an activity")]
        public bool IsAssignedToActivity
        {
            get { return isAssignedToActivity; }
            set { isAssignedToActivity = value; }
        }

        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("True when the resource is hired for the day")]       
        public bool IsRecruited
        {
            get { return isRecruited; }
            set { isRecruited = value; }
        }
        
        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("True when the resource is actually working")]       
        public bool IsWorking
        {
            get { return isWorking; }
            set { isWorking = value; }
        }

        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("Hourly Cost")]
        public double HourlyCost
        {
            get { return hourlyCost; }
            set { hourlyCost = value; }
        }

        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("Resource Type")]
        public string ResourceType
        {
            get { return resourceType; }
            set { resourceType = value; }
        }

        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("Daily Cost")]
        public double DailyCost
        {
            get { return dailyCost; }
            set { dailyCost = value; }
        }

        [CategoryAttribute("Resource"), ReadOnly(true), Browsable(true), DescriptionAttribute("Daily Output")]
        public double DailyOutput
        {
            get { return dailyOutput; }
            set { dailyOutput = value; }
        }

        //methods
    }
}
