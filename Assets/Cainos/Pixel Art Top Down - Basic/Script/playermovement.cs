using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameObject levelCompletePanel; // Tambahkan referensi ke panel UI

    private PlayerController playerControl;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;
    public SpriteRenderer sprite;
    private bool facingLeft = false;

    private void Awake()
    {
        playerControl = new PlayerController();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    private void Update()
    {
        PlayerInput();
        anim.SetFloat("Speed", movement.magnitude);
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControl.movement.movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        if (movement.x < 0f)
        {
            sprite.flipX = true;
            FacingLeft = true;
        }
        else if (movement.x > 0f)
        {
            sprite.flipX = false;
            FacingLeft = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            Debug.Log("Player triggered the door! Showing level complete panel...");

            if (levelCompletePanel != null)
            {
                levelCompletePanel.SetActive(true);
            }

            // Hentikan pergerakan dan input player
            movement = Vector2.zero;
            this.enabled = false;
        }
    }
}
