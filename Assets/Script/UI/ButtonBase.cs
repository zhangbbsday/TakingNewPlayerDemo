using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ButtonManager;

public abstract class ButtonBase : UIElementBase, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public Button Button { get; private set; }
    public Transform TextTransform { get; private set; }
    public Text Text { get; private set; }
    protected IButtonEffect ButtonEffect { get; private set; }

    protected override void Start()
    {
        base.Start();

        if (Transform.childCount != 0)
        {
            TextTransform = Transform.GetChild(0);
            if (TextTransform != null)
                Text = TextTransform.GetComponent<Text>();
        }

        Button = GetComponent<Button>();

        ButtonEffectType effect = SetButtonEffect();
        ButtonEffect = ButtonManager.Instance.GetEffect(effect, this);
        var action = AddMethod();
        Button.onClick.AddListener(() => action());
    }

    protected abstract Action AddMethod();
    protected virtual ButtonEffectType SetButtonEffect()
    {
        return ButtonEffectType.None;
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        ButtonEffect?.PressEffect();
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        ButtonEffect?.EnterEffect();
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ButtonEffect?.ExitEffect();
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        ButtonEffect?.ReleaseEffect();
    }
}
