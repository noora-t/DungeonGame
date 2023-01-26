using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{
    [SerializeField] GameObject _defeatMenuObjects;
    [SerializeField] GameObject _levelCompleteObjects;

    int lastLevel = 3;

    GameObject _touchInputCanvas;

    void Start()
    {
        _touchInputCanvas = GameObject.Find("Touch Input Canvas");
    }

    public void ShowDefeatMenu()
    {
        Invoke("PauseOnDelay", 4f);
    }

    void PauseOnDelay()
    {
        Time.timeScale = 0;
        _defeatMenuObjects.SetActive(true);
        _touchInputCanvas.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        _defeatMenuObjects.SetActive(false);
        _levelCompleteObjects.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == lastLevel)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
