using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectile : MonoBehaviour
{
    public float Speed = 5f;
    public Vector2 Direction;

    private void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);

        // Destruir el proyectil cuando sale de los límites de la cámara
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
        {
            Destroy(gameObject);
        }
    }
}