using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameState initialState;
    public GameState CurrentState { get; private set; }

    [SerializeField] private PlayerState initnialPlayer;
    [SerializeField] private PlayerState _currentPlayer;

    [SerializeField] private PlayerState Player1;
    [SerializeField] private PlayerState Player2;

    public ShootPanel ShootPanel { get; set; }
    void Start()
    {
        
        instance = this;
        ShootPanel = FindAnyObjectByType<ShootPanel>();
        _currentPlayer = initnialPlayer;
        _currentPlayer.OnEnter();
        CurrentState = initialState;
        CurrentState.OnEnter();

    }

    public void SwitchPlayers()
    {
        if (_currentPlayer == Player1) 
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
        _currentPlayer.OnExit();
        _currentPlayer = newPlayer;
        _currentPlayer.OnEnter();
    }
}
