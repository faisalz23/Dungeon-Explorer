using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 0.1f;
    private float nextAttackTime = 0f;

    void Update()
    {
        // Cek apakah klik kiri mouse dan pointer tidak di UI
        //if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        //{
         //   StartAttack();
        //}

        // Untuk stop attack, biasanya pointer up bisa langsung stop tanpa cek UI
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
}
