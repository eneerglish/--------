using UnityEngine;
using Platformer.Core;
using Platformer.Events;
public class Test : GameAwareBehaviour
{
    void Start()
    {
        model.workerManager.InstantiateWorker();
        //GeneEnemy(30);
    }

    public void GeneEnemy(int time = 0)
    {
        var ev = Simulation.Schedule<ApeerEnemyEvent>(time);
        ev.transform = model.enemyAppertransform;
    }
}
