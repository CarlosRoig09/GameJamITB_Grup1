using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeedBehaviour : ObjectBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private GameObject plantPrefab;
    public void OnPointerEnter(PointerEventData eventData)
    {
        HighLight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HighLight(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Check())
        {
            Use();
        }
    }
    public GameObject GetPlant()
    {
        return plantPrefab;
    }
}
