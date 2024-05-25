using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopObjectSO", menuName = "ObjectsSO/ShopObjectSO")]
public class ShopObjectSO : ObjectSO
{
    public ObjectSO Item;
    public override void OnUse()
    {
        Item.OnUse();
    }
}
