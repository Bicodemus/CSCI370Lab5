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
    public GameObject canvas;
    public GameObject events;


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
    IEnumerator LoadYourAsyncScene(string scene)
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

    }
    public void StartButton()
    {
        disableStartUI();
        backgroundCanvas.SetActive(false);
        StartCoroutine(LoadYourAsyncScene("Level"));


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
