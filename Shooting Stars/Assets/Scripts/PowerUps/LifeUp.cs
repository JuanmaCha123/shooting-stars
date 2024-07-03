using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : PowerUp
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Heal();
            Destroy(this.gameObject);
        }
    }
}
