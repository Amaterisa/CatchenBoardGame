using UnityEngine;

namespace General.ParabolicCalculator
{
    public static class ParabolicCalculator
    {
        //parabola: y = x^2 + k
        //where the distance between point 1 and point 2 is 2 * sqrt(k)
        public static float CalculateParabolicConstant(float initialPosition, float finalPosition)
        {
            var distance = finalPosition - initialPosition;
            return Mathf.Pow(distance, 2) / 4;
        }

        public static Vector3 ParabolicFunction(Vector3 initialPosition, Vector3 finalPosition, Vector3 currentPosition,  float k, float t)
        {
            var x = Mathf.Lerp(initialPosition.x, finalPosition.x, t);
            var y = -Mathf.Pow(x, 2) + k;
            return new Vector3(x, y, currentPosition.z);
        }
    }
}
