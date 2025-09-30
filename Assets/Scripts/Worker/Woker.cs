using UnityEngine;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;
using UnityEngine.AI;
public class Worker : GameAwareBehaviour
{
    public enum AnimState
    {
        移動_待機 = 0,
        回転 = 1,
        食べる = 2,
        死亡 = 3,
        なんか = 4

    }

    public Animator anim;
    public NavMeshAgent navMesh;
    public WorkerState workerState;
    public WorkerEmotion workerEmotion;
    public GameObject facility;

    #region Data
    public string workerName = "Worker";
    public int workerID = 0;
    public int sleapValue = 0;//3で寝る　アクション行うたびに+1
    public int lifeTime = 0; //秒数
    public float lifeValue = 0; //lifeTimeまでの経過時間

    //float productionSpeed = 3f;//生産にかかる時間

    //float moveSpeed = 3f;//移動速度

    //float sleepDesireUpRate = 10f;//眠くなる速度
    //float sleapDuration = 5f;//睡眠時間

    public int hungerValue = 0; //0~6
    float boaredValue = 0f; //0~1
    float boaredSpead = 5f;
    #endregion

    public void InitSet()
    {
        anim = GetComponent<Animator>();
        workerState = GetComponent<WorkerState>();
        workerEmotion = GetComponent<WorkerEmotion>();
        navMesh = GetComponent<NavMeshAgent>();
        workerID = GetInstanceID();
        workerName = "Worker" + workerID.ToString();
        hungerValue = 6;
        lifeTime = 60;
        lifeValue = 0;
        //productionSpeed = Random.Range(2f, 5f);
        //moveSpeed = Random.Range(2f, 5f);
        //sleepDesireUpRate = Random.Range(5f, 15f);
        //sleapDuration = Random.Range(3f, 7f);
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

    public MoveStateType GetRandomDestination()
    {
        List<MoveStateType> destinations = new List<MoveStateType>()
        {
            MoveStateType.生産所へ,
            //MoveStateType.水辺へ,
            MoveStateType.牧場へ
        };
        int randomIndex = Random.Range(0, destinations.Count);
        return destinations[randomIndex];
    }

    void Start()
    {
        InitSet();
        workerState.SetMoveStateType(MoveStateType.生産所へ);
    }
    private void Update()
    {
        //死亡状態の時はなにもしない
        if (workerState.followStateType == FollowStateType.死亡)
        {
            return;
        }

        //アニメーションの更新
        float speed = navMesh.velocity.magnitude;
        anim.SetFloat("speed", speed);

        //寿命のカウント
        lifeValue += Time.deltaTime;
        if (lifeValue >= lifeTime)
        {
            workerState.ChangeFollowState(FollowStateType.死亡);
        }

        //眠かったら家へ
        if (workerState.followStateType == FollowStateType.待機 && sleapValue >= 3 && workerState.moveStateType != MoveStateType.家へ)
        {
            sleapValue = 0;
            workerState.SetMoveStateType(MoveStateType.家へ);
        }

        //暇なとき適当な場所に行くようにしたい
        if (workerState.activeState == ActiveState.FollowStateType && workerState.followStateType == FollowStateType.待機)
        {
            boaredValue += Time.deltaTime / boaredSpead;
            Debug.Log("boaredValue:" + boaredValue);
            if (boaredValue >= 1f)
            {
                boaredValue = 0f;
                MoveStateType newDestination;

                // 現在の目的地と違う場所が選ばれるまで、ランダムな場所の取得を繰り返す
                do
                {
                    newDestination = GetRandomDestination();
                } while (newDestination == workerState.moveStateType);


                workerState.SetMoveStateType(newDestination);
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
                    case MoveStateType.食べ物探しへ:
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
                        //Debug.Log("働きたくなってきたー！");
                        //workerState.SetMoveStateType(MoveStateType.生産所へ);
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
