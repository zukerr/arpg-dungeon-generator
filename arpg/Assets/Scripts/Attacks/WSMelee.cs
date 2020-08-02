using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSMelee : AWeaponScript
{
    //some damage calculations
    protected override void CalculateDamage()
    {
        damage = 10f;
    }
}
