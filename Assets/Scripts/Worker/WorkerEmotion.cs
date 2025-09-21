using UnityEngine;
using System.Collections.Generic;
public class WorkerEmotion : GameAwareBehaviour
{
    //ワーカーの感情はこのスクリプトで管理する
    public List<EmotionData> emotionList = new List<EmotionData>();
    public EmotionData nowEmotion
    {
        get
        {
            return emotionList[0];
        }
    }
    void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(EmotionType)).Length; i++)
        {
            emotionList.Add(new EmotionData((EmotionType)i));
        }
    }

    public EmotionData GetEmotion(EmotionType type)
    {
        return emotionList.Find(e => e.emotion == type);
    }
    public bool SortEmotionAndCheckChange()
    {
        EmotionData oldEmotion = emotionList[0];
        emotionList.Sort((a, b) => b.value.CompareTo(a.value));
        if (oldEmotion != nowEmotion)
        {
            return true;
        }
        return false;
    }


}
