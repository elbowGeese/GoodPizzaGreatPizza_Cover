using UnityEngine;
using UnityEngine.UI;

public class PizzaCutHandler : MonoBehaviour
{
    public GameObject cutPrefab;
    public Transform cutsCanvas;

    public RectTransform originalPizza;
    public GameObject primaryPizzaVisual;
    public Image condensedPizza;
    public RenderTexture pizzaTexture;

    public Animator anim;
    private bool hasCondensedPizza = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        //pizzaCam.gameObject.SetActive(false);
        primaryPizzaVisual.SetActive(false);
    }

    //IEnumerator ScreenshotPizza()
    //{
    //    yield return new WaitForEndOfFrame();

    //    // turn on cam
    //    pizzaCam.gameObject.SetActive(true);

    //    // make temp render texture
    //    RenderTexture rt = new RenderTexture(500, 500, 24);
    //    pizzaCam.targetTexture = rt;

    //    // force render
    //    //pizzaCam.Render();
    //    primaryPizzaVisual.SetActive(true);

    //    //
    //    //RenderTexture oldActive = RenderTexture.active;
    //    RenderTexture.active = rt;

    //    // save the render to a texture 2d
    //    Texture2D screenshot = new Texture2D(500, 500, TextureFormat.ARGB32, false);
    //    Rect rect = new Rect(0, 0, 500, 500);
    //    screenshot.ReadPixels(rect, 0, 0);
    //    screenshot.Apply();

    //    //condensedPizza.texture = screenshot;

    //    // reset cam
    //    //pizzaCam.targetTexture = null;
    //    //RenderTexture.active = oldActive;

    //    // save screenshot to png
    //    byte[] bytes = screenshot.EncodeToPNG();
    //    System.IO.File.WriteAllBytes(Application.dataPath + "/currentPizzaBaby.png", bytes);

    //    // deactivate original pizza visual and cam
    //    //originalPizza.SetActive(false);
    //    //pizzaCam.gameObject.SetActive(false);

    //    hasCondensedPizza = true;
    //}

    public void CutPizza(Vector2 dir)
    {
        // disable the animator so it doesnt break
        if(anim.enabled == true) { anim.enabled = false; }

        if (!hasCondensedPizza) { CondensePizza(); }
        
        // spawn new cut
        Transform cut = Instantiate(cutPrefab, cutsCanvas).transform;
        // set cut to direction
        cut.right = dir;
    }

    private void CondensePizza()
    {
        RectTransform pizzaCanvas = GameObject.FindWithTag("PizzaCanvas").GetComponent<RectTransform>();

        originalPizza.SetParent(pizzaCanvas, false);
        originalPizza.anchoredPosition = Vector2.zero;

        // EXTREMELY IMPORTANT DONT DELETE
        GameObject.FindWithTag("PizzaCam").GetComponent<Camera>().Render();

        primaryPizzaVisual.SetActive(true);

        Sprite pizzaSprite = ConvertTextureToSprite(pizzaTexture);
        condensedPizza.sprite = pizzaSprite;

        hasCondensedPizza = true;
    }

    public Sprite ConvertTextureToSprite(RenderTexture renderTexture)
    {
        if (renderTexture == null)
        {
            Debug.LogError("RenderTexture is null!");
            return null;
        }

        RenderTexture previousActive = RenderTexture.active;
        RenderTexture.active = renderTexture;

        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = previousActive;

        Rect spriteRect = new Rect(0f, 0f, texture2D.width, texture2D.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f); // Center pivot

        return Sprite.Create(texture2D, spriteRect, pivot, 100.0f);
    }
}
