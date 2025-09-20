using UnityEngine;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;
public class Worker : GameAwareBehaviour
{
    public Animator anim;
    public WorkerState workerState;
    public WorkerEmotion workerEmotion;

    #region Data
    public string workerName = "Worker";
    public int workerID = 0; 
    float sleapValue = 0f; //0~1

    float productionSpeed = 3f;//生産にかかる時間

    float moveSpeed = 3f;//移動速度

    float sleepDesireUpRate = 10f;//眠くなる速度
    float sleapDuration = 5f;//睡眠時間
    #endregion

    public void InitSet()
    {
        anim = GetComponent<Animator>();
        workerState = GetComponent<WorkerState>();
        workerEmotion = GetComponent<WorkerEmotion>();
        workerID = GetInstanceID();
        workerName = "Worker" + workerID.ToString();
        productionSpeed = Random.Range(2f, 5f);
        moveSpeed = Random.Range(2f, 5f);
        sleepDesireUpRate = Random.Range(5f, 15f);
        sleapDuration = Random.Range(3f, 7f);
    }

    public void 仮ChangeState()
    {
        if (workerState.followStateType == FollowStateType.待機)
        {
            workerState.ChangeFollowState(FollowStateType.生産);
        }
    }

    void Start()
    {
        InitSet();
    }
    private void Update()
    {

        if (workerState.followStateType != FollowStateType.睡眠)
        {
            sleapValue += Time.deltaTime / sleepDesireUpRate;
            if (sleapValue >= 1f)
            {
                sleapValue = 0f;
                workerState.ChangeFollowState(FollowStateType.睡眠);
            }
        }


        switch (workerState.followStateType)
        {
            case FollowStateType.待機:
                workerEmotion.GetEmotion(EmotionType.喜).value += Time.deltaTime / 50f;
                workerEmotion.GetEmotion(EmotionType.怒).value -= Time.deltaTime / 100f;
                break;

            case FollowStateType.生産:
                
                workerEmotion.GetEmotion(EmotionType.喜).value -= Time.deltaTime / 1000f;
                workerEmotion.GetEmotion(EmotionType.怒).value += Time.deltaTime / 200f;
                break;

            case FollowStateType.運搬:
                workerEmotion.GetEmotion(EmotionType.喜).value -= Time.deltaTime / 1000f;
                workerEmotion.GetEmotion(EmotionType.怒).value += Time.deltaTime / 200f;
                break;
            case FollowStateType.睡眠:
                workerEmotion.GetEmotion(EmotionType.喜).value += Time.deltaTime / 100f;
                workerEmotion.GetEmotion(EmotionType.怒).value -= Time.deltaTime / 200f;
                break;
            case FollowStateType.暴走:
                sleapValue += Time.deltaTime / (sleepDesireUpRate / 2);
                workerEmotion.GetEmotion(EmotionType.喜).value += Time.deltaTime / 100f;
                workerEmotion.GetEmotion(EmotionType.怒).value -= Time.deltaTime / 200f;
                break;
        }

        if (workerEmotion.SortEmotionAndCheckChange())
        {
            switch (workerEmotion.nowEmotion.emotion)
            {
                case EmotionType.喜:
                    Debug.Log("わーい");
                    break;
                case EmotionType.怒:
                    Debug.Log("イライラ");
                    break;
                case EmotionType.哀:
                    Debug.Log("カナシイ");
                    break;
                case EmotionType.楽:
                    Debug.Log("ﾀﾉｼｰ");
                    break;
                default:
                    Debug.Log("何この感情");
                    break;
            }
        }

        /*if (workerState.followStateType != FollowStateType.待機)
        {
            return;
        }*/
        if (workerEmotion.nowEmotion.value >= 1f)
            {
                EmotionType dominantEmotion = workerEmotion.nowEmotion.emotion;
                switch (dominantEmotion)
                {
                    case EmotionType.喜:
                    case EmotionType.楽:
                        Debug.Log("働きたくなってきたー！");
                        workerState.ChangeFollowState(FollowStateType.生産);
                        break;
                    case EmotionType.怒:
                    case EmotionType.哀:
                        Debug.Log("もうやあだー");
                        workerState.ChangeFollowState(FollowStateType.暴走);
                        break;
                }
            }
    }
}
