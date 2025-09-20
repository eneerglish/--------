using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    /// <summary>
    /// ワーカーが生産するときに呼ばれるイベント
    /// </summary>
    public class GanerateProductionEvent : Simulation.Event<GanerateProductionEvent>
    {
        public Vector3 pos;
        public override void Execute()
        {
            //GameObject productPrefab = model.productionList.GetProduction();
            //GameObject item = Instantiate(productPrefab, pos, Quaternion.identity);
        }
    }
}