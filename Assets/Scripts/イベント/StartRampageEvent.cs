using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class StartRampageEvent : Simulation.Event<StartRampageEvent>
    {
        public GameObject target;

        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "おりゃあああああああ";
            Animator anim = target.GetComponent<Worker>().anim;
            anim.SetInteger("ID", (int)Worker.AnimState.回転);
            
            model.effectManager.InstantiateEffect(0, target.transform, 2);

            //タスク全消失してしまうので今後それぞれに対応したものだけ消すようにする
            //Simulation.Clear();

            var ev = Simulation.Schedule<StopRampageEvent>(2);
            ev.target = target;
        }
    }
}