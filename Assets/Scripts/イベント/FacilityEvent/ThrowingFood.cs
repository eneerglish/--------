using UnityEngine;
using Platformer.Core;
using DG.Tweening;

namespace Platformer.Events
{
    public class ThrowingFood : Simulation.Event<ThrowingFood>
    {
        public GameObject target;
        public FarmSpace facility;

        public override void Execute()
        {
            GameObject food = facility.GenerateFood(target.transform.position);
            Transform pos = facility.GetRandomTransform();
            facility.Feding(target, pos, food);
        }
    }
}