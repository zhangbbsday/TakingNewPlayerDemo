using UnityEngine;

namespace GameEditor
{
    public class BuildButton : MenuButton
    {
        enum ButtonType
        {
            Read,
            Write,
        }

        [SerializeField]
        private FunctionMenu menu;
        [SerializeField]
        private ButtonType buttonType;
        private bool IsClosed { get; set; }
        private bool IsStartChecking { get; set; }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            if (!IsStartChecking)
                return;

            if (!menu.GameObject.activeSelf && !IsClosed)
            {
                IsClosed = true;
                IsStartChecking = false;
                ButtonEffect.CancelEffect();
            }
        }

        public override void PressAction()
        {
            menu.Show();
            IsStartChecking = true;
            InAction();
        }

        public override void ReleseAction()
        {
            menu.Close();
            IsClosed = false;
            IsStartChecking = false;
            OutAction();
        }

        private void InAction()
        {
            switch (buttonType)
            {
                case ButtonType.Read:
                    ReadFilesIn();
                    break;
                case ButtonType.Write:
                    WriteFilesIn();
                    break;
            }
        }

        private void OutAction()
        {
            switch (buttonType)
            {
                case ButtonType.Read:
                    ReadFilesOut();
                    break;
                case ButtonType.Write:
                    WriteFilesOut();
                    break;
            }
        }

        private void ReadFilesIn()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.FileContainerManager.CreateAllFileContainers();
        }

        private void ReadFilesOut()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.FileContainerManager.DeleteAllFileConatiner();
        }

        private void WriteFilesIn()
        {

        }

        private void WriteFilesOut()
        {

        }
    }
}