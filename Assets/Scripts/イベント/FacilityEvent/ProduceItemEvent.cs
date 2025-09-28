using Platformer.Core;

namespace Platformer.Events
{
    public class ProduceItemEvent : Simulation.Event<ProduceItemEvent>
    {
        public ProductionSpace facility;

        public override void Execute()
        {

            facility.ProduceItem();

        }
    }
}