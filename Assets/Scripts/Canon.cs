using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(LineRenderer))]
public class Canon : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector2 _shootVector;

    [SerializeField] private GameObject projectile; 

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _lineRenderer.useWorldSpace = false;
    }

    public void UpdateAimingLine(Vector2 force)
    {
        _shootVector = force;
        _lineRenderer.SetPosition(1, force);
    }

    public void StartAiming()
    {
        _lineRenderer.positionCount = 2;
    }

    public void Shoot()
    {
        var instance = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,Vector2.SignedAngle( Vector2.up, _shootVector)) );
        instance.GetComponent<Rigidbody2D>().AddForce(instance.transform.up * instance.GetComponent<Projectile>().LaunchForce * _shootVector.magnitude);
        _lineRenderer.positionCount = 0;

        FindAnyObjectByType<ShootPanel>().GetComponent<Image>().raycastTarget = false;
    }
}
