using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour , IPointerDownHandler , IPointerUpHandler , IDragHandler
{
    public delegate void InputUpdatedDelegate(Vector2 inputVal);

    public event InputUpdatedDelegate OnInputUpdated;
    [SerializeField] private RectTransform rangeTransform;
    [SerializeField] private RectTransform thumbStickTransform;

    private float _range;

    private void Awake()
    {
        _range = rangeTransform.sizeDelta.x / 2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rangeTransform.position = eventData.position;
        thumbStickTransform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        thumbStickTransform.localPosition = Vector2.zero;
        rangeTransform.localPosition = Vector2.zero;
        OnInputUpdated?.Invoke(Vector2.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset = Vector2.ClampMagnitude(eventData.position - eventData.pressPosition, _range);
        thumbStickTransform.position = eventData.pressPosition + offset;
        Debug.Log($"offset is: {offset}");
        OnInputUpdated?.Invoke(offset/_range);
    }
    
    
}
