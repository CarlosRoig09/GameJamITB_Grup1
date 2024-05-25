using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObjectBehaivour : ObjectBehaviour
{ 
    public override bool Check()
    {
        if (objectSO) {
            return base.Check();
        }
        return false;
    }
}
