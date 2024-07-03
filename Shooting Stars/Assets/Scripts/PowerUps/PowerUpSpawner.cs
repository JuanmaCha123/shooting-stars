using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject shieldPrefab;
    public GameObject lifeUpPrefab;
    public GameObject extraPointsPrefab;

    public float spawnInterval = 5f;

    private ICloneable shieldPrototype;
    private ICloneable lifeUpPrototype;
    private ICloneable extraPointsPrototype;

    
    void Start()
    {
        shieldPrototype = shieldPrefab.GetComponent<ICloneable>();
        lifeUpPrototype = lifeUpPrefab.GetComponent<ICloneable>();
        extraPointsPrototype = extraPointsPrefab.GetComponent<ICloneable>();

        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnPowerUps()
    {
        while (EnemyInstanciator.Instance.IsFighting == true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomIndex = Random.Range(0, 3);
            ICloneable powerUpToSpawn;

            switch (randomIndex)
            {
                case 0:
                    powerUpToSpawn = shieldPrototype;
                    break;
                case 1:
                    powerUpToSpawn = lifeUpPrototype;
                    break;
                case 2:
                    powerUpToSpawn = extraPointsPrototype;
                    break;
                default:
                    powerUpToSpawn = shieldPrototype;
                    break;
            }

            ICloneable clonedPowerUp = powerUpToSpawn.Clone();
            GameObject powerUpObject = (clonedPowerUp as MonoBehaviour).gameObject;

            float cameraHeight = Camera.main.orthographicSize * 2;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            float xPos = Random.Range(Camera.main.transform.position.x - cameraWidth / 2, Camera.main.transform.position.x + cameraWidth / 2);
            float yPos = Camera.main.transform.position.y + cameraHeight / 2;

            powerUpObject.transform.position = new Vector3(xPos, yPos, 0);

            spawnInterval = Random.Range(3, 7);
        }
    }
}
