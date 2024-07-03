using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BigCannon : Enemy
{
    void Start()
    {
        // Llamar al m�todo Start de la clase base Enemy
        base.Start();

        // Duplicar FireRate
        FireRate *= 2;
    }

    // Sobrescribir el m�todo Shoot para ajustar el tama�o del proyectil
    public override void Shoot()
    {
        GameObject projectileInstance = Instantiate(Projectile, FirePosition.transform.position, Quaternion.identity);
        projectileInstance.transform.localScale *= (SizeMultiplier * 2); // Tama�o del proyectil multiplicado por 3
    }
}