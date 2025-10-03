using System;
using System.Collections.Generic;
using Platformer.Core;
using UnityEngine;
using Platformer.Events;
public abstract class Facility : GameAwareBehaviour
{
    [field: SerializeField]
    protected List<ActionData> actionData = new List<ActionData>();

    [field: SerializeField]
    protected MoveStateType humanMoveState { get; private set; }

    //ワーカーが施設に入ってきたとき行いたいことを書く
    public virtual void DoStartProcess(GameObject target)
    {
        var ev = Simulation.Schedule<ChangeStateEvent>();
        ev.target = target;
        ev.newState = actionData[0].followStateType;
        ev.actionData = actionData[0];

    }

    public virtual void HumanStartProcess(Human human)
    {
        //人間が施設に入ってきたとき行いたいことを書く
    }

    public ActionData GetActionData(int i = 0)
    {
        return actionData[i];
    }

}