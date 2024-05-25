using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundBehaviour : ObjectBehaviour
{
    public override void Use()
    {
        if (GameManager.Instance.selected != null && GameManager.Instance.selected.TryGetComponent(out SeedBehaviour sb) && sb.Check())
        {
            //var plant = Instantiate(sb.GetPlant(), gameObject.transform);
            //plant.tag = "Plant";
        }
    }
}
