using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StopProduceEvent : Simulation.Event<StopProduceEvent>
    {
        public GameObject target;
        public ProductionSpace facility;
        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "頑張ったー";
            Animator anim = target.GetComponent<Animator>();
            //anim.SetBool("IsProduce", false);

            facility.ProduceItem();
            target.GetComponent<WorkerState>().ChangeFollowState(FollowStateType.待機);
            
        }
    }
}