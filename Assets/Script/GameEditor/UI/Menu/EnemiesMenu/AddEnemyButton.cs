
namespace GameEditor
{
    public class AddEnemyButton : MenuButton
    {
        protected override ButtonManager.ButtonEffectType SetButtonEffect()
        {
            return ButtonManager.ButtonEffectType.NoTextEffect;
        }

        private void CreateEnemyContainer()
        {
            GameManager.Instance.EnemyContainerManager.CreateEnemyContainer(EnemyContainerManager.EnemyType.NormalEnemy);
        }

        public override void PressAction()
        {
            CreateEnemyContainer();
        }

        public override void ReleseAction()
        {

        }
    }
}