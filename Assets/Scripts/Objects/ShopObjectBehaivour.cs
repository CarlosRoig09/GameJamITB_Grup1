using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObjectBehaivour : ObjectBehaviour
{
    public override bool Check()
    {
        var waterController = GameObjectLibrary.Instance.WaterController.GetComponent<WaterController>();
        if (objectSO.Price<=waterController.WaterValue) {
            waterController.ModWater(objectSO.Price*-1);
            return base.Check();
        }
        return false;
    }
}
