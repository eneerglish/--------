using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    /// <summary>
    /// ワーカーが生産するときに呼ばれるイベント
    /// </summary>
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