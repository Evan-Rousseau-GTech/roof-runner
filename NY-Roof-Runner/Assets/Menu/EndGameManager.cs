using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{

    public static float timer;
    public static float highScore;
    public Button Return;
    public Button Replay;
    [SerializeField] Text TimerText;
    [SerializeField] Text SeedText;
    [SerializeField] Text HighScoreText;
    // Start is called before the first frame update
    void Start()
    {
        Return.onClick.AddListener(ReturnMainMenu);
        Replay.onClick.AddListener(ReplaySameSeed);

        string minutes = ((int)timer / 60).ToString();
        string secondes = ((int)(timer % 60)).ToString();
        if (secondes.Length == 1) secondes = "0" + secondes;
        string centisecondes = ((int)((timer % 1) * 100)).ToString();
        if (centisecondes.Length == 1) centisecondes = "0" + centisecondes;
        TimerText.text = "Your score : " + minutes + ":" + secondes + ":" + centisecondes;

        minutes = ((int)highScore / 60).ToString();
        secondes = ((int)(highScore % 60)).ToString();
        if (secondes.Length == 1) secondes = "0" + secondes;
        centisecondes = ((int)((highScore % 1) * 100)).ToString();
        if (centisecondes.Length == 1) centisecondes = "0" + centisecondes;
        HighScoreText.text = "HIGHSCORE : " + minutes + ":" + secondes + ":" + centisecondes;
        SeedText.text = "Seed : " + Seed.seed.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void LoadEndGameScene()
    {
        SceneManager.LoadScene("EndGameScene");
    }

    void ReturnMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    void ReplaySameSeed()
    {
        GameManager.startTime = Time.realtimeSinceStartup;
        SceneManager.LoadScene("GameScene");
    }

}
