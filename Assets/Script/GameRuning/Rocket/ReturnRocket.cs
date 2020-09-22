using UnityEngine;
namespace Assets.Script.Rocket
{
    public class ReturnRocket : RocketBase
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if (StateMachine.State!=StateEnum.Flying) return;
            
            Enemy other = col.gameObject.GetComponent<Enemy>();
            if (other == null)
            {
                Debug.Log("撞上了不是敌人的东西");
                return;
            }
            
            other.TurnAround();
            Destroy(gameObject);
        }
    }
}