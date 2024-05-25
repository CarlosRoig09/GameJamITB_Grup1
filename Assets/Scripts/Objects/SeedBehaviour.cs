using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SeedBehaviour : ObjectBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private GameObject plantPrefab;
    private float price;
    private Image img;
    protected override void Start()
    {
        img = GetComponent<Image>();
        originalColor = img.color;
        price = (plantPrefab.GetComponent<ObjectBehaviour>().GetSO() as PlantSO).GetPrice();
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
            var waterController = GameObjectLibrary.Instance.WaterController.GetComponent<WaterController>();
            if ((objectSO as PlantSO).Price <= waterController.WaterValue)
            {
                waterController.ModWater((objectSO as PlantSO).Price * -1);
                Use();
            }
        }
    }
    public GameObject GetPlant()
    {
        return plantPrefab;
    }
}
