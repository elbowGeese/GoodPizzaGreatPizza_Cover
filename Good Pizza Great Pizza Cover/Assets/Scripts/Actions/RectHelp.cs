using UnityEngine;

public class RectHelp : MonoBehaviour
{
    public DropPoint[] dropPoints;

    public static bool IsOverlapping(RectTransform rectA, RectTransform rectB)
    {
        if(rectA == null || rectB == null) return false;

        Vector3[] cornersA = new Vector3[4];
        Vector3[] cornersB = new Vector3[4];

        rectA.GetWorldCorners(cornersA);
        rectB.GetWorldCorners(cornersB);

        Rect rect1 = new Rect(cornersA[0], cornersA[2] - cornersA[0]);
        Rect rect2 = new Rect(cornersB[0], cornersB[2] - cornersB[0]);

        return rect1.Overlaps(rect2);
    }

    // check if pizza overlaps any of the stations
    public bool CheckPizzaOverlapsStation(RectTransform pizzaRect)
    {
        foreach (DropPoint dp in dropPoints)
        {
            if(IsOverlapping(pizzaRect, dp.GetRect()))
            {
                dp.DropPizzaHere(pizzaRect);
                return true;
            }
        }

        return false;
    }
}
