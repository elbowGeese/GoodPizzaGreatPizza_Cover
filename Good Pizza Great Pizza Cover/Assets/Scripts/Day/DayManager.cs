using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    [Header("Daily Timer")]
    public TMP_Text textTimer;
    public Slider sliderTimer;
    public Image sliderTimerFill;
    public Color dayStartColor, dayEndColor;
    public float timeInDay = 60f;
    private float timer;
    private Coroutine blinkCoroutine;
    public float blinkTime = 2f;

    [Header("Customers")]
    public GameObject customerPrefab;
    public Transform customerSpawnPoint;

    [Header("Patience")]
    public TMP_Text patienceText;
    public Image patienceImage;
    public TMP_Text patienceFace;

    [System.Serializable]
    public class PatienceSetting 
    {
        public float minPercent;
        public string face;
        public Color faceColor;
    }
    public PatienceSetting[] patienceSettings;

    [Header("Order Reciept")]
    public GameObject orderReciept;
    public GameObject orderCloseup;
    public TMP_Text orderRecieptText;

    [Header("Register")]
    public Animator registerAnim;
    public TMP_Text registerChargeText;

    [Header("Money Management")]
    public TMP_Text moneyText;
    public float moneyAmount { get; private set; }

    [Header("Build Counter")]
    public GameObject buildCounter;
    public Animator buildCounterAnim;


    void Start()
    {
        moneyAmount = PlayerPrefs.GetFloat("moneyAmount");
        UpdateMoneyUI();

        timer = 0f;
        buildCounter.SetActive(false);
        orderCloseup.SetActive(false);
        orderReciept.SetActive(false);
    }

    void Update()
    {
        UpdateTimer();
        TrySpawnCustomer();
    }

    // hours of operation 12pm - 9pm
    // displays time in 15min intervals
    // each hour is 4 chunks x 9 hours = 28 possible text updates
    private void UpdateTimer()
    {
        timer += Time.deltaTime;

        float step = timer / timeInDay;

        // text timer
        float clock = Mathf.Lerp(0f, 9f, step);
        int hour = Mathf.FloorToInt(clock);
        if(hour == 0) { hour = 12; }
        int minute = Mathf.FloorToInt((clock % 1f) * 100f);
        minute = (int) Mathf.Lerp(0f, 60f, (float) minute / 100f);
        int minDivisibleBy15 = (minute / 15) * 15;
        textTimer.text = $"{hour}:{minDivisibleBy15:D2}PM";

        if (hour == 8 && blinkCoroutine == null) 
        {
            blinkCoroutine = StartCoroutine(BlinkTimerTextRoutine());
        }

        if(timer >= timeInDay && blinkCoroutine != null) 
        {
            StopCoroutine(blinkCoroutine);
            textTimer.color = Color.red; 
            // play closing sfx
        }

        // slider timer
        float sliderValue = 1f - Mathf.Clamp01(step);
        sliderTimer.value = sliderValue;
        sliderTimerFill.color = Color.Lerp(dayStartColor, dayEndColor, step);
    }

    IEnumerator BlinkTimerTextRoutine()
    {
        while (true)
        {
            textTimer.color = Color.red;
            yield return new WaitForSeconds(blinkTime);
            textTimer.color = Color.white;
            yield return new WaitForSeconds(blinkTime);
        }
    }

    private void TrySpawnCustomer()
    {
        // if time isnt out
        if (timer < timeInDay)
        {
            // if there are no other customers in scene
            if(FindAnyObjectByType<CustomerBehaviour>() == null)
            {
                // spawn a customer
                Instantiate(customerPrefab, customerSpawnPoint);
            }
        }
    }

    public void UpdatePatience(float patienceLeft)
    {
        patienceText.text = $"{(int)patienceLeft}%";

        int index = 0;
        if(patienceLeft < patienceSettings[0].minPercent)
        {
            index = 0;
        }
        else if(patienceLeft < patienceSettings[1].minPercent)
        {
            index = 1;
        }
        else
        {
            index = 2;
        }

        patienceImage.color = patienceSettings[index].faceColor;
        patienceFace.text = patienceSettings[index].face;
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = $"{moneyAmount:F2}";
    }

    // called by customer button
    public void ConfirmOrder(float chargeAmount)
    {
        // add order reciept
        orderReciept.SetActive(true);

        // charge money
        registerChargeText.text = $"{chargeAmount:F2}";
        registerAnim.SetTrigger("charge");
        moneyAmount += chargeAmount;
        UpdateMoneyUI();

        // play sfx

        // open build counter
        buildCounter.SetActive(true);
        buildCounterAnim.SetBool("isOpen", true);
    }

    public void OpenReciept()
    {
        orderCloseup.SetActive(true);

        CustomerBehaviour currentCustomer = FindAnyObjectByType<CustomerBehaviour>();
        if (currentCustomer != null)
        {
            orderRecieptText.text = currentCustomer.dialog;
        }
    }

    public void CloseReciept()
    {
        orderCloseup.SetActive(false);
    }
}
