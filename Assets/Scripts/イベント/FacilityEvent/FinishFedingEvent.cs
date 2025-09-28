using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class FinishFedingEvent : Simulation.Event<FinishFedingEvent>
    {
        public GameObject target;

        public override void Execute()
        {

            target.GetComponent<WorkerState>().ChangeFollowState(FollowStateType.待機);
        }
    }
}