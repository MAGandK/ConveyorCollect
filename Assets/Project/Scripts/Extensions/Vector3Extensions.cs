using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 ChangeX(this Vector3 vector3, float newX)
        {
            return new Vector3(newX, vector3.y, vector3.z);
        }

        public static Vector3 ChangeY(this Vector3 vector3, float newY)
        {
            return new Vector3(vector3.x, newY, vector3.z);
        }

        public static Vector3 ChangeZ(this Vector3 vector3, float newZ)
        {
            return new Vector3(vector3.x, vector3.y, newZ);
        }

        public static bool IsBetween(
            this Vector3 position,
            Vector3 start,
            Vector3 end,
            float epsilon = 0.0001f)
        {
            if ((position - start).sqrMagnitude < epsilon * epsilon)
            {
                return true;
            }

            if ((position - end).sqrMagnitude < epsilon * epsilon)
            {
                return true;
            }

            var startToEnd = end - start;
            var lengthSquared = startToEnd.sqrMagnitude;

            if (lengthSquared < epsilon * epsilon)
            {
                return false;
            }

            var startToPos = position - start;

            if (Vector3.Cross(startToPos, startToEnd).sqrMagnitude >= epsilon * epsilon)
            {
                return false;
            }

            var dotProduct = Vector3.Dot(startToPos, startToEnd);

            return dotProduct >= -epsilon && dotProduct <= lengthSquared + epsilon;
        }

    }
}
