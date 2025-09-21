using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class ChangeStateEvent : Simulation.Event<ChangeStateEvent>
    {
        public GameObject target;
        public FollowStateType newState;

        public override void Execute()
        {
            WorkerEmotion emotions = target.GetComponent<WorkerEmotion>();
            switch (newState)
            {
                case FollowStateType.待機:
                    SpeakEvent spev = Simulation.Schedule<SpeakEvent>();
                    spev.str = "暇だなー";
                    emotions.GetEmotion(EmotionType.喜).value += 0.1f;
                    break;
                case FollowStateType.生産:
                    emotions.GetEmotion(EmotionType.喜).value -= 0.2f;
                    emotions.GetEmotion(EmotionType.怒).value += 0.1f;
                    var produceEvent = Simulation.Schedule<StartProduceEvent>();
                    produceEvent.target = target;
                    break;
                case FollowStateType.運搬:
                    SpeakEvent spev2 = Simulation.Schedule<SpeakEvent>();
                    spev2.str = "よーし、運ぶぞ！！";
                    emotions.GetEmotion(EmotionType.怒).value += 0.1f;
                    break;
                case FollowStateType.睡眠:
                    emotions.GetEmotion(EmotionType.喜).value += 0.1f;
                    var sleepEvent = Simulation.Schedule<SleepEvent>();
                    sleepEvent.target = target;
                    break;
                case FollowStateType.暴走:
                    emotions.GetEmotion(EmotionType.怒).value -= 0.1f;
                    emotions.GetEmotion(EmotionType.喜).value -= 0.05f;
                    var rampageEvent = Simulation.Schedule<StartRampageEvent>();
                    rampageEvent.target = target;
                    break;
            }
        }
    }
}