using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Nimble : Enemy
{
    void Start()
    {
        // Llamar al método Start de la clase base Enemy
        base.Start();

        // Duplicar la variable Speed
        Speed *= 2;

        // Dividir FireRate a la mitad
        FireRate /= 2;

        // Reducir Health a 1
        Health = 1;
    }

    // Sobrescribir el método Shoot para ajustar el tamaño del proyectil
    public override void Shoot()
    {
        GameObject projectileInstance = Instantiate(Projectile, FirePosition.transform.position, Quaternion.identity);
        projectileInstance.transform.localScale *= (SizeMultiplier * 0.5f); // Mitad del tamaño
    }
}