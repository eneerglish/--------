using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
public class WorkerEmotion : GameAwareBehaviour
{
    //ワーカーの感情はこのスクリプトで管理する
    NavMeshAgent navMesh;
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
        navMesh = GetComponent<NavMeshAgent>();
        for (int i = 0; i < System.Enum.GetValues(typeof(EmotionType)).Length; i++)
        {
            emotionList.Add(new EmotionData((EmotionType)i));
        }
    }

    void Update()
    {
        navMesh.speed = GetEmotion(EmotionType.喜).value *2f;
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
