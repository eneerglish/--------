using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StopProduceEvent : Simulation.Event<StopProduceEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            Debug.Log("頑張ったー");
            Animator anim = target.GetComponent<Animator>();
            //anim.SetBool("IsProduce", false);

            ProductionSpace p = target.GetComponent<Worker>().facility.GetComponent<ProductionSpace>();
            p.ProduceItem();
            target.GetComponent<WorkerState>().ChangeFollowState(FollowStateType.待機);
            
        }
    }
}