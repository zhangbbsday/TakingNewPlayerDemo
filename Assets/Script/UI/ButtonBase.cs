using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ButtonManager;

public abstract class ButtonBase : UIElementBase, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    protected Transform TextTransform { get; set; }
    protected Text Text { get; set; }
    protected Button Button { get; set; }
    private IButtonEffect ButtonEffect { get; set; }

    protected override void Start()
    {
        base.Start();

        TextTransform = Transform.GetChild(0);
        Text = TextTransform.GetComponent<Text>();
        Button = GetComponent<Button>();

        ButtonEffectType effect = SetButtonEffect();
        ButtonEffect = ButtonManager.Instance.GetEffect(effect);
    }

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
        ButtonEffect?.ReleseEffect();
    }
}
