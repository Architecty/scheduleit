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
using System.Collections;
using System.Linq;
using System.Text;

//using System.Drawing;
using System.ComponentModel;


using VCS;

//VCS namespace for the object model used for the VCS3D 
namespace VCS
{
    //VCSGeometry class - subclass of VCSObject
    //This class defines common properties of geometric models used for VCS3D
    //This class uses XNA game engine to render geometric models
    //As this class intends to be an abstract class that defines attributes and methods, 
    //further development is necessary in the future that has VCSXNAGeometry, or other classes as a subclass
    //[DefaultPropertyAttribute("Geometry Properties")]
    public class VCSGeometry : VCSObject
    {
        //attributes
        private string geometryFileName; //geometry file name that actually has geometry data
        //private System.Drawing.Color color;  //RGB
		private Color rgbColor;
        private float alpha;  //transparency


        //draw options
        private bool isDrawOn;      //On when the geometry is drawn, off when the geometry is hidden
        private bool isHighlighted; //On when the geometry is highlighted
        private bool isColorOn;     //On when the geometry is drawn in color, not its texture

        //constructors
        public VCSGeometry(): this ("", new Color(), false, false)
        {
        }

        public VCSGeometry(string modelName, Color color, bool isDrawOn, bool isHighlighted)
        {
            try
            {
                this.geometryFileName = modelName;
                this.rgbColor = color;
                this.alpha = 1.0f;
                this.isDrawOn = isDrawOn;
                this.isHighlighted = isHighlighted;
                this.isColorOn = false;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("VCSGeometry.VCSGeometry() failed: " + ex.Message);
            }
        }

        //properties
        //[CategoryAttribute("Geometry"), DescriptionAttribute("Color")]
        public Color RGBColor
        {
            get { return rgbColor; }
            set { rgbColor = value; }
        }
		
        //[CategoryAttribute("Geometry"), DescriptionAttribute("Transparency")]
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
		
        //[CategoryAttribute("Geometry"), DescriptionAttribute("DrawOn")]
        public bool IsDrawOn
        {
            get { return isDrawOn; }
            set { isDrawOn = value; }
        }
		
        //[CategoryAttribute("Geometry"), DescriptionAttribute("HighlightOn")]
        public bool IsHighlighted
        {
            get { return isHighlighted; }
            set { isHighlighted = value; }
        }
		
        //[CategoryAttribute("Geometry"), DescriptionAttribute("Draw with Color")]
        public bool IsColorModeOn
        {
            get { return isColorOn; }
            set { isColorOn = value; }
        }
		
        //[CategoryAttribute("Geometry"), DescriptionAttribute("Geometry File Name")]
        public string GeometryFileName
        {
            get { return geometryFileName; }
            set { geometryFileName = value; }
        }

        //methods

    }
}
