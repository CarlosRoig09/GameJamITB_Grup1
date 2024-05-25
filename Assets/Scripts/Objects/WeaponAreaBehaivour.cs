using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAreaBehaivour : ObjectBehaviour
{
    public override void Use()
    {
        if (GameManager.Instance.selected != null && GameManager.Instance.selected.TryGetComponent(out WeaponBehaivour sb) && sb.Check())
        {
            sb.weaponSO.OnUse();
            GameManager.Instance.selected.UnSelect();
            GameManager.Instance.selected = null;
        }
    }
}
