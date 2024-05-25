using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Use();
    public bool Check();
    public void HighLight(bool light);
}
