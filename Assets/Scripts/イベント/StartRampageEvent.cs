using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartRampageEvent : Simulation.Event<StartRampageEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            Simulation.Clear();

            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "おりゃあああああああ";
            AnimatorController animatorController = target.GetComponent<AnimatorController>();
            animatorController.ChangeAnimState((int)Worker.AnimState.Rotation);
            
            model.effectManager.InstantiateEffect(0, target.transform, 2);


            var ev = Simulation.Schedule<StopRampageEvent>(2);
            ev.target = target;
        }
    }
}