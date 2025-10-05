using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartRampageEvent : Simulation.Event<StartRampageEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();
            scheduler.CancelAllTasks();

            SpeakEvent spev = scheduler.Schedule<SpeakEvent>();
            spev.str = "おりゃあああああああ";
            spev.target = target;

            AnimatorController animatorController = target.GetComponent<AnimatorController>();
            animatorController.ChangeAnimState((int)Worker.AnimState.Rotation);
            
            model.effectManager.InstantiateEffect(0, target.transform, 2);


            var ev = scheduler.Schedule<StopRampageEvent>(2);
            ev.target = target;
        }
    }
}