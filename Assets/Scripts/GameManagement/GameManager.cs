using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameState initialState;
    public GameState CurrentState { get; private set; }

    [SerializeField] private PlayerState initnialPlayer;
    public PlayerState CurrentPlayer { get; private set; }

    [SerializeField] private PlayerState Player1;
    [SerializeField] private PlayerState Player2;

    public ShootPanel ShootPanel { get; set; }
    void Start()
    {
        
        instance = this;
        ShootPanel = FindAnyObjectByType<ShootPanel>();
        CurrentPlayer = initnialPlayer;
        CurrentPlayer.OnEnter();
        CurrentState = initialState;
        CurrentState.OnEnter();

    }

    public void SwitchPlayers()
    {
        if (CurrentPlayer == Player1) 
        {
            SetPlayer(Player2);
        }
        else 
        {
            SetPlayer(Player1);
        }
    }

    public void SetGameState(GameState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }

    public void SetPlayer(PlayerState newPlayer)
    {
        CurrentPlayer.OnExit();
        CurrentPlayer = newPlayer;
        CurrentPlayer.OnEnter();
    }
}
