using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class ResetButtonEvent : Simulation.Event<ResetButtonEvent>
    {
        public GameObject button;
        public override void Execute()
        {
            button.SetActive(true);

        }

    }
}