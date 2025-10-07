using UnityEngine;


public enum ActiveState
{
    FollowStateType,
    MoveStateType
}
public enum FollowStateType
{
    待機,
    水遊び,
    生産,
    運搬,
    お腹すいた,
    餌やり,
    水やり,
    睡眠,
    暴走,
    被捕食,
    死亡,
    スポーン
}

public enum MoveStateType
{
    生産所へ,
    水辺へ,
    家へ,
    牧場へ,
    木へ,
    食べ物探しへ,
    なし
}


