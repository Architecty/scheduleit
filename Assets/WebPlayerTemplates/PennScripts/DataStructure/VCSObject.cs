using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

//VCS namespace for the object model used for the VCS3D 
namespace VCS
{
    //VCSObject - The root of the VCS data model
    //This class defines common attributes of objects that represent various types of objects used for construction simulation
    public class VCSObject
    {
        //attributes
        private string name;
        private string description;
        private string id;

        //constructors
        public VCSObject() : this ("", "", "")
        {
        }

        public VCSObject(string name, string description, string id)
        {
            this.Name = name;
            this.Description = description;
            this.Id = id;
        }

        //properties
        //[CategoryAttribute("Basic"), DescriptionAttribute("ID of the element")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        //[CategoryAttribute("Basic"), DescriptionAttribute("Name of the element")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

       //[CategoryAttribute("Basic"), DescriptionAttribute("Brief description of the element")]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
    }
}
