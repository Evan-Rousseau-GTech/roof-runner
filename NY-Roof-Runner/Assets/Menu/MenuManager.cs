using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Button startButton;
    public Button loadButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(LoadGame);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
