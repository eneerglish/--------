using UnityEngine;
using System.Collections.Generic;
public class WorkerEmotion : GameAwareBehaviour
{
    public List<EmotionData> emotionList = new List<EmotionData>();
    //リストの一番上が現在の感情
    public EmotionData nowEmotion
    {
        get
        {
            return emotionList[0];
        }
    }
    public WorkerEmotion()
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
