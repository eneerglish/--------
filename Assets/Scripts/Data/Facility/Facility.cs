using System;
using System.Collections.Generic;
using Platformer.Core;
using UnityEngine;
using Platformer.Events;
public abstract class Facility : GameAwareBehaviour
{

    //ほんとはEventScriptを直接設定したいが、できないので列挙型で設定する
    [Header("イベント設定")]
    [SerializeField]
    protected FollowStateType startstate;

    //ワーカーが施設に入ってきたとき行いたいことを書く
    public abstract void DoStartProcess(GameObject target, Facility facility);

}