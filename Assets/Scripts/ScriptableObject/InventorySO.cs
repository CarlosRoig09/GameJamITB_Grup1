using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public List<WeaponSO> Weapons;
    public float LimitWeapons;
}