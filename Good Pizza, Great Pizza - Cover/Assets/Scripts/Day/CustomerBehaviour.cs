using System.Collections;
using TMPro;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    private DayManager dm;

    private Animator anim;
    public GameObject okayButton;
    public TMP_Text dialogBox;
    public string dialog;
    public float pizzaPrice;
    public float typeSpeed = 5f;

    [Header("Patience")]
    public float patienceDecay = 1f;
    private bool hasOrdered = false;
    private float patienceLeft = 100f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdatePatience();
    }

    public void BeginDialog()
    {
        StartCoroutine(PrintDialog());
    }

    IEnumerator PrintDialog()
    {
        // reset
        dialogBox.text = "";
        okayButton.gameObject.SetActive(false);

        // turn string dialog into character array
        char[] characters = dialog.ToCharArray();

        // print each character to dialog box
        foreach(char c in characters)
        {
            dialogBox.text += c;
            yield return new WaitForSeconds(typeSpeed * Time.deltaTime);
        }

        // make sure dialog is the same
        dialogBox.text = dialog;

        // enable button
        okayButton.gameObject.SetActive(true);
    }

    public void OkayButton()
    {
        dm = FindAnyObjectByType<DayManager>();

        // close dialog box
        dialogBox.transform.parent.gameObject.SetActive(false);
        hasOrdered = true;

        // let day manager handle everything outside customer
        dm.ConfirmOrder(pizzaPrice);
    }

    private void UpdatePatience()
    {
        if (hasOrdered)
        {
            patienceLeft -= Time.deltaTime * patienceDecay;
            if(patienceLeft < 0f)
            {
                patienceLeft = 0f;
            }

            dm.UpdatePatience(patienceLeft);
        }
    }

    public void DestroyFromAnim()
    {
        Destroy(gameObject);
    }
}
