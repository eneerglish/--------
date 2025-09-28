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
            int foodcount = facility.storage.Count;
            if(foodcount <= 0)
            {
                SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
                spev.str = "えさがないよー";
                var ev = Simulation.Schedule<FinishFedingEvent>(2);
                ev.target = target;
                return;
            }
            else
            {
                SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
                spev.str = "えさやるぞー";
            }

            int feedCount = Mathf.Min(5, foodcount);
            for (int i = 0; i < feedCount; i++)
            {
                var ev = Simulation.Schedule<WorkerFeeding>(i);
                ev.target = target;
                ev.facility = facility;
            }
            {
                var ev = Simulation.Schedule<FinishFedingEvent>(5);
                ev.target = target;
            }
        }
    }
}