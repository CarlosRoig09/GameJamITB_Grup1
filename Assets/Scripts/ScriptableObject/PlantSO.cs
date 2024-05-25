using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantSO", menuName = "ObjectsSO/PlantSO")]
public class PlantSO : ObjectSO
{
    [Range(1, 5)]
    public int buyPrice;
    [Range(1, 5)]
    public int time;
    [Range(1, 5)]
    public int waterNeed;
    [Range(1, 5)]
    public int sellPrice;
    public GameObject plantPrefab;

    public override void OnUse()
    {
        throw new System.NotImplementedException();
    }
    public float GetPrice()
    {
        return buyPrice * 5;
    }
}
