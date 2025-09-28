using UnityEngine;
using Platformer.Core;
using Platformer.Events;
public class Test : GameAwareBehaviour
{
    void Start()
    {
        model.workerManager.InstantiateWorker();
        //var ev = Simulation.Schedule<ApeerEnemyEvent>(30);
        //ev.transform = model.enemyAppertransform;
    }
}
