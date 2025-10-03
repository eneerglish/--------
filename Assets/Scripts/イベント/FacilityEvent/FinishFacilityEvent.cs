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
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = actionData.finishActionText;

            AnimatorController animatorController = target.GetComponent<AnimatorController>();
            animatorController.ChangeAnimState((int)Worker.AnimState.Move);

            var ev = Simulation.Schedule<ChangeStateEvent>();
            ev.target = target;
            ev.newState = FollowStateType.待機;

        }
    }
}