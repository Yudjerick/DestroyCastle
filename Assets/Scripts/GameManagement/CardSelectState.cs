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
        GameManager.instance.CurrentPlayer.Cards.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        GameManager.instance.CurrentPlayer.Cards.blocksRaycasts = false;
    }
}
