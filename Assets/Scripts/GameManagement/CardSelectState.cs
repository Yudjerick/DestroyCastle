using UnityEngine;

public class CardSelectState : GameState
{
    public static CardSelectState Instance;

    private void Start()
    {
        Instance = this;
    }
    public override void OnEnter()
    {
    }

    public override void OnExit()
    {

    }
}
