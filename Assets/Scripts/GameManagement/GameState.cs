using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    private static GameState instance;

    private static void SetInstance(GameState value)
    {
        instance = value;
    }

    private void Start()
    {
        instance = this;
    }
    public abstract void OnEnter();

    public abstract void OnExit();
}
