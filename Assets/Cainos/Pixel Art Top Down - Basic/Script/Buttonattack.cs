using UnityEngine;
using UnityEngine.EventSystems;

public class Buttonattack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Attack attackScript;

    public void OnPointerDown(PointerEventData eventData)
    {
        attackScript?.StartAttack();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        attackScript?.StopAttack();
    }
}
