using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, ICloneable
{
    public float Speed = 1f;

    public virtual void Start()
    {
        // Obtener los l�mites de la c�mara
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Calcular una posici�n aleatoria en el l�mite superior de la c�mara
        float xPos = Random.Range(Camera.main.transform.position.x - cameraWidth / 2, Camera.main.transform.position.x + cameraWidth / 2);
        float yPos = Camera.main.transform.position.y + cameraHeight / 2;

        // Establecer la posici�n inicial del objeto
        transform.position = new Vector3(xPos, yPos, 0);
    }

    void Update()
    {
        // Mover el objeto hacia abajo
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        // Destruir el objeto si sale de los l�mites de la c�mara
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