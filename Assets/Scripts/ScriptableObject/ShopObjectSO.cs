using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObjectSO : ObjectSO
{
    public ObjectSO Item;
    public override void OnUse()
    {
        Item.OnUse();
    }
}
