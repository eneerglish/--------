using UnityEngine;
using Platformer.Core;
using Platformer.Events;
public class Test : GameAwareBehaviour
{
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            var ev = Simulation.Schedule<SpawnWorker>();
            ev.startPos = model.workerManager.playerSpawnPoint;
        }

        //GeneEnemy(30);
    }

    public void GeneEnemy(int time = 0)
    {
        var ev = Simulation.Schedule<ApeerEnemyEvent>(time);
        ev.transform = model.enemyAppertransform;
    }
}
