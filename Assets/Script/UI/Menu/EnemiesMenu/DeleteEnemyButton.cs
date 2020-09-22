using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class DeleteEnemyButton : MenuButton
    {
        protected override ButtonManager.ButtonEffectType SetButtonEffect()
        {
            return ButtonManager.ButtonEffectType.NoTextEffect;
        }

        private void DeleteEnemyContainer()
        {
            EnemyContainer container = Transform.parent.GetComponent<EnemyContainer>();
            GameManager.Instance.EnemyContainerManager.DeleteEnemyContainer(container);
        }

        public override void PressAction()
        {
            DeleteEnemyContainer();
        }

        public override void ReleseAction()
        {

        }
    }
}