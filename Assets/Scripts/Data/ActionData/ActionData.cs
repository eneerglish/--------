using UnityEngine;
using System.Collections.Generic;
using System;

//ワーカーのアクションをScriptableObjectで管理する
//複数ある施設のイベントをこれでまとめたい
[CreateAssetMenu(fileName = "ActionData", menuName = "ActionData", order = 0)]
public class ActionData : ScriptableObject
{
    [field: SerializeField]
    public AnimationClip animClip { get; private set; }
    [field: SerializeField]
    public string startActionText { get; private set; }
    [field: SerializeField]
    public string finishActionText { get; private set; }
    [field: SerializeField]
    public FollowStateType followStateType { get; private set; }

    [field: SerializeField]
    public int actionTime { get; private set; }

    [field: SerializeField]
    private List<EmotionModifier> emotionModifiers = new List<EmotionModifier>();

    public List<EmotionModifier> GetEmotionModifiers()
    {
        return emotionModifiers;
    }
}

[Serializable]
public class EmotionModifier
{
    public EmotionType emotionType;
    public float value;
}