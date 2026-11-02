using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : Interactable
{
    Vector2 followOffset;
    
    void Update()
    {
        if (currentInteraction == this)
        {
            FollowDrag();
        }
    }

    private void FollowDrag()
    {
        Vector2 inputPos = GetInputPosition();
        rectTransform.anchoredPosition = inputPos - followOffset;
    }

    public override void DownAction()
    {
        base.DownAction();

        Vector2 inputPos = GetInputPosition();
        followOffset = GetDifference(inputPos, rectTransform.anchoredPosition);
    }

    public override void UpAction()
    {
        base.UpAction();
    }
}
