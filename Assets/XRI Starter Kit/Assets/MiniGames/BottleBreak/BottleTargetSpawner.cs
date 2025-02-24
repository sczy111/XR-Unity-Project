using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MikeNspired.UnityXRHandPoser
{
    public class BottleTargetSpawner : MonoBehaviour
    {
        public Bottle currentItem;
        [SerializeField] private Bottle prefab = null;
        [SerializeField] private float MinSpawnTime = 0;
        [SerializeField] private float ManSpawnTime = 4;
        private TransformStruct startingPosition;

        public UnityEvent OnBottleBroke;
        public GameObject spherePrefab;
        public Transform playerTransform;
        public Transform fixedTargetPoint;

        void Start()
        {
            startingPosition.SetTransformStruct(currentItem.transform.localPosition, currentItem.transform.localRotation, currentItem.transform.localScale);
            currentItem.onHit.AddListener(BottleDestroyed);
        }

        private void BottleDestroyed(float x)
        {
            OnBottleBroke.Invoke();

            if (spherePrefab && fixedTargetPoint)
            {
                // Ensure sphere spawns at the bottle's height
                Vector3 spawnPos = currentItem.transform.position + Vector3.up * 1.5f; 
                GameObject sphere = Instantiate(spherePrefab, spawnPos, Quaternion.identity);

                Rigidbody rb = sphere.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.isKinematic = false; 
                      
                            

                    // Direction toward the fixed target position
                    Vector3 directionToTarget = (fixedTargetPoint.position - spawnPos).normalized;

                    float force = 8f; // Adjust if needed
                    rb.velocity = directionToTarget * force; 
                }
                else
                {
                    Debug.LogError("Rigidbody is missing from the spherePrefab!");
                }
            }

            StartCoroutine(CreateNewItem());
        }



        private IEnumerator CreateNewItem()
        {
            yield return new WaitForSeconds(Random.Range(MinSpawnTime, ManSpawnTime));
            currentItem = Instantiate(prefab);
            currentItem.transform.parent = transform;
            currentItem.transform.localPosition = startingPosition.position;
            currentItem.transform.localRotation = startingPosition.rotation;
            currentItem.onHit.AddListener(BottleDestroyed);

        }
    }


}