using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundBehaviour : ObjectBehaviour
{
    public override void Use()
    {
        if (GameManager.Instance.selected != null && GetComponentsInChildren<Transform>().ToList().Count == 1 && GameManager.Instance.selected.TryGetComponent(out SeedBehaviour sb) && sb.Check())
        {
            var plant = Instantiate(sb.GetPlant(), gameObject.transform);
            plant.tag = "Plant";
        }
    }
}
