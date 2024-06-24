using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicEnemy : Enemy
{
    public float projectileSizeMultiplier = 1.0f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void InitializeEnemy()
    {
        base.InitializeEnemy();
        IMovementStrategy movementStrategy = new StraightDownMovementStrategy(2f);
        SetMovementStrategy(movementStrategy);
    }

    protected override void ShootProjectile()
    {
        base.ShootProjectile();
        GameObject instantiatedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        instantiatedProjectile.GetComponent<EnemyProjectile>().SetShootDirection(Vector3.down); // Dirección de disparo hacia abajo por defecto para BasicEnemy
        instantiatedProjectile.GetComponent<EnemyProjectile>().SetProjectileSize(projectileSizeMultiplier);
    }
}
