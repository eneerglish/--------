using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    /// <summary>
    /// ワーカーが生産するときに呼ばれるイベント
    /// </summary>
    public class GanerateProduction : Simulation.Event<GanerateProduction>
    {
        public override void Execute()
        {
            Debug.Log("GanerateProduction");
        }
    }
}