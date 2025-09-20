using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartProduceEvent : Simulation.Event<StartProduceEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            Debug.Log("生産するぞー！");
            Animator anim = target.GetComponent<Animator>();
            //anim.SetBool("IsProduce", true);

            var ev = Simulation.Schedule<StopProduceEvent>(5);
            ev.target = target;
        }
    }
}