using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private Canon canon;
    public void OnEnter()
    {
        GameManager.instance.ShootPanel.CurrentCanon = canon;
    }
    public void OnExit() 
    {

    }

}
