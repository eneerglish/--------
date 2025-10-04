using UnityEngine;
using Platformer.Core;
using UnityEngine.AI;

namespace Platformer.Events
{
    public class MoveEvent : Simulation.Event<MoveEvent>
    {
        public GameObject target;
        public Transform transform;
        public override void Execute()
        {
            NavMeshAgent navMesh = target.GetComponent<NavMeshAgent>();
            navMesh.SetDestination(transform.position);
        }
    }
}