using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static int GameState = 0; //0 = pas commencer, 1 = vient de commencer,2 = partie fini,3 = fermer le jeu
    public static float startTime = 0;
    [SerializeField] int IDcheckPoint = 1;
    [SerializeField] List<CheckPoint> GameCheckpointList = new List<CheckPoint>();
    [SerializeField] Character Player;

    [SerializeField] Text centerText;
    [SerializeField] Text timer;
    [SerializeField] Text seedText;
    [SerializeField] Text highScoreText;

    float timeSinceStart = 0;
    float highscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        highscore = SaveManager.getHighscore(Seed.seed);
        string minutes = ((int)highscore / 60).ToString();
        string secondes = ((int)(highscore % 60)).ToString();
        if (secondes.Length == 1) secondes = "0" + secondes;
        string centisecondes = ((int)((highscore % 1) * 100)).ToString();
        if (centisecondes.Length == 1) centisecondes = "0" + centisecondes;
        highScoreText.text = "HIGHSCORE: " + minutes + ":" + secondes + ":" + centisecondes;
        seedText.text = "SEED: " + Seed.seed.ToString();
        foreach (CheckPoint checkPoint in GameCheckpointList)
        {
            checkPoint.idCheckpoint = IDcheckPoint;
            IDcheckPoint = IDcheckPoint + 1;
        }
        //createCheckpoint();
    }

    public void FixedUpdate()
    {
       StartCoroutine(CheckEndGame());
    }
    // Update is called once per frame
    void Update()
    {
        timeSinceStart = Time.realtimeSinceStartup - startTime;
        int countdown = 3 - (int)timeSinceStart;
        if (GameManager.GameState == 0 || countdown == 0 || countdown == -1)
        {
            if(countdown > 0)
            {
                if(countdown < 4) centerText.text = countdown.ToString();
            }
            else if(countdown == 0)
            {
                centerText.text = "<color=#84EB9E>GO !</color>";
                float time = timeSinceStart - 3;
                string minutes = ((int)time / 60).ToString();
                string secondes = ((int)(time % 60)).ToString();
                if (secondes.Length == 1) secondes = "0" + secondes;
                string centisecondes = ((int)((time % 1) * 100)).ToString();
                if (centisecondes.Length == 1) centisecondes = "0" + centisecondes;
                timer.text = minutes + ":" + secondes + ":" + centisecondes;
                GameManager.GameState = 1;
            }
            else
            {
                centerText.text = null;
            }
        }
        if(GameManager.GameState == 1)
        {
            float time = timeSinceStart - 3;
            string minutes = ((int)time / 60).ToString();
            string secondes = ((int)(time % 60)).ToString();
            if (secondes.Length == 1) secondes = "0" + secondes;
            string centisecondes = ((int)((time % 1) * 100)).ToString();
            if (centisecondes.Length == 1) centisecondes = "0" + centisecondes;
            timer.text = minutes + ":" + secondes + ":" + centisecondes;
        }
        // Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - 0.001f);
    }
    void createCheckpoint()
    {
        CheckPoint NewCheckpoint = new CheckPoint();//new Vector3(Player.gameObject.transform.position.x, Player.gameObject.transform.position.y, Player.gameObject.transform.position.z + 5)
        NewCheckpoint.idCheckpoint = IDcheckPoint + 1;
        IDcheckPoint = IDcheckPoint + 1;
        GameCheckpointList.Add(NewCheckpoint);
    }

    public void AddCheckpoint(CheckPoint checkPoint)
    {
        checkPoint.idCheckpoint = IDcheckPoint + 1;
        IDcheckPoint = IDcheckPoint + 1;
        GameCheckpointList.Add(checkPoint);
    }

    IEnumerator CheckEndGame()
    {
        bool over = true;
        foreach (CheckPoint checkPoint in GameCheckpointList)
        {
            //Debug.Log("fin2" + checkPoint.isWaitingColision + " id" + checkPoint.idCheckpoint);
            if (checkPoint.isWaitingColision == true)
            {
                over = false;
            }
            else if (over != false)
            {
                over = true;
            }
        }
        if (over == true && GameManager.GameState < 2)
        {
            float score = timeSinceStart - 3;
            if ( score < highscore || highscore == 0)
            {
                SaveManager.setHighscore(Seed.seed, score);
                centerText.text = "<color=#84EB9E>NEW RECORD !</color>";
                string minutes = ((int)score / 60).ToString();
                string secondes = ((int)(score % 60)).ToString();
                if (secondes.Length == 1) secondes = "0" + secondes;
                string centisecondes = ((int)((score % 1) * 100)).ToString();
                if (centisecondes.Length == 1) centisecondes = "0" + centisecondes;
                highScoreText.text = "HIGHSCORE: " + minutes + ":" + secondes + ":" + centisecondes;
                highscore = score;
            }
            timer.text = "<color=#84EB9E>"+timer.text+"</color>";
            GameManager.GameState = 2;
            Debug.Log("fin");
            new WaitForSeconds(3);
            /// Debug.Log("fin4");
            // Application.Quit();
            // Debug.Log("fin5");
            Debug.Log(score+" " +highscore);
            EndGameManager.timer = score;
            EndGameManager.highScore = highscore;
            //EndGameManager ThisEngame = new EndGameManager(score, highscore);
            EndGameManager.LoadEndGameScene();
            yield break;
        }
        yield break;
    }


}
