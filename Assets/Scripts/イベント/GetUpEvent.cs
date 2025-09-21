using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class GetUpEvent : Simulation.Event<GetUpEvent>
    {
        public GameObject target;
        public override void Execute()
        {
            Debug.Log("おはよう！");
            Animator anim = target.GetComponent<Animator>();
            //anim.SetBool("IsSleep", false);

            target.GetComponent<WorkerState>().ChangeFollowState(FollowStateType.待機);
        }
    }
}