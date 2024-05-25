using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectBehaviour : MonoBehaviour, IInteractable
{
    private Color originalColor;
    public bool isInteractable;
    private bool selected;
    private bool highlighted;
    private void Start()
    {
        GetOriginalColor();
    }
    private void Update()
    {
        UpdateColor();
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
    private void UpdateColor()
    {
        if (TryGetComponent(out SpriteRenderer sr))sr.color = selected ? Color.magenta : highlighted ? Color.yellow : originalColor;
        if (TryGetComponent(out Image img)) img.color = selected ? Color.magenta : highlighted ? Color.yellow : originalColor;
    }
    private void GetOriginalColor()
    {
        if (TryGetComponent(out SpriteRenderer sr)) originalColor = sr.color;
        if (TryGetComponent(out Image img)) originalColor = img.color;
    }

    // IInteractable
    public virtual bool Check()
    {
        // Condiciones necesarias para el Use (Ej: Para seleccionar una semilla necesitas el suficiente dinero para pagarla.)
        return true;
    }

    public void HighLight(bool light)
    {
        highlighted = light;
    }

    public virtual void Use()
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
