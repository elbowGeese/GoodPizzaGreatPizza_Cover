using UnityEngine;

public class PizzaTopper : Interactable
{
    bool topping = false;

    void Update()
    {
        if (topping)
        {
            currentInteraction.GetComponent<Topping>().ApplyToppingToPizza(GetComponent<RectTransform>());
        }
    }

    public override void UpAction()
    {
        if (currentInteraction != null)
        {
            if (currentInteraction.gameObject.GetComponent<Topping>() != null)
            {
                EndInteraction();
            }
        }
    }

    public override void DownAction()
    {
        Debug.Log("PIZZA TOP 1");
        if (currentInteraction != null)
        {
            if (currentInteraction.gameObject.GetComponent<Topping>() != null)
            {
                Debug.Log("PIZZA TOP 2");
                BeginInteraction();
            }
        }
    }

    public override void ClickAction()
    {
        // do nothing
    }

    public override void BeginInteraction()
    {
        currentInteraction.GetComponent<Topping>().hasApplied = false;
        topping = true;
    }

    public override void EndInteraction()
    {
        topping = false;
    }
}
