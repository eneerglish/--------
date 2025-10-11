using UnityEngine;
using System.Collections.Generic;

public enum MoveStateType
{
    生産所へ,
    水辺へ,
    家1へ,
    牧場へ,
    木へ,
    鉱山へ,
    食べ物探しへ,
    なし
}


public class PositionManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> posList = new List<Transform>();

    private Dictionary<MoveStateType, Transform> posDic = new Dictionary<MoveStateType, Transform>();

    void Awake()
    {
        for (int i = 0; i < posList.Count; i++)
        {
            posDic.Add((MoveStateType)i, posList[i]);
        }
    }

    public Transform GetPosition(MoveStateType type)
    {
        if (posDic.TryGetValue(type, out Transform pos))
        {
            return pos;
        }
        else
        {
            Debug.LogError("指定されたMoveStateTypeに対応する位置が見つかりません: " + type);
            return null;
        }
    }
}
