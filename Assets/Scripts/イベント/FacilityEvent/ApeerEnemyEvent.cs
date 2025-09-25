using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class ApeerEnemyEvent : Simulation.Event<ApeerEnemyEvent>
    {
        public Transform transform;
        public override void Execute()
        {
            GameObject enemyobj = model.facilityManager.InstantiateFacility(0, transform);
            Enemy enemy = enemyobj.GetComponent<Enemy>();
            enemy.FallAndLand(5f);
            enemy.ChangeState(Enemy.EnemyState.落下中);
            //model.cameraManager.SwitchCamera(1);
        }
    }
}