using UnityEngine;

public class DropPoint : MonoBehaviour 
{
    public virtual void DropPizzaHere(RectTransform pizza) { }

    public virtual RectTransform GetRect() { return GetComponent<RectTransform>(); }
}
