using UnityEngine;
using System.Collections;

public class EnemyInstanciator : MonoBehaviour
{
    public bool IsFighting = true;
    public float MinTime = 1f;
    public float MaxTime = 5f;
    public GameObject[] EnemyPrefabs;

    public static EnemyInstanciator Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(IncreaseSpawnRate());
    }

    IEnumerator SpawnEnemies()
    {
        while (IsFighting)
        {
            // Esperar un tiempo aleatorio entre MinTime y MaxTime
            float waitTime = Random.Range(MinTime, MaxTime);
            yield return new WaitForSeconds(waitTime);

            // Seleccionar un prefab aleatorio
            int enemyIndex = Random.Range(0, EnemyPrefabs.Length);
            GameObject enemyPrefab = EnemyPrefabs[enemyIndex];

            // Calcular una posición aleatoria dentro de los límites de la cámara
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Instanciar el enemigo
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Obtener los límites de la cámara
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float xPos = Random.Range(Camera.main.transform.position.x - cameraWidth / 2, Camera.main.transform.position.x + cameraWidth / 2);
        float yPos = Camera.main.transform.position.y + cameraHeight / 2;

        return new Vector3(xPos, yPos, 0);
    }

    IEnumerator IncreaseSpawnRate()
    {
        float totalTime = 0f;
        while (totalTime < 180f) // 3 minutos
        {
            yield return new WaitForSeconds(30f);
            totalTime += 30f;

            MinTime = Mathf.Max(0.5f, MinTime - 0.5f);
            MaxTime = Mathf.Max(1f, MaxTime - 0.5f);
        }

        MinTime = 0.5f;
        MaxTime = 0.5f;
    }
}