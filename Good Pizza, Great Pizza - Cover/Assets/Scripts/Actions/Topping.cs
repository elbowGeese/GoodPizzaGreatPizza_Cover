using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Topping : Interactable
{
    public Animator toppingAnim;
    public GameObject toppingPrefab;
    public Vector2 toppingOffset;

    public bool applyOncePerTouch;
    public bool hasApplied = false;
    public float continuousMinApplyDist = 0.1f;
    private Vector2 lastFramePos;

    public override void ClickAction()
    {
        base.ClickAction();
        BeginInteraction();
    }

    public override void UpAction()
    {
        // override to do nothing
    }

    public override void DownAction()
    {
        // override to do nothing
    }

    public override void BeginInteraction()
    {
        base.BeginInteraction();

        toppingAnim.SetBool("isEquipped", true);
    }

    public override void EndInteraction()
    {
        base.EndInteraction();

        toppingAnim.SetBool("isEquipped", false);
    }

    //public Transform RayToPizza()
    //{
    //    Vector2 inputPos = base.GetInputPosition();
    //    Debug.DrawLine(GetComponent<RectTransform>().position, inputPos, Color.green);
    //    // convert input position to pointerdata
    //    PointerEventData pointerData = new PointerEventData(EventSystem.current)
    //    {
    //        position = inputPos
    //    };

    //    // raycast
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    canvas.GetComponent<GraphicRaycaster>().Raycast(pointerData, results);

    //    Debug.Log(results.Count + " hits");
    //    if (results.Count > 0)
    //    {
    //        foreach (RaycastResult result in results)
    //        {
    //            // if its a pizza
    //            if (result.gameObject.CompareTag("PizzaCrust"))
    //            {
    //                Debug.Log("In da pizza");
    //                return result.gameObject.transform;
    //            }
    //        }
    //    }

    //    Debug.Log("No pizza :(");
    //    return null;
    //}

    public void ApplyToppingToPizza(RectTransform pizza)
    {
        Debug.Log("APPLYING TOPPINGS...");
        Vector2 currentInputPos = base.GetInputPosition();

        // check if the topping is do once only
        if (applyOncePerTouch && hasApplied)
        {
            return;
        }

        // check if input pos moved enough from last frame
        if (!applyOncePerTouch)
        {
            if(Vector2.Distance(lastFramePos, currentInputPos) < continuousMinApplyDist)
            {
                return;
            }
        }

        // spawn toppings
        RectTransform topping = Instantiate(toppingPrefab, pizza).GetComponent<RectTransform>();
        //topping.SetParent(pizza);
        topping.anchoredPosition = currentInputPos + toppingOffset;

        // track do once and previous pos
        hasApplied = true;
        lastFramePos = currentInputPos;
    }
}
