using UnityEngine;
namespace Assets.Script.Rocket
{
    public class ReturnRocket : RocketBase
    {
        protected override void OnTriggerStay2D(Collider2D col)
        {
            if (StateMachine.State!=StateEnum.Flying) return;
            
            StateMachine.State = StateEnum.Hit;
            Enemy other = col.gameObject.GetComponent<Enemy>();
            if (other == null)
            {
                Debug.Log("撞上了不是敌人的东西");
                return;
            }
            other.TurnAround();
            GameManager.PlaySound(_returnSound);
            Destroy(gameObject);
        }
    }
}