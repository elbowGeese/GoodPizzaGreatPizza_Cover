using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Interactable currentInteraction = null;
    public bool equipOnDown = false;

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
        if (currentInteraction == null && !equipOnDown) { currentInteraction = this; }
        if (currentInteraction != this) return;
        ClickAction();
    }

    public virtual void ClickAction()
    {

    }

    // ON MOUSE DOWN
    public void OnPointerDown(PointerEventData eventData)
    {
        if(currentInteraction == null && equipOnDown) { currentInteraction = this; }
        if (currentInteraction != this) return;
        DownAction();
    }

    public virtual void DownAction()
    {

    }

    // ON MOUSE UP
    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentInteraction != this) return;
        if (equipOnDown) { currentInteraction = null; }
        UpAction();
    }

    public virtual void UpAction()
    {
        
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
