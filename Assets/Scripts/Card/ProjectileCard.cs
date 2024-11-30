using Unity.VisualScripting;
using UnityEngine;

public class ProjectileCard : Card
{
    [SerializeField] private Projectile projectile;
    public override void PerformAction()
    {
        base.PerformAction();
        GameManager.instance.ShootPanel.CurrentCanon.Projectile = projectile;
    }
}
