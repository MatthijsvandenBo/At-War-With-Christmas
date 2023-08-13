using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject startButtons;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject controlls;

    public void StartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsButton()
    {
        startButtons.SetActive(false);
        backButton.SetActive(true);
        credits.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void ControlleButton()
    {
        startButtons.SetActive(false);
        credits.SetActive(false);
        controlls.SetActive(true);
        backButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void BackButton()
    {
        backButton.SetActive(false);
        credits.SetActive(false);
        controlls.SetActive(false);
        startButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
