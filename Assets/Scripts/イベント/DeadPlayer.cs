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
            //タスク全消失
            Simulation.Clear();

            var spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "ぐえーしんだンゴーｗ";

            //移動を中止
            NavMeshAgent navMesh = target.GetComponent<NavMeshAgent>();
            navMesh.velocity = Vector3.zero;
            navMesh.enabled = false;

            //死亡アニメーション開始
            AnimatorController animatorController = target.GetComponent<AnimatorController>();
            animatorController.ChangeAnimState((int)Worker.AnimState.Death);
        }
    }
}