using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartProduceEvent : Simulation.Event<StartProduceEvent>
    {
        public GameObject target;
        public Facility facility;

        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "生産するぞー";
            Animator anim = target.GetComponent<Animator>();
            //anim.SetBool("IsProduce", true);
            var ev = Simulation.Schedule<StopProduceEvent>(5);
            ev.target = target;
            ev.facility = facility as ProductionSpace;
        }
    }
}