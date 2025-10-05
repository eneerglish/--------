using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class SpeakDisableEvent : Simulation.Event<SpeakDisableEvent>
    {
        public GameObject target;
        public override void Execute()
        {
            model.text.enabled = false;
        }
    }
}