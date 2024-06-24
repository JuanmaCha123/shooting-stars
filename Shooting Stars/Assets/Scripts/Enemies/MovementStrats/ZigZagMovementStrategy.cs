using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagMovementStrategy : IMovementStrategy
{
    private float moveSpeed;
    private float amplitude;
    private float frequency;
    private Vector3 startPosition;
    private bool moveRight = true;

    public ZigzagMovementStrategy(float moveSpeed, float amplitude, float frequency)
    {
        this.moveSpeed = moveSpeed;
        this.amplitude = amplitude;
        this.frequency = frequency;
        startPosition = Vector3.zero;
    }

    public void Move(Transform enemyTransform)
    {
        Vector3 offset = Vector3.right * amplitude * Mathf.Sin(frequency * Time.time);
        Vector3 movement = (startPosition + offset + Vector3.down * moveSpeed * Time.deltaTime) - enemyTransform.position;

        // Ajustar dirección si se alcanzan los límites laterales
        if (moveRight && (enemyTransform.position.x >= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x || enemyTransform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x))
        {
            moveRight = false;
        }
        else if (!moveRight && (enemyTransform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x || enemyTransform.position.x >= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x))
        {
            moveRight = true;
        }

        // Mover en la dirección determinada
        if (moveRight)
        {
            enemyTransform.Translate(movement.normalized * moveSpeed * Time.deltaTime);
        }
        else
        {
            enemyTransform.Translate(-movement.normalized * moveSpeed * Time.deltaTime);
        }
    }

    public void ReverseDirection()
    {
        moveRight = !moveRight;
    }
}
