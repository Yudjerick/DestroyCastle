using UnityEngine;

public class WaitForDisappearState : GameState
{
    public static WaitForDisappearState Instance;

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    void Start()
    {
        Instance = this;
    }
}
