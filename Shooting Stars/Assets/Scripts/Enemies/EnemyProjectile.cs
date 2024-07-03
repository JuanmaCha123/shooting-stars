using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float Speed = 5f;

    void Update()
    {
        // Mover el proyectil hacia abajo
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        // Verificar si el proyectil ha salido de los l�mites de la c�mara y destruirlo
        if (IsOutOfCameraBounds())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutOfCameraBounds()
    {
        // Obtener las coordenadas del proyectil en relaci�n a la c�mara
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Verificar si est� fuera de los l�mites de la c�mara
        return viewportPosition.y < 0;
    }
}