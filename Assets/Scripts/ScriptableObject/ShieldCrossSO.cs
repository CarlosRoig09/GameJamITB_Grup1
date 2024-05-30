using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldCrossSO", menuName = "ObjectsSO/WeaponsSO/ShieldCrossSO")]
public class ShieldCrossSO : WeaponSO
{
    public override void OnUse()
    {

        var waterController = GameObjectLibrary.Instance.WaterController.GetComponent<WaterController>();
        if (Price <= waterController.WaterValue)
        {
            waterController.ModWater(Price * -1);
            Debug.Log("Cross");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(Weapon, mousePos, Quaternion.identity);
        }
    }
}
