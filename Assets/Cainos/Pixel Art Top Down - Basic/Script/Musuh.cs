﻿using UnityEngine;
using UnityEngine.UI;

public class Musuh : MonoBehaviour
{
    public Transform player;
    public float speed = 1.5f;
    public float triggerRadius = 5f;

    public Transform[] patrolPoints;
    private int currentPoint = 0;

    private Rigidbody2D rb;
    private Animator anim;

    private bool chasingPlayer = false;

    [Header("HP Settings")]
    public float maxHP = 100f;
    private float currentHP;
    public Image hpBarFill;

    private float lastHitTime = 0f;
    private float hitCooldown = 0.2f;

    [Header("Attack Settings")]
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHP = maxHP;

        if (hpBarFill != null)
        {
            hpBarFill.fillAmount = 1f;
        }
        else
        {
            Debug.LogWarning("hpBarFill belum di-assign di Inspector!");
        }

        if (EnemyTracker.instance != null)
        {
            EnemyTracker.instance.RegisterEnemy();
        }
        else
        {
            Debug.LogWarning("EnemyTracker instance tidak ditemukan di scene!");
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
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
            }
        }
    }

    void SetAnim(bool isMoving)
    {
        if (anim != null)
        {
            anim.SetBool("isMoving", isMoving);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead || Time.time < lastHitTime + hitCooldown) return;
        lastHitTime = Time.time;

        currentHP -= amount;
        Debug.Log("Musuh kena damage: " + amount + ", sisa HP: " + currentHP);

        if (hpBarFill != null)
        {
            hpBarFill.fillAmount = Mathf.Clamp01(currentHP / maxHP);
        }

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        // Laporkan ke EnemyTracker
        if (EnemyTracker.instance != null)
        {
            EnemyTracker.instance.EnemyDefeated();
        }

        // Tambahkan animasi kematian jika ada
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        // Matikan collider & rb supaya tidak interaksi lagi
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        // Hancurkan setelah 1 detik agar animasi bisa diputar
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead || Time.time < lastAttackTime + attackCooldown) return;

        playermovement player = collision.GetComponent<playermovement>();
        if (player != null)
        {
            player.TakeDamage(attackDamage, Vector2.zero, false);
            lastAttackTime = Time.time;
        }
    }

    private void OnDestroy()
    {
        // Jaga-jaga kalau musuh dihancurkan di luar Die()
        if (!isDead && EnemyTracker.instance != null)
        {
            EnemyTracker.instance.EnemyDefeated();
        }
    }
}
