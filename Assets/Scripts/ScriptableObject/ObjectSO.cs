using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSO : ScriptableObject
{
    public string Name;
    public string Description;
    public float Price;
    public ObjectBehaviour behaviour;
    public Sprite UIsprite;
    public float SoldPrice;
    public float TimeGrow;
    public abstract void OnUse();
}
