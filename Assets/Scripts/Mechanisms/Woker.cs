using UnityEngine;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;
public class Worker : GameAwareBehaviour
{
    public Animator anim;
    class Emotions
    {
        public List<WorkerEmotion> emotionList = new List<WorkerEmotion>();
        public Emotions()
        {
            for (int i = 0; i < System.Enum.GetValues(typeof(EmotionType)).Length; i++)
            {
                emotionList.Add(new WorkerEmotion((EmotionType)i));
            }
        }

        public WorkerEmotion GetEmotion(EmotionType type)
        {
            return emotionList.Find(e => e.emotion == type);
        }
        public WorkerEmotion GetDominantEmotion()
        {
            return emotionList[0];
        }
        public void SortEmotion()
        {
            emotionList.Sort((a, b) => b.value.CompareTo(a.value));
        }
    }

    class States
    {
        public List<WorkerState> stateList = new List<WorkerState>();
        public States()
        {
            for (int i = 0; i < System.Enum.GetValues(typeof(StateType)).Length; i++)
            {
                stateList.Add(new WorkerState((StateType)i));
            }
        }

        public WorkerState GetState(StateType type)
        {
            return stateList.Find(s => s.state == type);
        }
        //これを使って、一番値が大きいものを先頭に持ってくる
        public void SortDesire()
        {
            stateList.Sort((a, b) => b.value.CompareTo(a.value));
        }
    }

    Emotions emotions = new Emotions();
    States states = new States();

    #region Data
    public string workerName = "Worker";
    public int workerID = 0;
    public StateType currentState = StateType.待機;
    public EmotionType currentEmotion = EmotionType.喜;
    bool stateEnter = true;

    float productionSpeed = 3f;//生産にかかる時間

    float moveSpeed = 3f;//移動速度

    float sleepDesireUpRate = 10f;//眠くなる速度
    float sleapDuration = 5f;//睡眠時間
    #endregion
    public void ChangeState(StateType newState)
    {
        currentState = newState;
        stateEnter = true;
        //stateが切り替わった時に微調整
        switch (newState)
        {
            case StateType.待機:
                emotions.GetEmotion(EmotionType.楽).value += 0.1f;
                break;
            case StateType.生産:
                emotions.GetEmotion(EmotionType.喜).value -= 0.2f;
                emotions.GetEmotion(EmotionType.怒).value += 0.1f;
                break;
            case StateType.運搬:
                emotions.GetEmotion(EmotionType.怒).value += 0.1f;
                break;
            case StateType.睡眠:
                emotions.GetEmotion(EmotionType.喜).value += 0.1f;
                break;
            case StateType.暴走:
                emotions.GetEmotion(EmotionType.怒).value -= 0.1f;
                emotions.GetEmotion(EmotionType.楽).value -= 0.05f;
                break;
        }
    }
    public void InitSet()
    {
        workerID = GetInstanceID();
        workerName = "Worker" + workerID.ToString();
        productionSpeed = Random.Range(2f, 5f);
        moveSpeed = Random.Range(2f, 5f);
        sleepDesireUpRate = Random.Range(5f, 15f);
        sleapDuration = Random.Range(3f, 7f);
    }

    public void 仮ChangeState()
    {
        if (currentState == StateType.待機)
        {
            ChangeState(StateType.生産);
        }
    }

    void Start()
    {
        InitSet();
    }
    private void Update()
    {

        if (currentState != StateType.睡眠)
        {
            states.GetState(StateType.睡眠).value += Time.deltaTime / sleepDesireUpRate;
            if (states.GetState(StateType.睡眠).value >= 1f)
            {
                ChangeState(StateType.睡眠);
            }
        }


        switch (currentState)
        {
            case StateType.待機:
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("暇だなー");
                }
                emotions.GetEmotion(EmotionType.喜).value += Time.deltaTime / 50f;
                emotions.GetEmotion(EmotionType.怒).value -= Time.deltaTime / 100f;
                break;

            case StateType.生産:
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("生産するぞー！");
                }
                states.GetState(StateType.生産).value += Time.deltaTime / productionSpeed;
                emotions.GetEmotion(EmotionType.楽).value -= Time.deltaTime / 1000f;
                emotions.GetEmotion(EmotionType.怒).value += Time.deltaTime / 200f;
                if (states.GetState(StateType.生産).value >= 1f)
                {
                    states.GetState(StateType.生産).value = 0f;
                    ChangeState(StateType.運搬);
                    Simulation.Schedule<GanerateProduction>();
                }
                break;

            case StateType.運搬:
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("よーし、運搬するぞー！");
                    ChangeState(StateType.待機);
                }
                emotions.GetEmotion(EmotionType.楽).value -= Time.deltaTime / 1000f;
                emotions.GetEmotion(EmotionType.怒).value += Time.deltaTime / 200f;
                break;
            case StateType.睡眠:
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("ねむねむzzz");
                }
                states.GetState(StateType.睡眠).value -= Time.deltaTime / sleapDuration;
                emotions.GetEmotion(EmotionType.喜).value += Time.deltaTime / 100f;
                emotions.GetEmotion(EmotionType.怒).value -= Time.deltaTime / 200f;
                if (states.GetState(StateType.睡眠).value <= 0f)
                {
                    states.GetState(StateType.睡眠).value = 0f;
                    Debug.Log("おはよう！");
                    ChangeState(StateType.待機);
                }
                break;
            case StateType.暴走:
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("おりゃああああ");
                    //var ev = Simulation.Schedule<StartWorkerRampage>();
                    //ev.worker = this.gameObject;
                }
                states.GetState(StateType.睡眠).value += Time.deltaTime / (sleepDesireUpRate / 2);

                break;
        }

        emotions.SortEmotion();
        if (emotions.GetDominantEmotion().emotion != currentEmotion)
        {
            currentEmotion = emotions.GetDominantEmotion().emotion;
            switch (currentEmotion)
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

        if (emotions.GetDominantEmotion().value >= 1f)
        {
            EmotionType dominantEmotion = emotions.GetDominantEmotion().emotion;
            switch (dominantEmotion)
            {
                case EmotionType.喜:
                case EmotionType.楽:
                    Debug.Log("働きたくなってきたー！");
                    ChangeState(StateType.生産);
                    break;
                case EmotionType.怒:
                case EmotionType.哀:
                    Debug.Log("もうやあだー");
                    ChangeState(StateType.暴走);
                    break;
            }
        }
    }
}
