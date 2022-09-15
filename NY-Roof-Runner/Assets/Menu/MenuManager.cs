using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Button startButton;
    public Button loadButton;
    public Button ControlButton;
    public GameObject seedInput;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        loadButton.onClick.AddListener(LoadGame);
        ControlButton.onClick.AddListener(LoadControlMenu);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void StartGame()
    {
        int seed = Random.Range(0, int.MaxValue);
        Seed.seed = seed;
        GameManager.startTime = Time.realtimeSinceStartup;
        SceneManager.LoadScene("GameScene");
    }

    void LoadGame()
    {
        string seedText = seedInput.GetComponent<TMP_InputField>().text;
        if (int.TryParse(seedText, out int result))
        {
            Seed.seed = result;
            GameManager.startTime = Time.realtimeSinceStartup;
            SceneManager.LoadScene("GameScene");
        } else
        {
            Debug.Log("Invalid seed");
        }
    }
    void LoadControlMenu()
    {
        //SceneManager.LoadScene("MenuControls");
    }
}
