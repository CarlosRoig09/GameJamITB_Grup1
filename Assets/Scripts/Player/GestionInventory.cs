using System.Collections.Generic;
using UnityEngine;
public class GestionInventory : MonoBehaviour, IOnStartGame
{
    [SerializeField]
    private WeaponSO[] startWeapons;
    [SerializeField]
    private Inventory inventory;
    public Inventory Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }
    // Update is called once per frame
    void Update()
    {
    }

    public bool AddWeapon(WeaponSO weaponData)
    {
        if (inventory.LimitWeapons > inventory.Weapons.Count)
        {
            if (!IsInTheList(weaponData))
            {
                inventory.Weapons.Add(weaponData);
                //Show it in UI method;
                UIManager.Instance.ModifyWeaponQuantity(weaponData.Quantity, weaponData.Index);
                UIManager.Instance.ModifyWeaponIcon(weaponData.UIsprite, weaponData.Quantity);
                return true;
            }
            UIManager.Instance.ModifyWeaponQuantity(weaponData.Quantity, weaponData.Index);
        }
        return false;
    }

    public bool UseWeapon(WeaponSO weaponData)
    {
        if (weaponData.Quantity > 0) 
        {
            weaponData.Quantity -= 1;
            return true;
        }
        return false;
}
    private bool IsInTheList(WeaponSO weaponData)
    {
        foreach (var weapon in inventory.Weapons)
            if (weapon.Name == weaponData.Name)
            {
                if(weaponData.Quantity<weaponData.QuantityLimit)
                weapon.Quantity += 1;
                return true;
            }
        return false;
    }

    public void OnStartGame()
    {
        Inventory.Weapons = new List<WeaponSO>();
        foreach (var weapon in startWeapons)
        {
            weapon.Quantity = 0;
            AddWeapon(weapon);
        }
    }
}