using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VCS;
namespace VCS
{
    //VCSCamera class defines a camera used for rendering VCS geometric models
    //attributes: x, y, z coordinates of the camera
    //attributes: pitch, yaw and roll value of the camera (rotation data)
    public class VCSCamera : VCSObject
    {
        //attributes
        private float x;
        private float y;
        private float z;

        private float pitch;
        private float yaw;
        private float roll;

        //constructors
        public VCSCamera() : this(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f)
        {
        }

        public VCSCamera(float initX, float initY, float initZ, float initPitch, float initYaw, float initRoll)
        {
            this.x = initX;
            this.y = initY;
            this.z = initZ;
            this.pitch = initPitch;
            this.yaw = initYaw;
            this.roll = initRoll;
        }

        //properties
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public float Pitch
        {
            get { return pitch; }
            set { pitch = value; }
        }

        public float Yaw
        {
            get { return yaw; }
            set { yaw = value; }
        }

        public float Roll
        {
            get { return roll; }
            set { roll = value; }
        }
    }
}
