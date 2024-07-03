using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMovement : IMovementStrategy
{
    private float direction = 1;

    public void Move(Enemy enemy)
    {
        enemy.transform.Translate(new Vector3(direction, -1, 0) * enemy.Speed * Time.deltaTime);

        if (enemy.transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x ||
            enemy.transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            direction *= -1;
        }

        if (enemy.transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y)
        {
            GameObject.Destroy(enemy.gameObject);
        }
    }
}