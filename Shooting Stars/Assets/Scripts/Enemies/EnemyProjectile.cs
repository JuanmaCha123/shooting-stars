using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 shootDirection; // Dirección en la que fue disparado el proyectil

    void Update()
    {
        Move();

        // Destruir el proyectil si sale de la vista de la cámara
        if (!IsVisibleByCamera())
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(shootDirection * speed * Time.deltaTime, Space.World);
    }

    private bool IsVisibleByCamera()
    {
        Camera mainCamera = Camera.main;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        return (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1);
    }

    public void SetShootDirection(Vector3 direction)
    {
        shootDirection = direction.normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
    }

    public void SetProjectileSize(float sizeMultiplier)
    {
        transform.localScale *= sizeMultiplier;
    }
}