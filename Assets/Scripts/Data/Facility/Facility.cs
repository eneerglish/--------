using System;
using System.Collections.Generic;
using Platformer.Core;
using UnityEngine;
using Platformer.Events;
public abstract class Facility : GameAwareBehaviour
{

    //ほんとはEventScriptを直接設定したいが、できないので列挙型で設定する
    [Header("イベント設定")]
    [field: SerializeField]
    protected ActionData actionData{ get; private set; }

    [Header("人間用")]
    [field: SerializeField]
    protected MoveStateType humanMoveState{get; private set; }

    //ワーカーが施設に入ってきたとき行いたいことを書く
    //public abstract void DoStartProcess(GameObject target, Facility facility);

    public virtual void HumanStartProcess(Human human)
    {
        //人間が施設に入ってきたとき行いたいことを書く
    }

}