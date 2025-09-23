using UnityEngine;
using Platformer.Core;
using UnityEngine.AI;

namespace Platformer.Events
{
    public class FinishWaterEvent : Simulation.Event<FinishWaterEvent>
    {
        public GameObject target;
        public WaterSpace facility;
        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "水やめうｒ";

            target.GetComponent<WorkerState>().ChangeFollowState(FollowStateType.待機);
        }
    }
}