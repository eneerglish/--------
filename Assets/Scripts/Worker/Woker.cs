using UnityEngine;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;
using UnityEngine.AI;
public class Worker : GameAwareBehaviour
{
    public Animator anim;
    public NavMeshAgent navMesh;
    public WorkerState workerState;
    public WorkerEmotion workerEmotion;
    public GameObject facility;

    #region Data
    public string workerName = "Worker";
    public int workerID = 0; 
    float sleapValue = 0f; //0~1

    float productionSpeed = 3f;//生産にかかる時間

    float moveSpeed = 3f;//移動速度

    float sleepDesireUpRate = 10f;//眠くなる速度
    float sleapDuration = 5f;//睡眠時間
    float boaredValue = 0f; //0~1
    float boaredSpead = 5f;
    #endregion

    public void InitSet()
    {
        anim = GetComponentInChildren<Animator>();
        workerState = GetComponent<WorkerState>();
        workerEmotion = GetComponent<WorkerEmotion>();
        navMesh = GetComponent<NavMeshAgent>();
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

    public void 仮move()
    {
        workerState.SetMoveStateType(MoveStateType.生産所へ);
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
            if (sleapValue >= 1f && workerState.moveStateType != MoveStateType.家へ)
            {
                sleapValue = 0f;
                workerState.SetMoveStateType(MoveStateType.家へ);
            }
        }

        //暇なとき適当な場所に行くようにしたい
        if (workerState.activeState == ActiveState.FollowStateType && workerState.followStateType == FollowStateType.待機)
        {
            boaredValue += Time.deltaTime / boaredSpead;
            if (boaredValue >= 1f)
            {
                boaredValue = 0f;

                MoveStateType[] destinations = {
                    MoveStateType.牧場へ,
                    MoveStateType.水辺へ
                };

                int randomIndex = Random.Range(0, destinations.Length);

                workerState.SetMoveStateType(destinations[randomIndex]);
            }
        }

        if (workerState.activeState == ActiveState.FollowStateType)
            {
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
            }
            else if (workerState.activeState == ActiveState.MoveStateType)
            {
                switch (workerState.moveStateType)
                {
                    case MoveStateType.生産所へ:
                        break;
                    case MoveStateType.水辺へ:
                        break;
                    case MoveStateType.家へ:
                        break;
                    case MoveStateType.牧場へ:
                        break;
                }
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
                    if (workerState.activeState == ActiveState.FollowStateType)
                    {
                        Debug.Log("働きたくなってきたー！");
                        workerState.SetMoveStateType(MoveStateType.生産所へ);
                    }

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
