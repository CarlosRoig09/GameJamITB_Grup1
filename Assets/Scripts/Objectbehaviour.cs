using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBehaviour : MonoBehaviour, IInteractable
{
    private SpriteRenderer sr;
    private Color originalColor;
    public bool isInteractable;
    public bool selected;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    private void Update()
    {
        sr.color = selected ? Color.red : originalColor;
    }

    // Mouse Interactions
    private void OnMouseEnter()
    {
        HighLight(true);
    }
    private void OnMouseExit()
    {
        HighLight(false);
    }
    private void OnMouseDown()
    {
        if (Check())
        {
            Use();
        }
    }

    // IInteractable
    public bool Check()
    {
        // Condiciones necesarias para el Use (Ej: Para seleccionar una semilla necesitas el suficiente dinero para pagarla.)
        return true;
    }

    public void HighLight(bool light)
    {
        if(TryGetComponent(out SpriteRenderer sr)) sr.color = light ? Color.yellow : originalColor;
    }

    public void Use()
    {
        if (Check())
        {
            // Si se cumplen las condiciones del Check(), se ejecuta el uso, que puede ser tanto dejar seleccionado el slot cambiando el lastSelectable cómo comprar un arma llamando al Inventory y más
            if (ClickManager.instance.selected != null && ClickManager.instance.selected.TryGetComponent(out ObjectBehaviour ob)) ob.UnSelect();
            selected = true;
            ClickManager.instance.selected = gameObject;
            return;
        }
    }
    public void UnSelect()
    {
        selected = false;
    }
}
