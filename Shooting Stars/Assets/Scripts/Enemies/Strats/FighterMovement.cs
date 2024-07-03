using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : IMovementStrategy
{
    private float timeMovingDown = 0.5f;
    private float timer = 0;
    private bool movingDown = true;
    private float direction = 1;

    public void Move(Enemy enemy)
    {
        if (movingDown)
        {
            enemy.transform.Translate(Vector3.down * enemy.Speed * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= timeMovingDown)
            {
                movingDown = false;
            }
        }
        else
        {
            enemy.transform.Translate(Vector3.right * direction * enemy.Speed * Time.deltaTime);

            if (enemy.transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x ||
                enemy.transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
            {
                direction *= -1;
            }
        }
    }
}