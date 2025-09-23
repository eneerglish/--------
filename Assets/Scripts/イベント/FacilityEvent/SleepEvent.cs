using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class SleepEvent : Simulation.Event<SleepEvent>
    {
        public GameObject target;
        public Facility facility;
        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "ねむねむzzz";
            Animator anim = target.GetComponent<Animator>();
            //anim.SetBool("IsSleep", true);

            var ev = Simulation.Schedule<GetUpEvent>(5);
            ev.target = target;
            ev.facility = facility as HomeSpace;
        }
    }
}