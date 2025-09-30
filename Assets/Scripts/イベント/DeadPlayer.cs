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
            Worker worker = target.GetComponent<Worker>();
            worker.anim.SetInteger("ID", (int)Worker.AnimState.死亡);
            worker.navMesh.velocity = Vector3.zero;
            worker.navMesh.enabled = false;

            Simulation.Clear();
        }
    }
}