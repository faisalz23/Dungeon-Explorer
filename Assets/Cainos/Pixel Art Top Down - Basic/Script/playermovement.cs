using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private PlayerController inputActions;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        inputActions = new PlayerController();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.movement.movement.performed += OnMovePerformed;
        inputActions.movement.movement.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        inputActions.movement.movement.performed -= OnMovePerformed;
        inputActions.movement.movement.canceled -= OnMoveCanceled;
        inputActions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
         rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        int state;

        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            state = 1; // Run
            sprite.flipX = moveInput.x < 0;
        }
        else
        {
            state = 0; // Idle
        }

        anim.SetInteger("state", state);
    }
}
