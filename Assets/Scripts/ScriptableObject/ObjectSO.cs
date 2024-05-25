using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSO : ScriptableObject
{
    [SerializeField]
    private string Name;
    [SerializeField]
    private string Description;
    [SerializeField]
    public float price;
    public ObjectBehaviour behaviour;
    public abstract void OnUse();
}
