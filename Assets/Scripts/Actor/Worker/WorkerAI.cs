using UnityEngine;
using Platformer.Core;
using Platformer.Events;

public class WorkerAI : MonoBehaviour
{
    private Worker worker;
    private WorkerState workerState;
    private WorkerEmotion workerEmotion;

    //自分専用のタスクスケジューラ
    private TaskScheduler scheduler;

    void Start()
    {
        worker = GetComponent<Worker>();
        workerState = GetComponent<WorkerState>();
        workerEmotion = GetComponent<WorkerEmotion>();

        scheduler = GetComponent<TaskScheduler>();
        workerState.SetFollowStateType(FollowStateType.スポーン);
        workerState.SetMoveStateType(MoveStateType.なし);


    }

    void Update()
    {

        // 死亡状態とスポーンの時は何もしない
        if (workerState.followStateType == FollowStateType.死亡
            || workerState.followStateType == FollowStateType.スポーン) return;

        if (workerState.followStateType != FollowStateType.死亡)
        {
            worker.lifeValue += Time.deltaTime;
        }

        //寿命の判定 
        if (worker.lifeValue >= worker.lifeTime)
        {
            var ev = scheduler.Schedule<ChangeStateEvent>();
            ev.target = this.gameObject;
            ev.newState = FollowStateType.死亡;
            return;
        }
        
        // アクティブ状態でなければ何もしない
        if (workerState.activeState != ActiveState.FollowStateType) return;

        //眠かったら家へ
        if (workerState.followStateType == FollowStateType.待機 && worker.sleapValue >= 3 && workerState.moveStateType != MoveStateType.家1へ)
        {
            worker.sleapValue = 0;
            workerState.SetMoveStateType(MoveStateType.家1へ);
            return;
        }

        //暇なとき適当な場所に行くようにしたい
        if (workerState.followStateType == FollowStateType.待機)
        {

            worker.boredValue += Time.deltaTime / worker.boredSpeed;
            //Debug.Log("boredValue:" + worker.boredValue);
            if (worker.boredValue >= 1f)
            {
                worker.boredValue = 0f;
                MoveStateType newDestination;
                do
                {
                    newDestination = worker.GetRandomDestination();
                } while (newDestination == workerState.moveStateType);
                workerState.SetMoveStateType(newDestination);
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

        if (workerState.followStateType != FollowStateType.待機)
        {
            return;
        }
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
                    var ev = Simulation.Schedule<ChangeStateEvent>();
                    ev.target = this.gameObject;
                    ev.newState = FollowStateType.暴走;
                    break;
            }
        }
    }
}