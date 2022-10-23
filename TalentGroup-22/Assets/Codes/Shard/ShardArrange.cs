using UnityEngine;
using UnityEngine.EventSystems;
public class ShardArrange : 
MonoBehaviour, 
IBeginDragHandler, 
IDragHandler,
IEndDragHandler
{
    Canvas canvas = null;
    CanvasGroup canvasGroup = null;
    RectTransform rectTransform = null;
    public Vector2 initialPosition = Vector2.zero;
    public static bool isCorrected = false;
    void ResetPosition()
    {
        rectTransform.anchoredPosition = initialPosition;
    }
    void OnEnable() 
    {
        if (rectTransform != null) ResetPosition();
    }
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
        isCorrected = false;
    }
    public void OnBeginDrag(PointerEventData eventData) 
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData) 
    {
        ShardSlot.hasReceived = false;
        rectTransform.anchoredPosition += (eventData.delta / canvas.scaleFactor);
    }
    public void OnEndDrag(PointerEventData eventData) 
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (!ShardSlot.hasReceived) ResetPosition();
    }
}