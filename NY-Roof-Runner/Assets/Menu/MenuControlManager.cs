using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControlManager : MonoBehaviour
{

    public Button Return;
    // Start is called before the first frame update
    void Start()
    {
        Return.onClick.AddListener(LoadControlScene);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LoadControlScene()
    {
        SceneManager.LoadScene("MenuControls");
    }

    void ReturnMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
