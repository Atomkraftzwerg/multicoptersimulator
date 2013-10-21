using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticopterSimulation.Algorithms
{
    class PIDController
    {
        // The constants for the PID algorihm
        public float Kp = 1.0f;
        public float Ki = 1.0f;
        public float Kd = 1.0f;

        public float referenceValue;
        public float controlValueMaxChange;

        private float error;
        private float integral;
        private float derivate;
        private float prevIntegral;
        private float prevError;
        private float result;
        private float controlValueChange;

        public float DebugOutputValue { get { return result; } } 

        public PIDController()
        {
            referenceValue = 0.0f;
            controlValueMaxChange = 0.05f;

            Reset();
        }

        public void Control(float outputValue, ref float controlValue)
        {
            error = referenceValue - outputValue;
            integral = integral + error;
            derivate = error - prevError;
            result = Kp * error + Ki * integral + Kd * derivate;

            // Prevents sudden value changes
            controlValueChange = result - controlValue;
            if (controlValueChange > controlValueMaxChange)
                controlValue += controlValueMaxChange;
            else if (controlValueChange < -controlValueMaxChange)
                controlValue -= controlValueMaxChange;
            else
                controlValue += controlValueChange;

            prevError = error;
        }

        public void Control(float outputValue, float dt, ref float controlValue)
        {
            error = referenceValue - outputValue;

            result = Kp * error + (Ki * integral * dt) + (Kd * derivate / dt);

            // Prevents sudden value changes
            controlValueChange = result - controlValue;
            if (controlValueChange > controlValueMaxChange)
                controlValue += controlValueMaxChange;
            else if (controlValueChange < -controlValueMaxChange)
                controlValue -= controlValueMaxChange;
            else
                controlValue += controlValueChange;

            integral = prevIntegral + error;
            derivate = error - prevError;

            prevIntegral = integral;
            prevError = error;
        }

        public void Reset()
        {
            error = 0.0f;
            integral = 0.0f;
            derivate = 0.0f;
            prevIntegral = 0.0f;
            prevError = 0.0f;
            result = 0.0f;
            controlValueChange = 0.0f;
        }

        #region Deprecated code

        // Simplified PID-Algorithm to keep altitude
        //if (keepAltitude && raw_leftY == 0.0f)
        //{
        //    float altitudeToKeep = 15.0f;
        //    float gain = 1.0f;
        //    float ft = Position.Y - altitudeToKeep;
        //    raw_thrust = (wasFt - ft) * gain + wasThrust;
        //    wasFt = ft;
        //    wasThrust = raw_thrust;
        //}

        // Variante 3: Bestimmte Höhe halten quadratische Formel
        //if (keepAltitude && raw_leftY == 0.0f)
        //{
        //    float incValue = (State.Velocity.Y * State.Velocity.Y) * 0.01f;

        //    if (State.Velocity.Y < -0.03f)
        //    {
        //        raw_thrust += incValue > 0.1f ? 0.1f : incValue;
        //    }
        //    else if (State.Velocity.Y > 0.03f)
        //    {
        //        raw_thrust -= incValue > 0.1f ? 0.1f : incValue;
        //    }
        //}

        // Variante 1: Bestimmte Höhe halten
        //if (Position.Y < 40.0)
        //    raw_thrust = MAX_THRUST;
        //else if (Position.Y < 49.5)
        //    raw_thrust = MAX_THRUST / 2.0f;
        //else if (Position.Y > 60.0)
        //    raw_thrust = 0.0f;
        //else if (Position.Y > 50.5)
        //    raw_thrust = MAX_THRUST / 3.0f;
        //else
        //    raw_leftY = 0.0f;

        // Variante 2: Thrust suchen, bei der der Multicopter die aktuelle Höhe hält Fautrieb=Fgravitation
        //if (Position.Y > 1.0f)
        //{
        //    if (State.Velocity.Y < -0.03f)
        //    {
        //        if (State.Velocity.Y > -0.5f)
        //        {
        //            raw_thrust += (-State.Velocity.Y / 0.5f) * 0.01f;
        //        }
        //        else
        //        {
        //            raw_thrust += 0.01f;
        //        }                    
        //    }
        //    else if (State.Velocity.Y > 0.03f)
        //    {
        //        if (State.Velocity.Y < 0.5f)
        //        {
        //            raw_thrust -= (State.Velocity.Y / 0.5f) * 0.01f;
        //        }
        //        else
        //        {
        //            raw_thrust -= 0.01f;
        //        } 
        //    }
        //}

        #endregion
    }
}
