using UnityEngine;
using Platformer.Core;
using UnityEngine.AI;

namespace Platformer.Events
{
    public class FinishFacilityEvent : Simulation.Event<FinishFacilityEvent>
    {
        public GameObject target;
        public ActionData actionData;
        public override void Execute()
        {
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();
            SpeakEvent spev = scheduler.Schedule<SpeakEvent>();
            spev.str = actionData.finishActionText;
            spev.target = target;

            AnimatorController animatorController = target.GetComponent<AnimatorController>();
            animatorController.ChangeAnimState((int)Worker.AnimState.Move);

            var ev = scheduler.Schedule<ChangeStateEvent>();
            ev.target = target;
            ev.newState = FollowStateType.待機;

        }
    }
}