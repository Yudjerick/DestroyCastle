using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField] public float LaunchForce { get; private set; }
    [SerializeField] protected float lifeTime;

    public virtual void OnCreated()
    {
        GameManager.instance.SetGameState(WaitForDisappearState.Instance);
        Destroy(gameObject, lifeTime);
    }

    protected void OnDestroy()
    {
        print("OnDestroyCalled");
        GameManager.instance.SwitchPlayers();
        GameManager.instance.SetGameState(CardSelectState.Instance);
    }

    public virtual void Activate()
    {
        
    }
}
