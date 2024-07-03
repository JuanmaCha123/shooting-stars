using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bossEnemyPrefab;
    private float elapsedTime = 0f;
    private bool bossSpawned = false;
    public static GameManager instance;

    public GameObject store;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {

        elapsedTime += Time.deltaTime;

        // Verificar si han pasado 4 minutos (240 segundos) y el jefe no ha sido instanciado
        if (elapsedTime >= 200f && !bossSpawned)
        {
            SpawnBoss();
            bossSpawned = true;
        }
    }

    private void SpawnBoss()
    {

        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        float xPos = Camera.main.transform.position.x;
        float yPos = Camera.main.transform.position.y + cameraHeight / 2 + 1;

        Instantiate(bossEnemyPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);


        if (EnemyInstanciator.Instance != null)
        {
            EnemyInstanciator.Instance.IsFighting = false;
        }
    }

    public void GameOver()
    {
        EnemyInstanciator.Instance.IsFighting = false;
        store.SetActive(false);
        gameOverScreen.SetActive(true);
        ClearScreen();
    }

    public void Victory()
    {
        EnemyInstanciator.Instance.IsFighting = false;
        store.SetActive(false);
        victoryScreen.SetActive(true);
        ClearScreen();
    }

    public void ClearScreen()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] EnemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }

        foreach(GameObject projectile in EnemyProjectiles)
        {
            Destroy(projectile.gameObject);
        }

        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup.gameObject);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
    }

}