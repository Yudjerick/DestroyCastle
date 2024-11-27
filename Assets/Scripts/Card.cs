using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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

    private bool preventDrag = false;
    private Vector2 _touchOffset;
    private Vector2 _initialPosition;

    private void Start()
    {
        _initScale = transform.localScale;
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
            print("DragBegin");
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!preventDrag) 
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - _touchOffset;
            print("Drag");
        }

        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggedNow = false;
        if (eventData.position.y > Camera.main.scaledPixelHeight * 2 / 3)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ReturnToInitialPos());
        }

        print("DragEnd");
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
}
