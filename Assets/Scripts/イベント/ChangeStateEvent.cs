using UnityEngine;
using Platformer.Core;

namespace Platformer.Events
{
    public class ChangeStateEvent : Simulation.Event<ChangeStateEvent>
    {
        public GameObject target;
        public FollowStateType newState;
        public ActionData actionData;

        //このイベントでワーカーの状態変化、感情変化を管理
        public override void Execute()
        {
            //各種読み込み
            WorkerEmotion emotions = target.GetComponent<WorkerEmotion>();
            Worker worker = target.GetComponent<Worker>();
            WorkerState state = target.GetComponent<WorkerState>();

            //状態変更
            state.SetFollowStateType(newState);

            if (newState == FollowStateType.死亡)
            {
                var ev = Simulation.Schedule<DeadPlayer>();
                ev.target = target;
                return;
            }


            //疲労値の増加　基本増加
            if (newState != FollowStateType.待機 && newState != FollowStateType.被捕食 && newState != FollowStateType.睡眠
                && newState != FollowStateType.スポーン)
            {
                worker.sleapValue += 1;
            }

            if (newState == FollowStateType.暴走)
            {
                emotions.GetEmotion(EmotionType.怒).value -= 0.2f;
                var ev = Simulation.Schedule<StartRampageEvent>();
                ev.target = target;
                return;
            }


            //ActionDataがセットされていたら感情変化と施設開始イベント発行
                if (actionData != null)
                {
                    foreach (var modifier in actionData.GetEmotionModifiers())
                    {
                        EmotionData targetEmotion = emotions.GetEmotion(modifier.emotionType);
                        if (targetEmotion != null)
                        {
                            targetEmotion.value += modifier.value;
                        }
                    }
                    var ev = Simulation.Schedule<StartFacilityEvent>();
                    ev.target = target;
                    ev.actionData = actionData;
                }

        }
        internal override void Cleanup()
        {
            this.target = null;
            this.newState = FollowStateType.待機;
            this.actionData = null;
        }
    }
}