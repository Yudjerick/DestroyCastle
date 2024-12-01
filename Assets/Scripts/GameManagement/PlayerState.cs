using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private Canon canon;
    [field: SerializeField] public CanvasGroup Cards { get; set; }
    public void OnEnter()
    {
        GameManager.instance.ShootPanel.CurrentCanon = canon;
        EnemyAI enemyAI = gameObject.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.MakeMove();
        }
    }
    public void OnExit() 
    {

    }

}
