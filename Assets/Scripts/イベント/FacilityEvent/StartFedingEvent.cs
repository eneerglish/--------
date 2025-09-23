using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartFedingEvent : Simulation.Event<StartFedingEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "えさやるぞー";

            var ev = Simulation.Schedule<FinishFedingEvent>(5);
            ev.target = target;
        }
    }
}