using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("CarRef")]
    [SerializeField] private CarController CarController;

    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;
    private EnemyMovement enemyMovement;
    [SerializeField] private Transform enemyParentTransform;
    [SerializeField] private List<Transform> enemySpawnPoints;

    [Header("Level")]
    [SerializeField]private List<float> enemySpawnTimesByLevel;
    private WaitForSeconds enemySpawnTime;
    [SerializeField] private List<float> enemySpeedsByLevel;
    private int levelIndex;

    private void Start()
    {
        enemySpawnTime = new WaitForSeconds(enemySpawnTimesByLevel[0]);

        StartCoroutine(fuelSpawnCo());
    }

    private IEnumerator fuelSpawnCo()
    {
        while (true)
        {
            yield return enemySpawnTime;

            int spawnIndex = Random.Range(0, enemySpawnPoints.Count);
            enemyMovement = Instantiate(enemyPrefab, enemySpawnPoints[spawnIndex].position, Quaternion.identity, enemyParentTransform).GetComponent<EnemyMovement>();

            enemyMovement.speed = enemySpeedsByLevel[levelIndex = CarController.level < enemySpeedsByLevel.Count ? CarController.level : enemySpeedsByLevel.Count - 1];
            enemySpawnTime = new WaitForSeconds
                             (enemySpawnTimesByLevel[levelIndex = CarController.level < enemySpawnTimesByLevel.Count ? CarController.level : enemySpawnTimesByLevel.Count - 1]);
        }
    }
}
