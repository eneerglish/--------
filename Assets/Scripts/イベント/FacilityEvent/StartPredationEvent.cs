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

            //facilityは敵

            model.workerManager.DestroyWorker(target);
            Simulation.Clear();
            enemy.SetTarget(model.workerManager.GetWorker());

            var spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "食べられるーーー";
            
        }
    }
}