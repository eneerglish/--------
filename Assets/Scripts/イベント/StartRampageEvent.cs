using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartRampageEvent : Simulation.Event<StartRampageEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "おりゃあああああああ";
            Animator anim = target.GetComponent<Worker>().anim;
            anim.SetBool("IsRampage", true);

            var ev = Simulation.Schedule<StopRampageEvent>(2);
            ev.target = target;
        }
    }
}