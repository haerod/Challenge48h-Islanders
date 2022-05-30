using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panelCredits;
    public GameObject fadeToBlack;

    private void Start()
    {
        panelCredits.SetActive(false);
        fadeToBlack.SetActive(false);
    }

    public void ClickOnCredits()
    {
        panelCredits.SetActive(true);
    }

    public void ClickOnClose()
    {
        panelCredits.SetActive(false);
    }

    public void ClickOnStart()
    {
        StartCoroutine(Start_Co());
        fadeToBlack.SetActive(true);
    }

    IEnumerator Start_Co()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void ClickOnQuit()
    {
        Application.Quit();
    }
}
