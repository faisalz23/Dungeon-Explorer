using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float speed = 1.5f;
    public float triggerRadius = 5f;
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    private Rigidbody2D rb;
    private Animator anim;
    private bool chasingPlayer = false;

    [Header("HP Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject hpBarPrefab;
    private HPBarController hpBar;
    private Transform hpBarTransform;

    [Header("Damage Logic")]
    private bool isDead = false;
    private float lastHitTime = 0f;
    private float hitCooldown = 0.2f;

    [Header("Drop Settings")]
    public GameObject coinPrefab; // drag prefab koin ke sini lewat Inspector
    public Transform dropOffset;  // opsional: untuk posisi jatuh (bisa pakai transform musuh)


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        SetupHPBar();

        if (EnemyTracker.instance != null)
        {
            EnemyTracker.instance.RegisterEnemy();
            Debug.Log("🟡 Enemy terdaftar ke EnemyTracker");
        }
        else
        {
            Debug.LogWarning("⚠️ EnemyTracker tidak ditemukan di scene!");
        }
    }

    void FixedUpdate()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        chasingPlayer = distanceToPlayer < triggerRadius;

        if (chasingPlayer)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            SetAnim(true);
        }
        else
        {
            if (patrolPoints.Length == 0) return;

            Vector2 target = patrolPoints[currentPoint].position;
            Vector2 moveDir = (target - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
            SetAnim(true);

            if (Vector2.Distance(transform.position, target) < 0.2f)
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void Update()
    {
        if (hpBarTransform != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
            hpBarTransform.position = screenPos;
        }
    }

    void SetAnim(bool isMoving)
    {
        if (anim != null)
            anim.SetBool("isMoving", isMoving);
    }

    void SetupHPBar()
    {
        if (hpBarPrefab != null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                GameObject hpGO = Instantiate(hpBarPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity, canvas.transform);
                hpBar = hpGO.GetComponent<HPBarController>();
                hpBarTransform = hpGO.transform;

                if (hpBar != null)
                    hpBar.SetHP(currentHealth, maxHealth);
                else
                    Debug.LogError("HPBarController tidak ditemukan di prefab HP bar!");
            }
            else
            {
                Debug.LogError("Canvas tidak ditemukan di scene!");
            }
        }
        else
        {
            Debug.LogError("hpBarPrefab belum di-assign di Inspector!");
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead || Time.time < lastHitTime + hitCooldown) return;
        lastHitTime = Time.time;

        Debug.Log("💥 Musuh terkena damage: " + damage);

        currentHealth -= Mathf.RoundToInt(damage);
        currentHealth = Mathf.Max(0, currentHealth);

        if (hpBar != null)
            hpBar.SetHP(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (EnemyTracker.instance != null)
        {
            EnemyTracker.instance.EnemyDefeated();
            Debug.Log("✅ Musuh mati, dilaporkan ke EnemyTracker");
        }

        if (coinPrefab != null)
        {
            Vector3 dropPosition = dropOffset != null ? dropOffset.position : transform.position;
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }

        if (hpBarTransform != null)
            Destroy(hpBarTransform.gameObject);

        Destroy(gameObject);
    }
}
