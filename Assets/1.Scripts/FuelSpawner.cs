using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fuelPrefab;
    [SerializeField] private Transform fuelParentTransform;
    [SerializeField] private List<Transform> fuelSpawnPoints;

    private WaitForSeconds fuelSpawnTime = new WaitForSeconds(3f);

    private void Start()
    {
        StartCoroutine(fuelSpawnCo());
    }

    private IEnumerator fuelSpawnCo()
    {
        while (true)
        {
            yield return fuelSpawnTime;  

            int spawnIndex = Random.Range(0, fuelSpawnPoints.Count);
            Instantiate(fuelPrefab, fuelSpawnPoints[spawnIndex].position, Quaternion.identity, fuelParentTransform);
        }
    }
}
