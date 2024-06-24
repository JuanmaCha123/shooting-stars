using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    void Start()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.health = 4;
        IMovementStrategy movementStrategy = new StraightDownMovementStrategy(1f);
        enemy.SetMovementStrategy(movementStrategy);
    }
}