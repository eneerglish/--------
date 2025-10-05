using UnityEngine;
using Platformer.Core;
using UnityEngine.AI;

namespace Platformer.Events
{
    public class SpawnWorker : Simulation.Event<SpawnWorker>
    {
        public Transform startPos;
        public override void Execute()
        {
            GameObject worker = model.workerManager.InstantiateWorker(startPos);
            TaskScheduler scheduler = worker.GetComponent<TaskScheduler>();
            var ev = scheduler.Schedule<ObjectThrowing>();
            ev.targetPos = model.workerManager.playerSpawnPoint.position;
            ev.startPos = startPos.position;
            ev.throwObject = worker;


            
            var ev2 = scheduler.Schedule<ChangeStateEvent>(2);
            ev2.target = worker;
            ev2.newState = FollowStateType.待機;

        }

    }
}