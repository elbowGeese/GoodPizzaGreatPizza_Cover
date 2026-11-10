using System.Collections;
using UnityEngine;

public class DoughZone : MonoBehaviour
{
    public GameObject doughPrefab;
    public Transform[] zones;
    public Transform buildArea;
    public Transform countertop;

    public GameObject pizzaPrefab;
    public float timeToSpawnPizza = 2f;

    void Start()
    {
        ResetDoughZones();
    }

    private void ResetDoughZones()
    {
        foreach (Transform zone in zones)
        {
            Instantiate(doughPrefab, zone);
        }
    }

    private bool DoughZonesAreEmpty()
    {
        foreach(Transform zone in zones)
        {
            if(zone.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }

    public void TryUseDough(RectTransform dough)
    {
        if (BuildAreaIsEmpty())
        {
            // spawn pizza in build area
            StartCoroutine(SpawnPizza(dough));
        }
    }

    IEnumerator SpawnPizza(RectTransform dough)
    {
        Vector2 startPos = dough.parent.position;
        Vector2 endPos = buildArea.position;
        Vector2 startSize = dough.sizeDelta;//new Vector2(dough.rect.width, dough.rect.height);
        Vector2 endSize = new Vector2(475f, 475f);

        dough.SetParent(countertop);
        Transform pizza = Instantiate(pizzaPrefab, buildArea).transform;
        pizza.position = buildArea.position;

        float timer = 0f;
        while (timer < timeToSpawnPizza)
        {
            yield return null;
            timer += Time.deltaTime;

            // move position
            Vector2 posStep = Vector2.Lerp(startPos, endPos, timer / timeToSpawnPizza);
            dough.position = posStep;

            // change size
            Vector2 sizeStep = Vector2.Lerp(startSize, endSize, timer / timeToSpawnPizza);
            dough.sizeDelta = sizeStep;
        }

        Destroy(dough.gameObject);

        // if that was the last piece of dough then spawn more dough
        if (DoughZonesAreEmpty())
        {
            ResetDoughZones();
        }
    }

    private bool BuildAreaIsEmpty()
    {
        if (buildArea.childCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
