using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class AbilityDock : Widget, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] AbilityWidget abilityWidgetPrefab;
    [SerializeField] RectTransform rootPanel;
    [SerializeField] float scaleRange = 150f;

    [SerializeField] float scaledSize = 1.5f;
    [SerializeField] float scaleRate = 20f;

    Vector3 _goalScale = Vector3.one;

    List<AbilityWidget> _abiltyWidgets = new List<AbilityWidget>();

    AbilitiySystemComponent _abilitySystemComponent;

    PointerEventData _touchData;
    private void Update()
    {
        if (_touchData != null)
        {
            ArrayScale();
            _goalScale = Vector3.one * scaledSize;
        }
        else
        {
            ResetScale();
            _goalScale = Vector3.one;
        }

        rootPanel.transform.localScale = Vector3.Lerp(rootPanel.transform.localScale, _goalScale, scaleRate * Time.deltaTime);
    }

    private void ResetScale()
    {
        foreach (AbilityWidget abilityWidget in _abiltyWidgets)
        {
            abilityWidget.SetScaleAmt(0);
        }
    }

    private void ArrayScale()
    {
        float touchYPos = _touchData.position.y;
        foreach (AbilityWidget abilityWidget in _abiltyWidgets)
        {
            float widgetYPos = abilityWidget.transform.position.y;
            float distanceToTouch = Mathf.Abs(widgetYPos - touchYPos);
            if (distanceToTouch > scaleRange)
            {
                abilityWidget.SetScaleAmt(0);
                continue;
            }

            float scaleAmt = (scaleRange - distanceToTouch) / scaleRange;
            abilityWidget.SetScaleAmt(scaleAmt);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _touchData = eventData;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ActivateAbilityUnderTouch();
        _touchData = null;
    }

    private void ActivateAbilityUnderTouch()
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_touchData, raycastResults);

        foreach (RaycastResult raycastResult in raycastResults)
        {
            AbilityWidget abilityWidget = raycastResult.gameObject.GetComponent<AbilityWidget>();
            if (abilityWidget != null)
            {
                abilityWidget.CastAbility();
                return;
            }
        }
    }

    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        _abilitySystemComponent = newOwner.GetComponent<AbilitiySystemComponent>();
        if (_abilitySystemComponent)
        {
            _abilitySystemComponent.OnAbilityGiven += AbilityGiven;
        }
    }

    private void AbilityGiven(Ability newAbility)
    {
        AbilityWidget newAbilityWidget = Instantiate(abilityWidgetPrefab, rootPanel);
        newAbilityWidget.Init(newAbility);
        _abiltyWidgets.Add(newAbilityWidget);
    }
}
