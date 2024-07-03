using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMovement : IMovementStrategy
{
    public void Move(Enemy enemy)
    {
        enemy.transform.Translate(Vector3.down * enemy.Speed * Time.deltaTime);

        if (enemy.transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y)
        {
            GameObject.Destroy(enemy.gameObject);
        }
    }
}
