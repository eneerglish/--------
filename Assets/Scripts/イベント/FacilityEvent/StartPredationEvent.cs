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
            var spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "食べられるーーー";
            //facilityは敵

            model.workerManager.DestroyWorker(target);
        }
    }
}