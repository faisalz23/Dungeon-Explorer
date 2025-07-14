using System.Collections;
using UnityEngine;
using TMPro;

public class playermovement : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameObject levelCompletePanel;

    private PlayerController playerControl;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;
    public SpriteRenderer sprite;
    private bool facingLeft = false;

    [Header("Health System")]
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText;
    public HPBarController hpBarUI;

    [Header("Knockback Settings")]
    [SerializeField] private float knockBackTime = 0.2f;
    [SerializeField] private float knockBackThrust = 10f;
    private bool isKnockedBack = false;

    [Header("Damage Cooldown")]
    private float lastDamageTime = 0f;
    private float damageCooldown = 0.1f;

    [Header("Coin System")]
    public int currentCoin = 0;
    public TextMeshProUGUI coinText;

    private PlayerAudioManager audioManager;
    private bool isFootstepPlaying = false;

    public void TakeDamage(int damage, Vector2 direction, bool applyKnockback)
    {
        if (Time.time < lastDamageTime + damageCooldown) return;
        lastDamageTime = Time.time;

        if (applyKnockback && isKnockedBack) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player Mati");
            // Tambahkan animasi atau efek kematian
        }

        UpdateHealthUI();

        if (applyKnockback)
            StartCoroutine(HandleKnockback(direction.normalized));
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        TakeDamage(damage, direction, true);
    }

    private IEnumerator HandleKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        rb.velocity = Vector2.zero;

        Vector2 force = direction * knockBackThrust * rb.mass;
        rb.AddForce(force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + currentHealth;

        if (hpBarUI != null)
            hpBarUI.SetHP(currentHealth, maxHealth);
    }

    private void Awake()
    {
        playerControl = new PlayerController();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        audioManager = FindObjectOfType<PlayerAudioManager>();
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
        if (isKnockedBack) return;

        PlayerInput();
        anim.SetFloat("Speed", movement.magnitude);

        if (movement.magnitude > 0.1f)
        {
            if (!isFootstepPlaying)
                StartCoroutine(PlayFootstepWithDelay());
        }
    }

    private IEnumerator PlayFootstepWithDelay()
    {
        isFootstepPlaying = true;
        if (audioManager != null)
            audioManager.PlayFootstepSound();
        yield return new WaitForSeconds(0.4f);
        isFootstepPlaying = false;
    }

    private void FixedUpdate()
    {
        if (isKnockedBack) return;

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
            Debug.Log("Player triggered the door.");

            if (EnemyTracker.instance != null && EnemyTracker.instance.AllEnemiesDefeated())
            {
                Debug.Log("All enemies defeated. Showing level complete panel...");
                if (levelCompletePanel != null)
                    levelCompletePanel.SetActive(true);

                movement = Vector2.zero;
                this.enabled = false;

                Collider2D doorCollider = collision.GetComponent<Collider2D>();
                if (doorCollider != null)
                {
                    doorCollider.enabled = false;
                }

                if (audioManager != null)
                    audioManager.PlayLevelCompleteSound();
            }
            else
            {
                Debug.Log("Belum semua musuh dikalahkan!");
                Vector2 pushBack = (transform.position - collision.transform.position).normalized;
                rb.MovePosition(rb.position + pushBack * 0.1f);
            }
        }
    }

    public void addCoin(int amount)
    {
        currentCoin += amount;
        if (coinText != null)
            coinText.text = "Coin : " + currentCoin.ToString();

        if (audioManager != null)
            audioManager.PlayPickupSound();
    }
}
