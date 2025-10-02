using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class GetUpEvent : Simulation.Event<GetUpEvent>
    {
        public GameObject target;
        public HomeSpace facility;
        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "おはよう！";
            Animator anim = target.GetComponent<Animator>();
            anim.SetInteger("ID", (int)Worker.AnimState.Move);

            target.GetComponent<WorkerState>().ChangeFollowState(FollowStateType.待機);
        }
    }
}