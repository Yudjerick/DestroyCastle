using UnityEngine;
using UnityEngine.UI;

public class WaitForProjetileActivationState : GameState
{
    [SerializeField] private ProjectileActivationPanel activationPanel;

    public static WaitForProjetileActivationState Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }
    public override void OnEnter()
    {
        activationPanel.GetComponent<Image>().raycastTarget = true;
    }

    public override void OnExit()
    {
        activationPanel.GetComponent<Image>().raycastTarget = false;
    }
}

