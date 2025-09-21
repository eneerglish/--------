using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class SleepEvent : Simulation.Event<SleepEvent>
    {
        public GameObject target;
        public override void Execute()
        {
            Debug.Log("ねむねむzzz");
            Animator anim = target.GetComponent<Animator>();
            //anim.SetBool("IsSleep", true);

            var ev = Simulation.Schedule<GetUpEvent>(5);
            ev.target = target;
        }
    }
}