using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactable : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Interactable currentInteraction = null;

    public Canvas canvas;
    public RectTransform rectTransform;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    // ON CLICK
    public void OnPointerClick(PointerEventData eventData)
    {
        ClickAction();
    }

    // ON MOUSE DOWN
    public void OnPointerDown(PointerEventData eventData)
    {
        DownAction();
    }

    // ON MOUSE UP
    public void OnPointerUp(PointerEventData eventData)
    {
        UpAction();
    }

    public virtual void ClickAction()
    {
        if (currentInteraction != null)
        {
            currentInteraction.EndInteraction();
        }

        currentInteraction = this;
    }

    public virtual void DownAction()
    {
        // stop interacting with current 
        if (currentInteraction != null) 
        {
            currentInteraction.EndInteraction(); 
        }
        
        currentInteraction = this;
    }

    public virtual void UpAction()
    {
        if (currentInteraction != this) return;

        currentInteraction = null;
    }

    public virtual void BeginInteraction()
    {
        GameObject.FindWithTag("ScrollZone").GetComponent<ScrollRect>().enabled = false;
    }

    public virtual void EndInteraction()
    {
        GameObject.FindWithTag("ScrollZone").GetComponent<ScrollRect>().enabled = true;
    }

    // UTILITY
    public Vector2 GetInputPosition()
    {
        Vector2 screenPoint = Vector2.zero;

        if(Mouse.current != null)
        {
            screenPoint = Mouse.current.position.ReadValue();
        }

        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
        {
            screenPoint = Touchscreen.current.touches[0].position.ReadValue();
        }

        Vector2 inputPos = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPoint,
            canvas.worldCamera,
            out inputPos
        );

        return inputPos;
    }

    public Vector2 GetDifference(Vector2 a, Vector2 b)
    {
        float x = a.x - b.x;
        float y = a.y - b.y;
        return new Vector2(x, y);
    }
}
