using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartFedingEvent : Simulation.Event<StartFedingEvent>
    {
        public GameObject target;
        public FarmSpace facility;

        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "えさやるぞー";

            for (int i = 0; i < 5; i++)
            {
                var foodev = Simulation.Schedule<ThrowingFood>(0.5f * i);
                foodev.target = target;
                foodev.facility = facility;
            }

            var ev = Simulation.Schedule<FinishFedingEvent>(5);
            ev.target = target;
        }
    }
}