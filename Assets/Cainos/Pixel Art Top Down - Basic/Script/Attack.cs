using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 0.1f;
    private float nextAttackTime = 0f;
    public GameObject attackArea;

    void Start()
    {
        if (attackArea != null)
        {
            attackArea.SetActive(false); // Pastikan off di awal
        }
    }

    void Update()
    {
        // Serang jika klik kiri dan tidak di atas UI
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            StartAttack();
        }

        // Hentikan animasi serangan saat tombol mouse dilepas
        if (Input.GetMouseButtonUp(0))
        {
            StopAttack();
        }
    }

    public void StartAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetBool("IsAttacking", true);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void StopAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    // Fungsi ini dipanggil lewat event di Animation
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
