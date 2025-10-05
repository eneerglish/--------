using UnityEngine;
using System.Collections.Generic;
using Platformer.Core;

namespace Platformer.Core
{
    //今までのイベントはstaticで、すべてのタスクをひとまとまりで管理していたので、
    //各々のオブジェクト単位でタスクを管理できるようにしたい
    //chatgpt参考
    public class TaskScheduler : MonoBehaviour
    {
        private HeapQueue<Simulation.Event> eventQueue = new HeapQueue<Simulation.Event>();
        private Dictionary<System.Type, Stack<Simulation.Event>> eventPools = new Dictionary<System.Type, Stack<Simulation.Event>>();

        //他のスクリプトのUpdateでこれを呼ぶ
        void Update()
        {
            Tick();
        }

        public T Schedule<T>(float delay = 0) where T : Simulation.Event, new()
        {
            var ev = New<T>();
            ev.tick = Time.time + delay;
            eventQueue.Push(ev);
            return ev;
        }

        private T New<T>() where T : Simulation.Event, new()
        {
            Stack<Simulation.Event> pool;
            if (!eventPools.TryGetValue(typeof(T), out pool))
            {
                pool = new Stack<Simulation.Event>(4);
                pool.Push(new T());
                eventPools[typeof(T)] = pool;
            }
            if (pool.Count > 0)
            {
                return (T)pool.Pop();
            }
            else
            {
                return new T();
            }
        }
        //イベントキューを処理する(Simulation.Tickとほぼ同じ)
        private int Tick()
        {
            var time = Time.time;
            while (eventQueue.Count > 0 && eventQueue.Peek().tick <= time)
            {
                var ev = eventQueue.Pop();
                ev.ExecuteEvent();
                ev.Cleanup();
                //プールに戻す処理
                if (!eventPools.ContainsKey(ev.GetType()))
                {
                    eventPools[ev.GetType()] = new Stack<Simulation.Event>(4);
                }
                eventPools[ev.GetType()].Push(ev);
            }
            return eventQueue.Count;
        }

        /// <summary>
        /// このスケジューラに予約されているすべてのイベントをキャンセルします。
        /// </summary>
        public void CancelAllTasks()
        {
            eventQueue.Clear();
        }
    }
}