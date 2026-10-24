using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Interactable currentInteraction;

    // ON CLICK
    public void OnPointerClick(PointerEventData eventData)
    {
        if(currentInteraction != this) return;
    }

    // ON MOUSE DOWN
    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentInteraction != this) return;
    }

    // ON MOUSE UP
    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentInteraction != this) return;
    }
}
