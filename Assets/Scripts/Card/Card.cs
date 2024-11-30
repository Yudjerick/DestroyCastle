using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, ICombinedDragHandler
{
    public bool DraggedNow { get; private set; }
    
    [SerializeField] private float returnToInitPosTime = 0.5f; //make anim curve later maybe?
    [SerializeField] private AnimationCurve returnToInitPosCurve;
    private float _returnToInitPosAplpha;

    [SerializeField] private float highlightTime = 0.1f;
    [SerializeField] private float scaleIncreaseMultipier = 1.1f;
    private Vector3 _initScale;
    private Quaternion _initRot;
    private float _scaleIncreaseAlpha;
    private bool _scaleIncreasing;

    [SerializeField] private float dissolveTime;
    private float _dissolveAlpha;

    private bool preventDrag = false;
    private Vector2 _touchOffset;
    private Vector2 _initialPosition;

    private ShootPanel _shootPanel;

    private void Start()
    {
        _initScale = transform.localScale;
        _shootPanel = FindAnyObjectByType<ShootPanel>();
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if(!preventDrag)
        {
            DraggedNow = true;
            if (!_scaleIncreasing)
            {
                StartCoroutine(IncreaseScale());
            }
            
            _touchOffset = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - (Vector2)transform.position;
            _initialPosition = transform.position;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!preventDrag) 
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - _touchOffset;
        }

        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        if (eventData.position.y > Camera.main.scaledPixelHeight * 2 / 3)
        {
            StartCoroutine(Dissolve());
        }
        else
        {
            DraggedNow = false;
            StartCoroutine(ReturnToInitialPos());
        }
        print(eventData.position);
    }

    IEnumerator IncreaseScale()
    {
        _scaleIncreasing = true;
        _scaleIncreaseAlpha = 0f;
        
        _initRot = transform.localRotation;
        while (_scaleIncreaseAlpha < 1f)
        {
            yield return new WaitForEndOfFrame();
            _scaleIncreaseAlpha += Time.deltaTime / highlightTime;
            transform.localScale = Vector2.Lerp(_initScale, _initScale * scaleIncreaseMultipier, _scaleIncreaseAlpha);
            transform.localRotation = Quaternion.Lerp(_initRot, Quaternion.identity, _scaleIncreaseAlpha);
        }
        yield return new WaitWhile(() => DraggedNow);
        _scaleIncreaseAlpha = 0f;
        while (_scaleIncreaseAlpha < 1f)
        {
            yield return new WaitForEndOfFrame();
            _scaleIncreaseAlpha += Time.deltaTime / highlightTime;
            transform.localScale = Vector2.Lerp(_initScale * scaleIncreaseMultipier, _initScale, _scaleIncreaseAlpha);
            transform.localRotation = Quaternion.Lerp(Quaternion.identity, _initRot, _scaleIncreaseAlpha);
        }
        _scaleIncreasing = false;
    }

    IEnumerator ReturnToInitialPos()
    {
        Vector2 startPos = transform.position;
        print("Coroutine");
        preventDrag = true;
        _returnToInitPosAplpha = 0;
        while (_returnToInitPosAplpha < 1f)
        {
            yield return new WaitForEndOfFrame();
            _returnToInitPosAplpha += Time.deltaTime / returnToInitPosTime;
            if(returnToInitPosCurve != null)
            {
                transform.position = Vector2.Lerp(startPos, _initialPosition, returnToInitPosCurve.Evaluate(_returnToInitPosAplpha));
            }
            else
            {
                transform.position = Vector2.Lerp(startPos, _initialPosition, _returnToInitPosAplpha);
            }
        }
        preventDrag = false;
    }

    IEnumerator Dissolve()
    {
        while (_dissolveAlpha < 1f)
        {
            yield return new WaitForEndOfFrame();
            _dissolveAlpha += Time.deltaTime / dissolveTime;
            GetComponent<CanvasGroup>().alpha = 1 - _dissolveAlpha;
        }
        PerformAction();
        Destroy(gameObject);
    }

    public virtual void PerformAction()
    {
        GameManager.instance.SetGameState(WaitForShootState.Instance);
    }
}
