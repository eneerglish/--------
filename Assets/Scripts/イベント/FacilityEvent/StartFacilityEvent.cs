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
            spev.str = actionData.actionText;
            Animator anim = target.GetComponent<Animator>();
            //anim.SetInteger("ID", (int)Worker.AnimState.食べる);

            var ev = Simulation.Schedule<GetUpEvent>(5);
            ev.target = target;
        }
    }
}