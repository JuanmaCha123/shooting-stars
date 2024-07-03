using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Tank : Enemy
{
    void Start()
    {
        // Llamar al método Start de la clase base Enemy
        base.Start();

        // Dividir Speed por 2
        Speed /= 2;

        // Multiplicar FireRate por 2
        FireRate *= 2;

        // Multiplicar Health por 2
        Health *= 2;
    }
}