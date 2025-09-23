using Platformer.Core;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// イベントのTick処理を行うものらしい
    /// </summary> 
    public class GameController : GameAwareBehaviour
    {
        public static GameController Instance { get; private set; }


        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }
    }
}