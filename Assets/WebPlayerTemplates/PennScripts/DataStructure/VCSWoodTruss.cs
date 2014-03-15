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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using VCS;
//VCS namespace for the object model used for the VCS3D
namespace VCS
{
    //VCSWoodTruss class - subclass of VCSWoodBuildingElement class
    //This class defines attributes and methods of a truss made of wood
    public class VCSWoodTruss : VCSTruss
    {
        //attributes
        private double span;
        private string trussType;

        //constructors
        public VCSWoodTruss() : this("", "", "", "")
        {
        }

        public VCSWoodTruss(string name, string mat, string dim_str, string type)
        {
            this.Name = name;
            this.Material = mat;
            this.DimensionString = dim_str;
            this.BuildingElementType = type;
        }

        //properties
        [CategoryAttribute("WoodTruss"), ReadOnly(true), Browsable(true), DescriptionAttribute("Span value")]
        public double Span
        {
            get { return span; }
            set { span = value; }
        }

        [CategoryAttribute("WoodTruss"), ReadOnly(true), Browsable(true), DescriptionAttribute("Truss Type")]
        public string TrussType
        {
            get { return trussType; }
            set { trussType = value; }
        }

        //methods
    }
}
