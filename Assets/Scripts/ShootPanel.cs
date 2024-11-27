using UnityEngine;
using UnityEngine.EventSystems;

public class ShootPanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canon canon;
    private Vector2 _touchStart;
    private Vector2 _touchEnd;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _touchStart = Camera.main.ScreenToWorldPoint(eventData.position);
        canon.StartAiming();
        print("Panel");
    }

    public void OnDrag(PointerEventData eventData)
    {
        _touchEnd = Camera.main.ScreenToWorldPoint(eventData.position);
        canon.UpdateAimingLine(_touchStart - _touchEnd);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canon.Shoot();
    }
}
