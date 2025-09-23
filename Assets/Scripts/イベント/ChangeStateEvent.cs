using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class ChangeStateEvent : Simulation.Event<ChangeStateEvent>
    {
        public GameObject target;
        public FollowStateType newState;
        public Facility facility;

        public override void Execute()
        {
            WorkerEmotion emotions = target.GetComponent<WorkerEmotion>();
            switch (newState)
            {
                case FollowStateType.待機:
                {
                    emotions.GetEmotion(EmotionType.喜).value += 0.1f;
                    break;
                }

                case FollowStateType.水遊び:
                    {
                        emotions.GetEmotion(EmotionType.喜).value += 0.1f;
                        var ev = Simulation.Schedule<PlayWaterEvent>();
                        ev.target = target;
                        ev.facility = facility;
                        break;
                }

                case FollowStateType.生産:
                {
                    emotions.GetEmotion(EmotionType.喜).value -= 0.2f;
                    emotions.GetEmotion(EmotionType.怒).value += 0.1f;
                    var ev = Simulation.Schedule<StartProduceEvent>();
                    ev.target = target;
                    ev.facility = facility;
                    break;
                }

                case FollowStateType.運搬:
                {
                    emotions.GetEmotion(EmotionType.怒).value += 0.1f;
                    break;
                }

                case FollowStateType.餌やり:
                    {
                        emotions.GetEmotion(EmotionType.喜).value -= 0.1f;
                        emotions.GetEmotion(EmotionType.怒).value += 0.1f;
                        var ev = Simulation.Schedule<StartFedingEvent>();
                        ev.target = target;
                        ev.facility = facility as FarmSpace;
                        break;
                }

                case FollowStateType.睡眠:
                {
                    emotions.GetEmotion(EmotionType.喜).value += 0.1f;
                    var ev = Simulation.Schedule<SleepEvent>();
                    ev.target = target;
                        ev.facility = facility;
                    break;
                }
                case FollowStateType.暴走:
                {
                    emotions.GetEmotion(EmotionType.怒).value -= 0.1f;
                    emotions.GetEmotion(EmotionType.喜).value -= 0.05f;
                    var ev = Simulation.Schedule<StartRampageEvent>();
                    ev.target = target;
                    break;
                }
            }
        }
    }
}