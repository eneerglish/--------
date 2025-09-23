using UnityEngine;
using Platformer.Core;
using Platformer.Events;

public class WaterSpace : Facility
{
    public override void DoStartProcess(GameObject target, Facility facility )
    {
        target.GetComponent<WorkerState>().ChangeFollowState(startstate, facility);
    }
}