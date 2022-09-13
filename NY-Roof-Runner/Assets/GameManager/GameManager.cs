using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [SerializeField] float PlayerScore = 0;
    [SerializeField] int PlayerHp = 5;
    [SerializeField] float Gametimer = 0;
    [SerializeField] int GameState = 0; //0 = pas commencer, 1 = vient de commencer,2 = partie fini,3 = fermer le jeu
    [SerializeField] int IDcheckPoint = 1;
    [SerializeField] List<CheckPoint> GameCheckpointList = new List<CheckPoint>();
    Character Player;

    // Start is called before the first frame update
    void Start()
    {
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
       // Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - 0.001f);
    }
    /*
    void createCheckpoint()
    {
        CheckPoint NewCheckpoint = new CheckPoint();//new Vector3(Player.gameObject.transform.position.x, Player.gameObject.transform.position.y, Player.gameObject.transform.position.z + 5)
        NewCheckpoint.idCheckpoint = IDcheckPoint + 1;
        IDcheckPoint = IDcheckPoint + 1;
        GameCheckpointList.Add(NewCheckpoint);
    }
    */
    IEnumerator CheckEndGame()
    {
        bool over = true;
        foreach (CheckPoint checkPoint in GameCheckpointList)
        {
            Debug.Log("fin2"+ checkPoint.isWaitingColision+ " id"+ checkPoint.idCheckpoint);
            if (checkPoint.isWaitingColision == true)
            {
                over = false;
            }
            else if(over != false)
            {
                over = true;
            }
        }
        if(over == true)
        {
            GameState = 2;
            Debug.Log("fin3");
            new WaitForSeconds(3);
            Debug.Log("fin4");
            Application.Quit();
            Debug.Log("fin5");
            yield break;
        }
        yield break;
    }
}
