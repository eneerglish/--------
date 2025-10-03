using UnityEngine;
using Platformer.Core;
using Platformer.Events;
public class WorkerState : GameAwareBehaviour
{
    //ワーカーの状態はこのすくりぷとで管理
    public ActiveState activeState { get; private set; }
    public FollowStateType followStateType { get; private set; }
    public MoveStateType moveStateType { get; private set; }

    public void SetActiveState(ActiveState state)
    {
        activeState = state;
    }
    public void SetFollowStateType(FollowStateType state)
    {
        followStateType = state;
        SetActiveState(ActiveState.FollowStateType);
    }

    public void SetMoveStateType(MoveStateType state)
    {
        moveStateType = state;
        SetActiveState(ActiveState.MoveStateType);
        var ev = Simulation.Schedule<MoveEvent>();
        ev.target = this.gameObject;
        ev.moveStateType = state;
    }
}