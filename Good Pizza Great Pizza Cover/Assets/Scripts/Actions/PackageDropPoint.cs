using System.Collections;
using UnityEngine;

public class PackageDropPoint : DropPoint
{
    public float dropTime = 0.1f;
    public Animator anim;

    public override void DropPizzaHere(RectTransform pizza)
    {
        StartCoroutine(AddPizzaToCuttingBoard(pizza));
    }

    IEnumerator AddPizzaToCuttingBoard(RectTransform pizza)
    {
        pizza.SetParent(GetRect());

        // lerp to y
        Vector2 startPos = pizza.anchoredPosition;
        Vector2 endPos = GetRect().anchoredPosition;

        float dropTimer = 0f;
        while (dropTimer < dropTime)
        {
            yield return null;
            dropTimer += Time.deltaTime;

            Vector2 step = Vector2.Lerp(startPos, endPos, dropTimer / dropTime);
            pizza.anchoredPosition = step;
        }
        pizza.anchoredPosition = endPos;

        anim.SetTrigger("boxUp");

        yield return new WaitForSeconds(1f);

        // clear box contents
        Destroy(pizza.gameObject);
    }
}
