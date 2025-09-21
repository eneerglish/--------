using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class SpeakEvent : Simulation.Event<SpeakEvent>
    {
        public string str;
        public override void Execute()
        {
            model.text.enabled = true;
            model.text.text = str;

            var ev = Simulation.Schedule<SpeakDisableEvent>(5);
        }
    }
}