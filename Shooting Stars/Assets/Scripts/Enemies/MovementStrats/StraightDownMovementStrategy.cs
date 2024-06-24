using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightDownMovementStrategy : IMovementStrategy
{
    private float moveSpeed;

    public StraightDownMovementStrategy(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void Move(Transform enemyTransform)
    {
        enemyTransform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
