using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbleEnemy : MonoBehaviour
{
    void Start()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.health = 1;
        IMovementStrategy movementStrategy = new StraightDownMovementStrategy(4f);
        enemy.SetMovementStrategy(movementStrategy);
    }
}