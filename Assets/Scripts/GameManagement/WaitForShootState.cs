using UnityEngine;

public class WaitForShootState : GameState
{
    [SerializeField] private ShootPanel shootPanel;

    public static WaitForShootState Instance;

    private void Start()
    {
        Instance = this;
    }
    public override void OnEnter()
    {
        shootPanel.SetRaycastTargeyActive(true);
    }

    public override void OnExit()
    {
        shootPanel.SetRaycastTargeyActive(false);
    }
}
