using UnityEngine;

public class Slicer : Interactable
{
    public Transform pizzaPlatform;

    public RectTransform fullSlicerRect;
    public Quaternion defaultRotation;
    public GameObject cutPreview;
    private bool isFollowing = false;

    void Update()
    {
        if (isFollowing)
        {
            RotateToFollowPointer();
        }
    }

    public override void ClickAction()
    {
        // do nothing
    }

    public override void DownAction()
    {
        base.DownAction();
        BeginInteraction();
    }

    public override void UpAction()
    {
        base.UpAction();
        EndInteraction();
    }

    public override void BeginInteraction()
    {
        base.BeginInteraction();

        cutPreview.SetActive(true);
        isFollowing = true;
    }

    public override void EndInteraction()
    {
        CutPizza(fullSlicerRect.transform.right);

        fullSlicerRect.transform.rotation = defaultRotation;
        cutPreview.SetActive(false);
        isFollowing = false;

        base.EndInteraction();
    }

    private void RotateToFollowPointer()
    {
        Vector2 inputPos = GetInputPosition();
        Vector2 boardCenter = fullSlicerRect.anchoredPosition;

        Vector2 dir = GetDifference(inputPos, boardCenter).normalized;
        fullSlicerRect.transform.right = dir;
    }

    private void CutPizza(Vector2 dir)
    {
        Debug.Log("CUT!");
        if(pizzaPlatform.childCount <= 0) { return; }

        pizzaPlatform.GetChild(0).GetComponent<PizzaCutHandler>().CutPizza(dir);
    }
}
