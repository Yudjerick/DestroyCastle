using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class Canon : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector2 _shootVector;

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
        _lineRenderer.positionCount = 0;
    }
}
