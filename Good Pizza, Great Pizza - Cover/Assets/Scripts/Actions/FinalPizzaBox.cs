using System.Collections;
using UnityEngine;

public class FinalPizzaBox : Interactable
{
    RectHelp rectHelp;
    Vector2 followOffset;

    Coroutine lerpBackRoutine;
    public float timeToLerpBack = 0.2f;

    private Vector2 previousHome;

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

    public override void ClickAction()
    {
        // override to do nothing
    }

    public override void DownAction()
    {
        base.DownAction();
        BeginInteraction();
    }

    public override void UpAction()
    {
        EndInteraction();
        base.UpAction();
    }

    public override void BeginInteraction()
    {
        base.BeginInteraction();

        previousHome = GetComponent<RectTransform>().anchoredPosition;

        if (lerpBackRoutine != null) { StopCoroutine(lerpBackRoutine); }

        Vector2 inputPos = GetInputPosition();
        followOffset = GetDifference(inputPos, rectTransform.anchoredPosition);
    }

    public override void EndInteraction()
    {
        base.EndInteraction();

        // look for customer
        CustomerBehaviour customer = FindAnyObjectByType<CustomerBehaviour>();
        if(customer != null)
        {
            if (RectHelp.IsOverlapping(GetComponent<RectTransform>(), customer.gameObject.GetComponent<RectTransform>()))
            {
                CompleteOrder(customer.gameObject);
            }
            else
            {
                // otherwise lerp it back
                lerpBackRoutine = StartCoroutine(LerpBackToHome());
            }
        }

    }

    IEnumerator LerpBackToHome()
    {
        RectTransform rect = GetComponent<RectTransform>();

        Vector2 startPos = rect.anchoredPosition;
        Vector2 endPos = previousHome;

        float timer = 0f;
        while (timer < timeToLerpBack)
        {
            yield return null;
            timer += Time.deltaTime;

            Vector2 step = Vector2.Lerp(startPos, endPos, timer / timeToLerpBack);
            rect.anchoredPosition = step;
        }

        rect.anchoredPosition = endPos;
    }

    private void CompleteOrder(GameObject customer)
    {
        Destroy(customer);
        GetComponent<RectTransform>().anchoredPosition = previousHome;
        gameObject.SetActive(false);
    }
}
