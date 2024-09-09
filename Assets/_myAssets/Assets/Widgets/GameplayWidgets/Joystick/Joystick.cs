using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour , IPointerDownHandler , IPointerUpHandler , IDragHandler , IPointerClickHandler
{
    public delegate void InputUpdatedDelegate(Vector2 inputVal);

    InventoryComponent inventoryComponent;

    public event InputUpdatedDelegate OnInputUpdated;
    [SerializeField] private RectTransform rangeTransform;
    [SerializeField] private RectTransform thumbStickTransform;
    [SerializeField] private float deadZone = 0.2f;

    private float _range;

    private void Awake()
    {
        _range = rangeTransform.sizeDelta.x / 2f;
        deadZone = deadZone * deadZone;
    }
    private void Start()
    {
        inventoryComponent = GetComponent<InventoryComponent>();
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
        Vector2 input = offset / _range;

        if (input.sqrMagnitude < deadZone)
            return;

        OnInputUpdated?.Invoke(offset/_range);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Equip next weapon");
        inventoryComponent.EquipNextWeapon();
    }
    public void OnMouseDown()
    {
        Debug.Log("Equip next weapon");
        inventoryComponent.EquipNextWeapon();
    }
}
