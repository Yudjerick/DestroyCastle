using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileActivationPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Projectile Projectile { get; set; }
    public void OnPointerDown(PointerEventData eventData)
    {
        Projectile.Activate();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
