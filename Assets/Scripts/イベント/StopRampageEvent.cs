using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StopRampageEvent : Simulation.Event<StopRampageEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "ふうー、すっきりしたぜー";
            Animator anim = target.GetComponent<Worker>().anim;
            anim.SetBool("IsRampage", false);


            target.GetComponent<WorkerState>().ChangeFollowState(FollowStateType.待機);
        }
    }
}