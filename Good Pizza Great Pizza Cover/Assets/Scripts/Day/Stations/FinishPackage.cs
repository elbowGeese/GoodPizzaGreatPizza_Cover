using UnityEngine;

public class FinishPackage : MonoBehaviour
{
    public GameObject customerBox;
    public Animator kitchenAnim;

    public void GoBackToCounterWithPackage()
    {
        // enable box on customer counter
        customerBox.SetActive(true);

        // animate out kitchen
        kitchenAnim.SetBool("isOpen", false);

    }
}
