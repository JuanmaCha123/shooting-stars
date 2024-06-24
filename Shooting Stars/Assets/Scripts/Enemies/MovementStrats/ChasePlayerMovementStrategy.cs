using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerMovementStrategy : IMovementStrategy
{
    private float moveSpeed;
    private Transform playerTransform;

    public ChasePlayerMovementStrategy(float moveSpeed, Transform playerTransform)
    {
        this.moveSpeed = moveSpeed;
        this.playerTransform = playerTransform;
    }

    public void Move(Transform enemyTransform)
    {
        Vector3 direction = (playerTransform.position - enemyTransform.position).normalized;
        enemyTransform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
