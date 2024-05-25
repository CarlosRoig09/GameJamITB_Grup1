using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectBehaviour : MonoBehaviour, IInteractable
{
    private SpriteRenderer sr;
    public Color originalColor;
    public bool isInteractable;
    public bool selected;
    public bool highlighted;
    [SerializeField]
    protected ObjectSO objectSO;
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    protected virtual void Update()
    {
        sr.color = selected ? Color.magenta : highlighted ? Color.yellow : originalColor;
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

    public float GetPrice()
    {
        return objectSO.price;
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
            // Si se cumplen las condiciones del Check(), se ejecuta el uso, que puede ser tanto dejar seleccionado el slot cambiando el lastSelectable c�mo comprar un arma llamando al Inventory y m�s
            if (GameManager.Instance.selected != null) GameManager.Instance.selected.UnSelect();
            selected = true;
            GameManager.Instance.selected = this;
            return;
        }
    }
    public void UnSelect()
    {
        selected = false;
    }
}
