using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponBehaivour : ObjectBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public WeaponSO weaponSO;
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
}
