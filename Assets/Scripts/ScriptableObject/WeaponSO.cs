using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSO : ObjectSO
{
    public int Index;
    public int Quantity;
    public int QuantityLimit;
    public float Countdown;
    public GameObject Weapon;
}
