using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand
{
    private Weapon weapon;

    public ShootCommand(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void Execute()
    {
        weapon.Shoot();
    }
}
