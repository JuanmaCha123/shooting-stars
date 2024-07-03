using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BigCannon : Enemy
{
    void Start()
    {
        // Llamar al método Start de la clase base Enemy
        base.Start();

        // Duplicar FireRate
        FireRate *= 2;
    }

    // Sobrescribir el método Shoot para ajustar el tamaño del proyectil
    public override void Shoot()
    {
        GameObject projectileInstance = Instantiate(Projectile, FirePosition.transform.position, Quaternion.identity);
        projectileInstance.transform.localScale *= (SizeMultiplier * 2); // Tamaño del proyectil multiplicado por 3
    }
}