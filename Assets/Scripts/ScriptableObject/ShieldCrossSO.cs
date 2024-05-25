using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldCrossSO", menuName = "ObjectsSO/WeaponsSO/ShieldCrossSO")]
public class ShieldCrossSO : WeaponSO
{
    public override void OnUse()
    {
        Debug.Log("Cross");

    }
}
