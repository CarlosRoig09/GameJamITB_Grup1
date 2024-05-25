using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundBehaviour : ObjectBehaviour
{
    override protected void Update()
    {
        base.Update();
        GetComponent<BoxCollider2D>().enabled = GetComponentsInChildren<Transform>().ToList().Count == 1;
    }
    public override void Use()
    {
        if (GameManager.Instance.selected != null && GameManager.Instance.selected.TryGetComponent(out SeedBehaviour sb) && sb.Check())
        {
            var plant = Instantiate(sb.GetPlant(), gameObject.transform);
            plant.tag = "Plant";
        }
    }
}
