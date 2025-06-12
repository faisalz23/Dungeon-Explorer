using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    public Image fillImage; // Assign ini di prefab bar HP (BarEnemy > bar_fill)

    public void SetHP(int current, int max)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = (float)current / max;
        }
    }
}
