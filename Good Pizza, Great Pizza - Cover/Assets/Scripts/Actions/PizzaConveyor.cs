using System.Collections;
using UnityEngine;

public class PizzaConveyor : DropPoint
{
    public float dropTime = 0.1f;
    public float moveTime = 2f;
    public RectTransform conveyorRect;
    public RectTransform endPoint;

    public override void DropPizzaHere(RectTransform pizza)
    {
        StartCoroutine(AddPizzaToConveyor(pizza));
    }

    IEnumerator AddPizzaToConveyor(RectTransform pizza)
    {
        pizza.SetParent(conveyorRect);

        // deactivate drag and drop on pizza
        pizza.gameObject.GetComponent<DragAndDrop>().enabled = false;

        // lerp to y
        Vector2 startPos = pizza.position;
        Vector2 endPos = conveyorRect.position;
        endPos.x = startPos.x;

        float dropTimer = 0f;
        while(dropTimer < dropTime)
        {
            yield return null;
            dropTimer += Time.deltaTime;

            Vector2 step = Vector2.Lerp(startPos, endPos, dropTimer / dropTime);
            pizza.position = step;
        }
        pizza.position = endPos;

        // animate remove peel
        pizza.gameObject.GetComponent<Animator>().SetTrigger("removePeel");

        // begin moving pizza on conveyor
        StartCoroutine(MoveDaPizza(pizza));
    }

    IEnumerator MoveDaPizza(RectTransform pizza)
    {
        // lerp position to end
        Vector2 startPos = pizza.anchoredPosition;
        Vector2 endPos = endPoint.anchoredPosition;

        float moveTimer = 0f;
        while (moveTimer < moveTime)
        {
            yield return null;
            moveTimer += Time.deltaTime;

            // move
            Vector2 step = Vector2.Lerp(startPos, endPos, moveTimer / moveTime);
            pizza.anchoredPosition = step;

            // cook

        }
        pizza.anchoredPosition = endPos;

        // reactivate drag drop
        pizza.gameObject.GetComponent<DragAndDrop>().enabled = true;

        // sfx ding
    }
}
