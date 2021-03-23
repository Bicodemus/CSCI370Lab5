using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject Player;


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

}
