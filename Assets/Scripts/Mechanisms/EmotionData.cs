using UnityEngine;

[System.Serializable]
public class EmotionData
{
    public EmotionType emotion{ get; private set; }
    private float _value;
    public float value
    {
        get { return _value; }
        set { _value = Mathf.Clamp01(value); }
    }

    public EmotionData(EmotionType initialEmotion)
    {
        emotion = initialEmotion;
        value = 0.5f; //0~1で変化
    }

    public void SetValue(float newValue)
    {
        value = Mathf.Clamp01(newValue);
    }
}

public enum EmotionType
    {
        喜,
        怒,
        哀,
        楽
    }
//とりあえず、うれしいと楽しいはgood,怒りと悲しいはbadに分ける
//感情は、workerの行動に影響を与える
