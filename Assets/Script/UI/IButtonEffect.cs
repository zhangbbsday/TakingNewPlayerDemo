public enum ButtonState
{
    None,
    Enter,
    Exit,
    Press,
    Relese,
    Selected,
    Cancel,
}

public interface IButtonEffect
{
    void EnterEffect();
    void ExitEffect();
    void PressEffect();
    void ReleseEffect();
    void SelectedEffect();
    void CancelEffect();
}
