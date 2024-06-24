using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float minInterval = 0.3f;
    public float maxInterval = 3f;

    void Start()
    {
        Invoke("InstantiateRandomEnemy", Random.Range(minInterval, maxInterval));
    }

    private void InstantiateRandomEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomEnemyPrefab = enemyPrefabs[randomEnemyIndex];

        // Calcular posición aleatoria en X entre los límites laterales de la cámara
        float randomX = Random.Range(Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0, 0)).x);
        Vector3 spawnPosition = new Vector3(randomX, Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).y, 0);

        // Instanciar enemigo y asignar estrategia aleatoria
        GameObject instantiatedEnemy = Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);

        // Asignar una estrategia aleatoria al enemigo
        IMovementStrategy[] strategies = {
            new StraightDownMovementStrategy(2f),
            new ZigzagMovementStrategy(1f, 1f, 1f),
            new ChasePlayerMovementStrategy(1f, FindObjectOfType<PlayerController>().transform)
        };
        int randomStrategyIndex = Random.Range(0, strategies.Length);
        instantiatedEnemy.GetComponent<Enemy>().SetMovementStrategy(strategies[randomStrategyIndex]);

        // Programar la siguiente instancia
        Invoke("InstantiateRandomEnemy", Random.Range(minInterval, maxInterval));
    }
}
