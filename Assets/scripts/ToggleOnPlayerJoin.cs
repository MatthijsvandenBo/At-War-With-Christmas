using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleOnPlayerJoin : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField] GameObject canvas;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += ToggleThis;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= ToggleThis;
    }

    private void ToggleThis(PlayerInput player)
    {
        this.gameObject.SetActive(false);
        canvas.SetActive(false);
    }
}
