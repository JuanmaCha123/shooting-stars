using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minShootInterval = 1f;
    public float maxShootInterval = 3f;
    public GameObject projectilePrefab; // Prefab del proyectil que dispara este enemigo

    protected float nextShootTime;

    protected virtual void Start()
    {
        InitializeEnemy();
        CalculateNextShootTime();
    }

    protected virtual void Update()
    {
        if (Time.time >= nextShootTime)
        {
            ShootProjectile();
            CalculateNextShootTime();
        }
    }

    protected virtual void InitializeEnemy()
    {
        // Implementar en los tipos específicos de enemigo
    }

    protected virtual void CalculateNextShootTime()
    {
        nextShootTime = Time.time + Random.Range(minShootInterval, maxShootInterval);
    }

    protected virtual void ShootProjectile()
    {
        GameObject instantiatedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        instantiatedProjectile.GetComponent<EnemyProjectile>().SetShootDirection(Vector3.down); // Dirección de disparo hacia abajo por defecto
        instantiatedProjectile.GetComponent<EnemyProjectile>().SetProjectileSize(1.0f); // Tamaño base del proyectil
    }

    public void SetMovementStrategy(IMovementStrategy strategy)
    {
        // Implementar la lógica para asignar la estrategia de movimiento
    }
}
