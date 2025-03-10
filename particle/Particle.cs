﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evdokimov_David_PRI_121_CourseProject.particle
{
    class Partilce
    {
        private float[] position = new float[3];
        private float _size;
        private float _lifeTime;
        private float[] Grav = new float[3];
        private float[] power = new float[3];
        private float attenuation;
        private float[] speed = new float[3];
        private float LastTime = 0;

        public Partilce(float x, float y, float z, float size, float lifeTime, float start_time)
        {
            _size = size;
            _lifeTime = lifeTime;
            position[0] = x;
            position[1] = y;
            position[2] = z;
            Grav[0] = 0;
            Grav[1] = -9.8f;
            Grav[2] = 0;
            attenuation = 3.33f;
            LastTime = start_time;
        }

        public void SetPower(float x, float y, float z)
        {
            power[0] = x;
            power[1] = y;
            power[2] = z;
        }

        public void InvertSpeed(int os, float attenuation)
        {
            speed[os] *= -1 * attenuation;
        }

        public float GetSize()
        {
            return _size;
        }

        public void setAttenuation(float new_value)
        {
            attenuation = new_value;
        }

        public void UpdatePosition(float timeNow)
        {
            float dTime = timeNow - LastTime;
            _lifeTime -= dTime;
            LastTime = timeNow;
            for (int a = 0; a < 3; a++)
            {
                if (power[a] > 0)
                {
                    power[a] -= attenuation * dTime;
                    if (power[a] <= 0) power[a] = 0;
                }
                position[a] += Math.Abs(speed[a] * dTime + (Grav[a] + power[a]) * dTime * dTime);
                speed[a] += Math.Abs(Grav[a] + power[a]) * dTime * 15;
            }
        }

        public bool isLife()
        {
            return _lifeTime > 0;
        }

        public float GetPositionX()
        {
            return position[0];
        }

        public float GetPositionY()
        {
            return position[1];
        }

        public float GetPositionZ()
        {
            return position[2];
        }
    }
}
