using UnityEngine;


public enum ActiveState
{
    FollowStateType,
    MoveStateType
}
public enum FollowStateType
{
    待機,
    生産,
    運搬,
    睡眠,
    暴走
}

public enum MoveStateType
{
    生産所へ,
    水辺へ,
    家へ
}


