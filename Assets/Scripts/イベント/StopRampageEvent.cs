using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StopRampageEvent : Simulation.Event<StopRampageEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();

            SpeakEvent spev = scheduler.Schedule<SpeakEvent>();
            spev.str = "ふうー、すっきりしたぜー";
            spev.target = target;

            AnimatorController animatorController = target.GetComponent<AnimatorController>();
            animatorController.ChangeAnimState((int)Worker.AnimState.Move);

            var ev = scheduler.Schedule<ChangeStateEvent>();
            ev.target = target;
            ev.newState = FollowStateType.待機;
        }
    }
}