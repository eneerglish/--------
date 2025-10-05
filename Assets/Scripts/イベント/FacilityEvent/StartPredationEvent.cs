using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartPredationEvent : Simulation.Event<StartPredationEvent>
    {
        public GameObject target;
        public Enemy enemy;

        public override void Execute()
        {
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();
            //facilityは敵

            model.workerManager.DestroyWorker(target);
            scheduler.CancelAllTasks();
            enemy.SetTarget(model.workerManager.GetWorker());

            var spev = scheduler.Schedule<SpeakEvent>();
            spev.str = "食べられるーーー";
            spev.target = target;
            
        }
    }
}