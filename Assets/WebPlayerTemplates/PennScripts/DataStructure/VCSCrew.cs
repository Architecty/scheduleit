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

using VCS;

//VCS namespace for the object model used for the VCS3D 
namespace VCS
{
    //VCSCrew class - subclass of VCSResource
    //This class defines a crew, a combination of human resources and equipment resources, to perform a construction activity
    public class VCSCrew : VCSResource
    {
        //attributes
        private string activityAssigned;    //activity name that this crew performs

        public List<VCSHumanResource> humanResources;   //human resources in the crew
        public List<VCSEquipmentResource> equipmentResources;   //equipment resources in the crew
        public List<string> resourceTypeList;   //a list of resource types

        //constructors
        public VCSCrew() : this ("", "")
        {
            //humanResources = new List<VCSHumanResource>();
            //equipmentResources = new List<VCSEquipmentResource>();
            //id = "";
            //activityAssigned = "";
        }

        public VCSCrew(string crewType, string newID)
        {
            humanResources = new List<VCSHumanResource>();
            equipmentResources = new List<VCSEquipmentResource>();
            resourceTypeList = new List<string>();
            this.Name = crewType;
            this.Id = newID;
            activityAssigned = "";

            this.formCrew(crewType);
        }


        //properties
        public string ActivityAssigned
        {
            get { return activityAssigned; }
            set { activityAssigned = value; }
        }

        //methods
        public void addHumanResource(VCSHumanResource newHR)
        {
            humanResources.Add(newHR);
        }

        public void removeHumanResource(int index)
        {
            humanResources.RemoveAt(index);
        }

        public VCSHumanResource getHumanResource(int index)
        {
            return humanResources[index];
        }

        public void addEquipmentResource(VCSEquipmentResource newER)
        {
            equipmentResources.Add(newER);
        }

        public void removeEquipmentResource(int index)
        {
            equipmentResources.RemoveAt(index);
        }

        public VCSEquipmentResource getEquipmentResource(int index)
        {
            return equipmentResources[index];
        }

        //method to form a crew based on the crew types defined in RSMeans 
        private void formCrew(string crewType)
        {
            try
            {
                //one laborer
                if (crewType.ToLower() == "1 laborer")
                {
                    humanResources.Add(new VCSHumanResource("Laborer"));
                    this.Description = "1 Laborer";
                    resourceTypeList.Add("Laborer");
                }
                //one carpenter
                else if (crewType.ToLower() == "1 carpenter")
                {
                    humanResources.Add(new VCSHumanResource("Carpenter"));
                    this.Description = "1 Carpenter";
                    resourceTypeList.Add("Carpenter");
                }
                //two carpenters
                else if (crewType.ToLower() == "2 carpenters")
                {
                    humanResources.Add(new VCSHumanResource("Carpenter"));
                    humanResources.Add(new VCSHumanResource("Carpenter"));
                    this.Description = "2 Carpenters";
                    resourceTypeList.Add("Carpenter");
                    resourceTypeList.Add("Carpenter");
                }
                //one rodman
                else if (crewType.ToLower() == "1 rodman")
                {
                    humanResources.Add(new VCSHumanResource("Rodman"));
                    this.Description = "1 Rodman";
                    resourceTypeList.Add("Rodman");
                }
                //two rodmen
                else if (crewType.ToLower() == "2 rodman")
                {
                    humanResources.Add(new VCSHumanResource("Rodman"));
                    humanResources.Add(new VCSHumanResource("Rodman"));
                    this.Description = "2 Rodman";
                    resourceTypeList.Add("Rodman");
                    resourceTypeList.Add("Rodman");
                }
                //one roofer and one laborer
                else if (crewType.ToLower() == "1 roofer 1 laborer") //Dragana 4-12
                {
                    humanResources.Add(new VCSHumanResource("Roofer"));
                    humanResources.Add(new VCSHumanResource("Laborer"));//DRagana 4-12
                    this.Description = "1 Roofer 1 Laborer";
                    resourceTypeList.Add("Roofer");
                    resourceTypeList.Add("Laborer");//Dragana 4-12
                }
                //one laborer, one excavator and its operator
                else if (crewType.ToLower() == "1 laborer 1 operator 1 excavator")
                {
                    humanResources.Add(new VCSHumanResource("Laborer"));
                    humanResources.Add(new VCSHumanResource("Equipment operator"));
                    equipmentResources.Add(new VCSEquipmentResource("Bobcat excavator"));
                    this.Description = "1 Laborer, 1 Equipment operator, 1 Bobcat Excavator";
                    resourceTypeList.Add("Laborer");
                    resourceTypeList.Add("Equipment operator");
                    resourceTypeList.Add("Bobcat excavator");
                }
                //one laborer, one truck and its operator
                else if (crewType.ToLower() == "1 laborer 1 operator 1 truck")
                {
                    humanResources.Add(new VCSHumanResource("Laborer"));
                    humanResources.Add(new VCSHumanResource("Equipment operator"));
                    equipmentResources.Add(new VCSEquipmentResource("Truck"));
                    this.Description = "1 Laborer, 1 Equipment Operator, 1 Truck";
                    resourceTypeList.Add("Laborer");
                    resourceTypeList.Add("Equipment operator");
                    resourceTypeList.Add("Truck");
                }
                //one laborer and one cart
                else if (crewType.ToLower() == "1 laborer 1 cart")
                {
                    humanResources.Add(new VCSHumanResource("Laborer"));
                    equipmentResources.Add(new VCSEquipmentResource("Cart (10CF)"));
                    this.Description = "1 Laborer, 1 Cart (10CF)";
                    resourceTypeList.Add("Laborer");
                    resourceTypeList.Add("Cart (10CF)");
                }
                //one laborer, one concrete pump and its operator
                else if (crewType.ToLower() == "1 laborer 1 operator 1 concrete pump")
                {
                    humanResources.Add(new VCSHumanResource("Laborer"));
                    humanResources.Add(new VCSHumanResource("Equipment operator"));
                    equipmentResources.Add(new VCSEquipmentResource("Concrete pump"));
                    this.Description = "1 Laborer, 1 Equipment Operator, 1 Concrete Pump";
                    resourceTypeList.Add("Laborer");
                    resourceTypeList.Add("Equipment operator");
                    resourceTypeList.Add("Concrete pump");
                }
                //one laborer and one carpenter
                else if (crewType.ToLower() == "1 carpenter 1 laborer")
                {
                    humanResources.Add(new VCSHumanResource("Carpenter"));
                    humanResources.Add(new VCSHumanResource("Laborer"));
                    this.Description = "1 Carpenter, 1 Laborer";
                    resourceTypeList.Add("Carpenter");
                    resourceTypeList.Add("Carpenter");
                    resourceTypeList.Add("Laborer");
                }
                else if (crewType.ToLower() == "1 carpenter 1 operator 1 crane")
                {
                    humanResources.Add(new VCSHumanResource("Carpenter"));
                    humanResources.Add(new VCSHumanResource("Equipment operator"));
                    equipmentResources.Add(new VCSEquipmentResource("Crane12t"));
                    this.Description = "2 Carpenters, 1 Equipment Operator, 1 Crane12t";
                    resourceTypeList.Add("Carpenter");
                    resourceTypeList.Add("Carpenter");
                    resourceTypeList.Add("Equipment operator");
                    resourceTypeList.Add("Crane12t");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("VCSCrew.formCrew() failed: " + ex.Message);
            }
        }
    }
}
