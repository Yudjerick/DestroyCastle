using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShootPanel : MonoBehaviour, ICombinedDragHandler
{
    [SerializeField] private Canon canon1;
    [SerializeField] private Canon canon2;
    public Canon CurrentCanon { get; set; }
    [field: SerializeField] public float MinDragVector { get; set; }
    [field: SerializeField] public float MaxDragVector { get; set; }
    private Vector2 _touchStart;
    private Vector2 _touchEnd;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _touchStart = Camera.main.ScreenToWorldPoint(eventData.position);
        CurrentCanon.StartAiming();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _touchEnd = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2 dragVector = _touchStart - _touchEnd;

        if(dragVector.magnitude < MinDragVector)
        {
            dragVector = Vector2.zero;
        }
        if(dragVector.magnitude > MaxDragVector)
        {
            dragVector = dragVector.normalized * MaxDragVector;
        }
        CurrentCanon.UpdateAimingLine(dragVector);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if((_touchStart - _touchEnd).magnitude >= MinDragVector)
        {
            CurrentCanon.Shoot();
        }
        
    }

    public void SetRaycastTargeyActive(bool state)
    {
        GetComponent<Image>().raycastTarget = state;
    }
}
