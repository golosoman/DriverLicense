using UnityEngine;

public class TramMovement : RoadUserMovement
   {
       public float maxTramSpeed = 5f; // Максимальная скорость трамвая

       public void AdjustSpeed(float distanceToTarget)
       {
           CurrentSpeed = Mathf.Lerp(CurrentSpeed, maxTramSpeed, Time.deltaTime * Acceleration);
       }
   }