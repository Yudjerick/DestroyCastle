using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [Button]
    public void MakeMove()
    {
        StartCoroutine(MakeMoveCoroutine());
    }

    public IEnumerator MakeMoveCoroutine()
    {
        Card selectedCard = FindAnyObjectByType<Card>();
        Vector2 cardScreenPos = Camera.main.WorldToScreenPoint(selectedCard.transform.position);
        Vector2 destinationPos = new Vector2(Camera.main.scaledPixelWidth / 2, Camera.main.scaledPixelHeight * 0.75f);
        StartCoroutine(SimulateDrag(cardScreenPos, destinationPos, 1f, selectedCard));
        yield return new WaitUntil(() => GameManager.instance.CurrentState is WaitForShootState);

        ShootPanel shootPanel = FindAnyObjectByType<ShootPanel>();
        var shootWorldMagnittude = GameManager.instance.ShootPanel.MaxDragVector;
        var balisticResult = BallisticCalculator.CalculateLaunchVector(GameManager.instance.ShootPanel.CurrentCanon.transform.position, target.transform.position, GameManager.instance.ShootPanel.CurrentCanon.Projectile.LaunchForce);

        if (balisticResult != null)
        {
            Vector2 shootVector = (Vector2)balisticResult;
            Vector2 shootScreenDrag = Camera.main.WorldToScreenPoint(shootVector);
            Vector2 screenCenter = new Vector2(Camera.main.scaledPixelWidth / 2, Camera.main.scaledPixelHeight /2);
            GameManager.instance.ShootPanel.CurrentCanon.UpdateAimingLine(shootVector);
            GameManager.instance.ShootPanel.CurrentCanon.Shoot();
            //StartCoroutine(SimulateDrag(screenCenter, screenCenter - shootScreenDrag, 0.5f, shootPanel));
        }
        
    }

    public IEnumerator SimulateDrag(Vector2 start, Vector3 end, float time, ICombinedDragHandler handler)
    {
        float completionAlpha = 0f;
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = start;
        handler.OnBeginDrag(eventData);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            completionAlpha += Time.deltaTime / time;
            eventData.position = Vector2.Lerp(start, end, completionAlpha);
            handler.OnDrag(eventData);
            if (completionAlpha >= 1f)
            {
                eventData.position = Vector2.Lerp(start, end, completionAlpha);
                handler.OnEndDrag(eventData);
                break;
            }
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
