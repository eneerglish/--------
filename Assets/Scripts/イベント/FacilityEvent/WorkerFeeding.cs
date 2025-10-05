using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class WorkerFeeding : Simulation.Event<WorkerFeeding>
    {
        public GameObject target;
        public FarmSpace facility;

        public override void Execute()
        {
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();
            var ev = scheduler.Schedule<ObjectThrowing>();
            ev.throwObject = facility.GetFoodFromStorage();
            ev.startPos = target.transform.position + new Vector3(0, 0.2f, 0);
            ev.targetPos = facility.GetRandomTransform().position;
        }
    }
}