using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShardSlot : 
MonoBehaviour, 
IDropHandler
{
    public Vector2 coordinate = Vector2.zero;
    public static List<Vector2> correctCoordinates = new List<Vector2>();
    RectTransform rectTransform = null;
    RectTransform shardRectTransform = null;
    public static bool hasReceived = false;
    static bool correctCoordinateIsExist = false;
    void Start() 
    {
        correctCoordinates.Clear();
        rectTransform = GetComponent<RectTransform>();
        hasReceived = false;
    }
    public void OnDrop(PointerEventData eventData) 
    {
        if (eventData.pointerDrag != null)
        {
            hasReceived = true;
            shardRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            shardRectTransform.anchoredPosition = rectTransform.anchoredPosition;
            #region Check Correctness
            if 
            (
                coordinate == new Vector2(1f, 4f) 
                || 
                coordinate == new Vector2(2f, 1f) 
                || 
                coordinate == new Vector2(3f, 3f) 
                || 
                coordinate == new Vector2(3f, 6f) 
                || 
                coordinate == new Vector2(4f, 5f) 
                || 
                coordinate == new Vector2(5f, 2f) 
                || 
                coordinate == new Vector2(6f, 7f) 
            )
            {
                if (correctCoordinates.Count == 0) correctCoordinates.Add(coordinate);
                else 
                {
                    correctCoordinateIsExist = false;
                    foreach (Vector2 correctCoordinate in correctCoordinates) 
                    {
                        if (coordinate == correctCoordinate) correctCoordinateIsExist = true;
                    }
                    if (!correctCoordinateIsExist) correctCoordinates.Add(coordinate);
                }
            }
            #endregion
        }
    }
}