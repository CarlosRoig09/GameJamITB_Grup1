using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightSprite : MonoBehaviour
{
    private Sprite daySprite;
    [SerializeField] private Sprite nightSprite;

    private void Start()
    {
        if (TryGetComponent(out SpriteRenderer sr)) daySprite = sr.sprite;
        if (TryGetComponent(out Image img)) daySprite = img.sprite;
    }
    private void Update()
    {
        switch (GameManager.Instance._currentCycle)
        {
            case DayCicle.Day:
                if (TryGetComponent(out SpriteRenderer sr)) sr.sprite = daySprite;
                if (TryGetComponent(out Image img)) img.sprite = daySprite;
                break;
            case DayCicle.Night:
                if (TryGetComponent(out SpriteRenderer sr2)) sr2.sprite = nightSprite;
                if (TryGetComponent(out Image img2)) img2.sprite = nightSprite;
                break;
            default:
                break;
        }
    }
}
