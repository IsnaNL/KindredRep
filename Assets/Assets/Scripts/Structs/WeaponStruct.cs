using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponStruct 
{
    public int id;
    public float damage;
    public string naming;

    public WeaponStruct(int v1, float v2, string v3)
    {
        id = v1;
        damage = v2;
        naming = v3;
    }

    
}
