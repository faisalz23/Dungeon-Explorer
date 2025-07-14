using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 0.1f;
    private float nextAttackTime = 0f;
    public GameObject attackArea;

    private bool isAttackingFromButton = false;

    void Start()
    {
        if (attackArea != null)
        {
            attackArea.SetActive(false);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        // Hanya di editor: masih bisa pakai klik kiri
        if (Input.GetMouseButtonDown(0))
        {
            StartAttack();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopAttack();
        }
#endif
    }

    public void StartAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetBool("IsAttacking", true);
            nextAttackTime = Time.time + attackCooldown;

            // SFX (kalau ada)
            FindObjectOfType<PlayerAudioManager>()?.PlayAttackSound();
        }
    }

    public void StopAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    // Dipanggil oleh event di animasi
    public void EnableAttackArea()
    {
        if (attackArea != null)
            attackArea.SetActive(true);
    }

    public void DisableAttackArea()
    {
        if (attackArea != null)
            attackArea.SetActive(false);
    }
}
