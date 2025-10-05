using UnityEngine;
using Platformer.Core;
using UnityEngine.AI;

namespace Platformer.Events
{
    public class DeadPlayer : Simulation.Event<DeadPlayer>
    {
        public GameObject target;
        public override void Execute()
        {
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();
            //個人のタスク全消し
            scheduler.CancelAllTasks();

            var spev = scheduler.Schedule<SpeakEvent>();
            spev.str = "ぐえーしんだンゴーｗ";
            spev.target = target;

            //移動を中止
            NavMeshAgent navMesh = target.GetComponent<NavMeshAgent>();
            navMesh.velocity = Vector3.zero;
            navMesh.avoidancePriority = 99;
            navMesh.enabled = false;


            //死亡アニメーション開始
            AnimatorController animatorController = target.GetComponent<AnimatorController>();
            animatorController.ChangeAnimState((int)Worker.AnimState.Death);
        }
    }
}