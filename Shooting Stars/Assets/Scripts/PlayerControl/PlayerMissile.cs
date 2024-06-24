using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask collisionLayers;

    void Update()
    {
        // Mover el misil hacia arriba
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el misil ha colisionado con un objeto en las capas de colisión especificadas
        if (((1 << collision.gameObject.layer) & collisionLayers) != 0)
        {
            // Destruir el misil
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
