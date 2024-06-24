using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    private PlayerInputActions inputActions;
    private EventQueue eventQueue;
    private Weapon weapon;
    private Vector2 moveInput;
    private Camera mainCamera;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        eventQueue = FindObjectOfType<EventQueue>();
        if (eventQueue == null)
        {
            Debug.LogError("EventQueue not found in the scene. Make sure there is an EventQueue GameObject with the EventQueue script attached.");
        }
        weapon = GetComponent<Weapon>();
        if (weapon == null)
        {
            Debug.LogError("Weapon component not found on the Player GameObject. Make sure there is a Weapon script attached to the Player GameObject.");
        }
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Shoot.started += OnShoot;
        inputActions.Player.Shoot.canceled += OnShootCanceled;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputActions.Player.Shoot.started -= OnShoot;
        inputActions.Player.Shoot.canceled -= OnShootCanceled;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);
            ICommand moveCommand = new MoveCommand(transform, direction, moveSpeed, mainCamera);
            eventQueue.EnqueueEvent(moveCommand);
        }
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (eventQueue == null || weapon == null) return;

        if (Time.time >= nextFireTime)
        {
            ICommand shootCommand = new ShootCommand(weapon);
            eventQueue.EnqueueEvent(shootCommand);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        // No se necesita acción específica cuando se cancela el disparo
    }
}