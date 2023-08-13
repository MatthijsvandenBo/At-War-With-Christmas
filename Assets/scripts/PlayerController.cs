using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int ID;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction movement;
    private InputAction ladder;
    private Rigidbody rb;
    private PlayerInput playerInput;
    [SerializeField] private float movementForce = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;
    [SerializeField] private Camera playerCamera;
    private Vector2 delta;
    private bool controller;
    public float sensitivityController = 50;
    public float sensitivityMouse = 5;
    public int killCount = 0;

    [SerializeField] private GameObject firePoint;

    private Animator animator;
    private bool running = false;

    private bool off = false;
    public bool onladder = false;
    private float upDownSpeed = 2;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        if (playerInput.currentControlScheme == "Gamepad")
        {
            controller = true;
        }
        rb = GetComponent<Rigidbody>();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        player.FindAction("Movement").started += DoMove;
        player.FindAction("Movement").canceled += DoRestMove;
        player.FindAction("Run").started += DoRun;
        player.FindAction("Jump").started += DoJump;
        player.FindAction("Crouch").performed += DoCrouch;
        player.FindAction("Crouch").canceled += DoResetCrouch;
        player.FindAction("Look").performed += DoLook;
        player.FindAction("Look").canceled += DoResetLook;
        movement = player.FindAction("Movement");
        ladder = player.FindAction("Ladder");
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Movement").started -= DoMove;
        player.FindAction("Movement").canceled -= DoRestMove;
        player.FindAction("Run").started -= DoRun;
        player.FindAction("Jump").started -= DoJump;
        player.FindAction("Crouch").performed -= DoCrouch;
        player.FindAction("Crouch").canceled -= DoResetCrouch;
        player.FindAction("Look").performed -= DoLook;
        player.FindAction("Look").canceled -= DoResetLook;
        player.Disable();
    }

    private void DoClimb(InputAction.CallbackContext obj)
    {

    }

    private void DoMove(InputAction.CallbackContext obj)
    {
        SoundManager.instance.WalkingSound();
        animator.SetBool("IsWalking", true);
    }

    private void DoRestMove(InputAction.CallbackContext obj)
    {
        animator.SetBool("IsWalking", false);
    }

    private void DoRun(InputAction.CallbackContext obj)
    {
        running = running ? false : true;
        if (running)
        {
            animator.SetBool("IsRunning", true);
            maxSpeed = 10;
        }
        else
        {
            animator.SetBool("IsRunning", false);
            maxSpeed = 5;
        }
    }

    private void DoLook(InputAction.CallbackContext context)
    {
        delta = context.ReadValue<Vector2>();
        if (!controller)
        {
            float pan = playerCamera.transform.eulerAngles.y;
            float tilt = playerCamera.transform.eulerAngles.x;

            pan += delta.x * Time.deltaTime * sensitivityMouse;
            tilt += -delta.y * Time.deltaTime * sensitivityMouse;

            tilt = (tilt > 180) ? tilt - 360 : tilt;

            tilt = Mathf.Clamp(tilt, -90, 30);

            gameObject.transform.localRotation = Quaternion.Euler(0, pan, 0);
            playerCamera.transform.localRotation = Quaternion.Euler(tilt, 0, 0);
            firePoint.transform.localRotation = Quaternion.Euler(tilt, 0, 0);
        }
    }

    private void Update()
    {
        if (controller && !off)
        {
            float pan = playerCamera.transform.eulerAngles.y;
            float tilt = playerCamera.transform.eulerAngles.x;

            pan += delta.x * Time.deltaTime * sensitivityController;
            tilt += -delta.y * Time.deltaTime * sensitivityController;

            tilt = (tilt > 180) ? tilt - 360 : tilt;

            tilt = Mathf.Clamp(tilt, -90, 30);

            gameObject.transform.localRotation = Quaternion.Euler(0, pan, 0);
            playerCamera.transform.localRotation = Quaternion.Euler(tilt, 0, 0);
            firePoint.transform.localRotation = Quaternion.Euler(tilt, 0, 0);
        }
    }

    private void DoResetLook(InputAction.CallbackContext obj)
    {
        delta.x = 0;
        delta.y = 0;
    }

    private void DoCrouch(InputAction.CallbackContext obj)
    {
        GetComponent<CapsuleCollider>().height = 1.3f;
        GetComponent<CapsuleCollider>().center = new Vector3(0, 0.5f, 0);
    }

    private void DoResetCrouch(InputAction.CallbackContext obj)
    {
        GetComponent<CapsuleCollider>().height = 1.85f;
        GetComponent<CapsuleCollider>().center = new Vector3(0, 0.95f, 0);
    }

    private void FixedUpdate()
    {
        if (!off && !onladder)
        {
            forceDirection += movement.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
            forceDirection += movement.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;
            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            if (rb.velocity.y < 0f)
            {
                rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
            }

            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;
            if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
            }
        }
        if (!off && onladder)
        {
            Vector2 upDown = ladder.ReadValue<Vector2>();
            if (onladder)
            {
                if (upDown.y >= 0.2)
                {
                    transform.position += Vector3.up / upDownSpeed;
                }
                if (upDown.y <= -0.2)
                {
                    transform.position += Vector3.down / upDownSpeed;
                }
            }
        }
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            animator.SetTrigger("IsJumping");
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f))
            return true;
        else
            return false;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    public void TurnOff(bool turnOff)
    {
        if (turnOff)
        {
            player.FindAction("Movement").started -= DoMove;
            player.FindAction("Movement").canceled -= DoRestMove;
            player.FindAction("Run").started -= DoRun;
            player.FindAction("Jump").started -= DoJump;
            player.FindAction("Crouch").performed -= DoCrouch;
            player.FindAction("Crouch").canceled -= DoResetCrouch;
            player.FindAction("Look").performed -= DoLook;
            player.FindAction("Look").canceled -= DoResetLook;
            off = true;
        }
        else
        {
            player.FindAction("Movement").started += DoMove;
            player.FindAction("Movement").canceled += DoRestMove;
            player.FindAction("Run").started += DoRun;
            player.FindAction("Jump").started += DoJump;
            player.FindAction("Crouch").performed += DoCrouch;
            player.FindAction("Crouch").canceled += DoResetCrouch;
            player.FindAction("Look").performed += DoLook;
            player.FindAction("Look").canceled += DoResetLook;
            off = false;
        }
    }
}
