using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 1.5f;
    public float triggerRadius = 5f;

    public Transform[] patrolPoints;
    private int currentPoint = 0;

    private Rigidbody2D rb;
    private Animator anim;
    private bool chasingPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

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
}
