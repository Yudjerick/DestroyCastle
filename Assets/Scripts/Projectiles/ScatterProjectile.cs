using UnityEngine;
public class ScatterProjectile: Projectile
{
    public override void OnCreated()
    {
        GameManager.instance.SetGameState(WaitForProjetileActivationState.Instance);
        Destroy(gameObject, lifeTime);
    }
    public override void Activate()
    {
        Instantiate(gameObject);
        Destroy(gameObject);
        GameManager.instance.SetGameState(CardSelectState.Instance);
    }
}