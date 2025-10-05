using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class SpeakEvent : Simulation.Event<SpeakEvent>
    {
        public string str;
        public GameObject target;
        public override void Execute()
        {
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();
            model.text.enabled = true;
            model.text.text = str;

            var ev = scheduler.Schedule<SpeakDisableEvent>(5);
            ev.target = target;
        }
    }
}