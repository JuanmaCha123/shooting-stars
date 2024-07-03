using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, ICloneable
{
    public float Speed = 1f;

    public virtual void Start()
    {
        // Obtener los límites de la cámara
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Calcular una posición aleatoria en el límite superior de la cámara
        float xPos = Random.Range(Camera.main.transform.position.x - cameraWidth / 2, Camera.main.transform.position.x + cameraWidth / 2);
        float yPos = Camera.main.transform.position.y + cameraHeight / 2;

        // Establecer la posición inicial del objeto
        transform.position = new Vector3(xPos, yPos, 0);
    }

    void Update()
    {
        // Mover el objeto hacia abajo
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        // Destruir el objeto si sale de los límites de la cámara
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }

    public ICloneable Clone()
    {
        return Instantiate(this);
    }
}