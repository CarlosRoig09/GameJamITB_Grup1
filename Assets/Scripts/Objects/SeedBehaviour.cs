using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SeedBehaviour : ObjectBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private float price;
    private Image img;
    protected override void Start()
    {
        img = GetComponent<Image>();
        originalColor = img.color;
        price = ((objectSO as PlantSO).plantPrefab.GetComponent<ObjectBehaviour>() as PlantBehaviour).GetPrice();
    }
    protected override void Update()
    {
        img.color = selected ? Color.magenta : highlighted ? Color.yellow : originalColor;
    }
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
