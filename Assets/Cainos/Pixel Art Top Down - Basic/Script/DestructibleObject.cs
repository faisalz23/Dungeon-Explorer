using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int maxHP = 50;
    private int currentHP;

    [Header("Animation")]
    private Animator anim;
    private bool isDestroyed = false;

    [Header("Item Drop Settings")]
    public GameObject dropItemPrefab;
    [Range(0f, 1f)]
    public float dropChance = 0.3f;

    void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return;

        currentHP -= Mathf.RoundToInt(damage);

        if (currentHP <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        isDestroyed = true;

        if (anim != null)
            anim.SetTrigger("Break");

        // 🔊 Suara objek hancur
        FindObjectOfType<PlayerAudioManager>()?.PlayDestroyObjectSound();

        TryDropItem();

        Destroy(gameObject, 0.5f);
    }

    void TryDropItem()
    {
        if (dropItemPrefab == null) return;

        float randomValue = Random.value;
        if (randomValue <= dropChance)
        {
            Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
        }
    }
}
