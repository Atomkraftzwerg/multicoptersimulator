using Microsoft.Robotics.PhysicalModel;
using System;

using xna = Microsoft.Xna.Framework;

namespace MulticopterSimulation.Algorithms
{
    /// <summary>
    /// A class containing some help methods for math
    /// </summary>
    class MathHelper
    {
        // TODO Als Parameter eine Referenz übergeben und so den Vorgang hoffentlich beschleunigen

        #region Rotation around Axis (XNA)

        public static xna.Vector3 RotateAroundAxis_X(xna.Vector3 vector, xna.Vector3 pointToRotateAround, float angle)
        {
            float x, y, z, x1, y1, z1, xnew, ynew, znew, cosangle, sinangle;

            x = vector.X;
            y = vector.Y;
            z = vector.Z;

            x1 = pointToRotateAround.X;
            y1 = pointToRotateAround.Y;
            z1 = pointToRotateAround.Z;

            sinangle = (float)Math.Sin(angle);
            cosangle = (float)Math.Cos(angle);

            xnew = x;
            ynew = y1 + cosangle * (y - y1) - sinangle * (z - z1);
            znew = z1 + sinangle * (y - y1) + cosangle * (z - z1);

            return new xna.Vector3(xnew, ynew, znew);
        }

        public static xna.Vector3 RotateAroundAxis_Y(xna.Vector3 vector, xna.Vector3 pointToRotateAround, float angle)
        {
            float x, y, z, x1, y1, z1, xnew, ynew, znew, cosangle, sinangle;

            x = vector.X;
            y = vector.Y;
            z = vector.Z;

            x1 = pointToRotateAround.X;
            y1 = pointToRotateAround.Y;
            z1 = pointToRotateAround.Z;

            sinangle = (float)Math.Sin(angle);
            cosangle = (float)Math.Cos(angle);

            xnew = x1 + cosangle * (x - x1) - sinangle * (z - z1);
            ynew = y;
            znew = z1 + sinangle * (x - x1) + cosangle * (z - z1);

            return new xna.Vector3(xnew, ynew, znew);
        }

        public static xna.Vector3 RotateAroundAxis_Z(xna.Vector3 vector, xna.Vector3 pointToRotateAround, float angle)
        {
            float x, y, z, x1, y1, z1, xnew, ynew, znew, cosangle, sinangle;

            x = vector.X;
            y = vector.Y;
            z = vector.Z;

            x1 = pointToRotateAround.X;
            y1 = pointToRotateAround.Y;
            z1 = pointToRotateAround.Z;

            sinangle = (float)Math.Sin(angle);
            cosangle = (float)Math.Cos(angle);

            xnew = x1 + cosangle * (x - x1) - sinangle * (y - y1);
            ynew = y1 + sinangle * (x - x1) + cosangle * (y - y1);
            znew = z;

            return new xna.Vector3(xnew, ynew, znew);
        }

        #endregion

        #region Rotation around axis

        public static Vector3 RotateAroundAxis_X(Vector3 vector, Vector3 pointToRotateAround, float angle)
        {
            float x, y, z, x1, y1, z1, xnew, ynew, znew, cosangle, sinangle;

            x = vector.X;
            y = vector.Y;
            z = vector.Z;

            x1 = pointToRotateAround.X;
            y1 = pointToRotateAround.Y;
            z1 = pointToRotateAround.Z;

            sinangle = (float)Math.Sin(angle);
            cosangle = (float)Math.Cos(angle);

            xnew = x;
            ynew = y1 + cosangle * (y - y1) - sinangle * (z - z1);
            znew = z1 + sinangle * (y - y1) + cosangle * (z - z1);

            return new Vector3(xnew, ynew, znew);
        }

        public static Vector3 RotateAroundAxis_Y(Vector3 vector, Vector3 pointToRotateAround, float angle)
        {
            float x, y, z, x1, y1, z1, xnew, ynew, znew, cosangle, sinangle;

            x = vector.X;
            y = vector.Y;
            z = vector.Z;

            x1 = pointToRotateAround.X;
            y1 = pointToRotateAround.Y;
            z1 = pointToRotateAround.Z;

            sinangle = (float)Math.Sin(angle);
            cosangle = (float)Math.Cos(angle);

            xnew = x1 + cosangle * (x - x1) - sinangle * (z - z1);
            ynew = y;
            znew = z1 + sinangle * (x - x1) + cosangle * (z - z1);

            return new Vector3(xnew, ynew, znew);
        }

        public static Vector3 RotateAroundAxis_Z(Vector3 vector, Vector3 pointToRotateAround, float angle)
        {
            float x, y, z, x1, y1, z1, xnew, ynew, znew, cosangle, sinangle;

            x = vector.X;
            y = vector.Y;
            z = vector.Z;

            x1 = pointToRotateAround.X;
            y1 = pointToRotateAround.Y;
            z1 = pointToRotateAround.Z;

            sinangle = (float)Math.Sin(angle);
            cosangle = (float)Math.Cos(angle);

            xnew = x1 + cosangle * (x - x1) - sinangle * (y - y1);
            ynew = y1 + sinangle * (x - x1) + cosangle * (y - y1);
            znew = z;

            return new Vector3(xnew, ynew, znew);
        }

        #endregion
    }
}
