using UnityEngine;

public class Trash : DropPoint
{
    public override void DropPizzaHere(RectTransform pizza)
    {
        Destroy(pizza.gameObject);
    }
}
