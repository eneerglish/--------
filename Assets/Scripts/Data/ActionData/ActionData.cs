using UnityEngine;

//ワーカーのアクションをScriptableObjectで管理する
//複数ある施設のイベントをこれでまとめたい
[CreateAssetMenu(fileName = "ActionData", menuName = "ActionData", order = 0)]
public class ActionData : ScriptableObject
{
    [field: SerializeField]
    public AnimationClip animClip { get; private set; }
    [field: SerializeField]
    public string actionText { get; private set; }
    [field: SerializeField]
    public FollowStateType followStateType { get; private set; }
}