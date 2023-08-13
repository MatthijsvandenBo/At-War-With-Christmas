using SimplePool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    [SerializeField] Slider sensitivityControllerSlider;
    [SerializeField] Slider sensitivityMouseSlider;

    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        player.FindAction("Pause").performed += DoPause;
        player.Enable();
    }
    private void OnDisable()
    {
        player.FindAction("Pause").performed -= DoPause;
        player.Disable();
    }

    private void DoPause(InputAction.CallbackContext obj)
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            hud.SetActive(true);
            GetComponent<PlayerControlShoot>().TurnOffShooting(false);
            GetComponent<PlayerController>().TurnOff(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            pauseMenu.SetActive(true);
            hud.SetActive(false);
            GetComponent<PlayerControlShoot>().TurnOffShooting(true);
            GetComponent<PlayerController>().TurnOff(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        hud.SetActive(true);
        GetComponent<PlayerControlShoot>().TurnOffShooting(false);
        GetComponent<PlayerController>().TurnOff(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitButton()
    {
        if (GameManager.instance.players.Count > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void MouseSensitivityChange()
    {
         GetComponent<PlayerController>().sensitivityMouse = sensitivityMouseSlider.value;
    }

    public void ControllerSensitivityChange()
    {
        GetComponent<PlayerController>().sensitivityController = sensitivityControllerSlider.value;
    }

}
