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
            Debug.Log($"{moveStateType}行くぞ");
            NavMeshAgent navMesh = target.GetComponent<Worker>().navMesh;
            navMesh.SetDestination(model.positionManager.posList[(int)moveStateType].position);
        }
    }
}