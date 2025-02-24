using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikeNspired.UnityXRHandPoser
{
    public class BallCollisionHandler : MonoBehaviour
    {
        public FloatSO totalTargetsHit;

        private void OnCollisionEnter(Collision collision)
        {
            // Check if the ball hits the scoring zone (floor)
            if (collision.gameObject.CompareTag("ScoringZone"))
            {
                Debug.Log("Ball hit the scoring zone! +1 Score!");

                // Increase score
                totalTargetsHit.SetValue(totalTargetsHit.GetValue() + 1);

                // Destroy the ball after scoring (optional)
                Destroy(gameObject, 1f);
            }
        }
    }
}

