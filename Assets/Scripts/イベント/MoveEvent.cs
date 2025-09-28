using UnityEngine;
using Platformer.Core;
using UnityEngine.AI;

namespace Platformer.Events
{
    public class MoveEvent : Simulation.Event<MoveEvent>
    {
        public GameObject target;
        public MoveStateType moveStateType;
        public override void Execute()
        {
            NavMeshAgent navMesh = target.GetComponent<NavMeshAgent>();
            navMesh.SetDestination(model.positionManager.GetPosition(moveStateType).position);
        }
    }
}