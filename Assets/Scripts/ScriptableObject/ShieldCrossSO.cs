using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldCrossSO", menuName = "ObjectsSO/WeaponsSO/ShieldCrossSO")]
public class ShieldCrossSO : WeaponSO
{
    public override void OnUse()
    {
   
        Debug.Log("Cross");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(Weapon,mousePos,Quaternion.identity);
    }
}
