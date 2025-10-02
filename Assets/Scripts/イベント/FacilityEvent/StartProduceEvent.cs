using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartProduceEvent : Simulation.Event<StartProduceEvent>
    {
        public GameObject target;
        public ProductionSpace facility;

        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "生産するぞー";
            Animator anim = target.GetComponent<Animator>();
            //anim.SetInteger("ID", (int)Worker.AnimState.食べる);
            for(int i = 1; i <= 5; i++)
            {
                var ev = Simulation.Schedule<ProduceItemEvent>(i);
                ev.facility = facility;
            }
            {
                var ev = Simulation.Schedule<StopProduceEvent>(5);
                ev.target = target;
                ev.facility = facility as ProductionSpace;
            }
        }
    }
}