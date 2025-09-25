using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class EnemyChaseEvent : Simulation.Event<EnemyChaseEvent>
    {
        public Enemy enemy;
        public override void Execute()
        {
            enemy.SetTarget(model.workerManager.GetWorker());
            enemy.ChangeState(Enemy.EnemyState.追跡);
            model.cameraManager.SwitchCamera(0);
            enemy.navMesh.enabled = true;
        }
    }
}