using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartFacilityEvent : Simulation.Event<StartFacilityEvent>
    {
        public GameObject target;
        public ActionData actionData;
        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = actionData.startActionText;

            AnimatorController animatorController = target.GetComponent<AnimatorController>();
            animatorController.ChangeAnimationClip(actionData.animClip);
            animatorController.ChangeAnimState((int)Worker.AnimState.Action);

            float nextEventTime = actionData.actionTime;

            // もしActionTimeが0だったら、ごくわずかな遅延を入れる
            if (nextEventTime <= 0)
            {
                nextEventTime = 0.1f; 
                Debug.LogWarning("ActionTimeが0だったため、0.1秒後にイベントをスケジュールします。");
            }

            var ev = Simulation.Schedule<FinishFacilityEvent>(nextEventTime);
            ev.target = target;
            ev.actionData = actionData;

        }
    }
}