using UnityEngine;

public class Dough : MonoBehaviour
{
    public void UseMe()
    {
        FindAnyObjectByType<DoughZone>().TryUseDough(GetComponent<RectTransform>());
    }
}
