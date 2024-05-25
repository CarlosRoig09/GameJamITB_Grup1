using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopObjectBehaivour : ObjectBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Image image;

    private void Awake()
    {
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = objectSO.Price.ToString();
    }

    protected override void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    protected override void Update()
    {
        image.color = selected ? Color.red : highlighted ? Color.yellow : originalColor;
    }
    public override bool Check()
    {
        var waterController = GameObjectLibrary.Instance.WaterController.GetComponent<WaterController>();
        if (objectSO.Price<=waterController.WaterValue) {
            waterController.ModWater(objectSO.Price*-1);
            return base.Check();
        }
        return false;
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
        if(Check())
        {
            Use();
        }
    }
}
