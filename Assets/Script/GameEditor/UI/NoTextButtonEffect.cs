namespace GameEditor
{
    public class NoTextButtonEffect : IButtonEffect
    {
        private ButtonBase Button { get; }

        public NoTextButtonEffect(ButtonBase button)
        {
            Button = button;
        }

        public void CancelEffect()
        {

        }

        public void EnterEffect()
        {

        }

        public void ExitEffect()
        {

        }

        public void PressEffect()
        {

        }

        public void ReleaseEffect()
        {

        }

        public void SelectedEffect()
        {

        }
    }
}