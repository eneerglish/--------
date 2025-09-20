using UnityEngine;
using Platformer.Core;
using Platformer.Events;
public class WorkerState : GameAwareBehaviour
{
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
    }

    public void SetMoveStateType(MoveStateType state)
    {
        moveStateType = state;
    }

    public void ChangeFollowState(FollowStateType state)
    {
        var ev = Simulation.Schedule<ChangeStateEvent>();
        ev.target = this.gameObject;
        ev.newState = state;
        SetFollowStateType(state);
    }
}