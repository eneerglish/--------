using UnityEngine;
using Platformer.Core;
using UnityEngine.AI;

namespace Platformer.Events
{
    public class PlayWaterEvent : Simulation.Event<PlayWaterEvent>
    {
        public GameObject target;
        public Facility facility;
        public override void Execute()
        {
            SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
            spev.str = "水であそぼ";

            var ev = Simulation.Schedule<FinishWaterEvent>(5);
            ev.target = target;
            ev.facility = facility as WaterSpace;
        }
    }
}