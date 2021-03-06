using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI titleText;
    public GameObject startButton;
    public GameObject creditsButton;
    public GameObject creditsText;
    public GameObject creditsBackButton;
    public GameObject howtoplayButton;
    public GameObject playText;
    public GameObject playBackButton;

  

    public GameObject backgroundCanvas;
    public GameObject backgroundImage;
    public GameObject backgroundImageC;
    public GameObject canvas;
    public GameObject events;

    private bool warm;

    public GameObject HealthBar;
    public GameObject ColdBar;
    public HealthBar healthBar;
    public ColdBar coldBar;
    public GameObject survived;

    //Player
    public int maxHealth = 100;
    public int currentHealth;
    //public GameObject deathEffect;
    public GameObject coldImage;
    public int minCold = 100;
    public float currentCold;
    public bool isCold = false;
    public bool cold = false;
    public float coolSpeed = 1f;
    public NearFire fire;

    public GameObject endtext;
    public GameObject endbackbutton;
    public GameObject endscreen;

    public GameObject dialog;

    //[SerializeField, Range(0, 24)] private float TimeOfDay;

    public TextMeshProUGUI timeCounter;
    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;





    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(backgroundCanvas);
            DontDestroyOnLoad(events);
        }
        else
        {
            Destroy(gameObject);
            Destroy(canvas);
            Destroy(events);

        }
    }
    // Start is called before the first frame update
    void Start()
    {

        currentCold = 50f;

        timeCounter.text = "Time: 00:00";
        timerGoing = false;
        BeginTimer();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
        


    private void disableStartUI()
    {
        titleText.gameObject.SetActive(false);
        startButton.SetActive(false);
        creditsButton.SetActive(false);
        howtoplayButton.SetActive(false);

    }
    private void enableStartUI()
    {
        titleText.gameObject.SetActive(true);
        startButton.SetActive(true);
        creditsButton.SetActive(true);
        howtoplayButton.SetActive(true);
        backgroundCanvas.SetActive(true);

    }


    IEnumerator ColorLerp(Color endValue, float duration)
    {
        float time = 0;
        Image sprite = backgroundImage.GetComponent<Image>();
        Image sprite2 = backgroundImageC.GetComponent<Image>();
        Color startValue = sprite.color;
        Color startValue2 = sprite2.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            sprite2.color = Color.Lerp(startValue2, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endValue;
        sprite2.color = endValue;
    }

    IEnumerator LoadYourAsyncScene(bool lerp, string scene)
    {

        Debug.Log("Loading " + scene);
        if (scene == "MainMenu")
        {
            Start();

        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        if (lerp) { StartCoroutine(ColorLerp(new Color(0, 0, 0, 0), 2)); }
        else StartCoroutine(ColorLerp(new Color(1, 1, 1, 1), 2)); // reverse
    }
    public void StartButton()
    {
        disableStartUI();
        StartCoroutine(LoadYourAsyncScene(true, "Level"));
        HealthBar.SetActive(true);
        ColdBar.SetActive(true);
        survived.SetActive(true);
        setCold(100);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        coldBar.SetMinCold(100);


    }

    public void CreditsButton()
    {
        disableStartUI();
        creditsText.SetActive(true);
        creditsBackButton.SetActive(true);
    }
    public void HowToPlayButton()
    {
        disableStartUI();
        playText.SetActive(true);
        playBackButton.SetActive(true);
    }
    public void exitCredits()
    {
        creditsText.SetActive(false);
        creditsBackButton.SetActive(false);
        enableStartUI();
    }
    public void exithowto()
    {
        playText.SetActive(false);
        playBackButton.SetActive(false);
        enableStartUI();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            Debug.Log("dead");
        }
        else if (currentCold == 100)
        {
            if (!isCold)
            {
                Cold();
                isCold = true;
            }
            TakeDamage(10);
        }
        else
        {
            if (isCold)
            {
                NotCold();
                isCold = false;
            }
        }
    }

    // Enables or disables a chilled border
    IEnumerator ColdColorLerp(Color endValue, float duration)
    {
        float time = 0;
        Image sprite = coldImage.GetComponent<Image>();
        Color startValue = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endValue;
    }
    public void byFire(bool fire)
    {
        warm = fire;
    }

    public bool isWarm()
    {
        return warm;
    }
    public void Cold()
    {
        StartCoroutine(ColdColorLerp(new Color(1, 1, 1, 1), 2));
    }
    public void NotCold()
    {
        StartCoroutine(ColdColorLerp(new Color(0, 0, 0, 0), 2));
    }
    public void setCold(float cold)
    {
        currentCold = cold;
        coldBar.SetCold(currentCold);
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        StartCoroutine(LoadYourAsyncScene(true, "end"));
        endtext.SetActive(true);
        endbackbutton.SetActive(true);
        endscreen.SetActive(true);
        //Destroy(gameObject);
        Cursor.lockState = CursorLockMode.None;



    }
    public void endbackButton()
    {
        enableStartUI();
        endtext.SetActive(false);
        endbackbutton.SetActive(false);
        endscreen.SetActive(false);
        Debug.Log("endscreen back button is pressed");
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm' : 'ss");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
