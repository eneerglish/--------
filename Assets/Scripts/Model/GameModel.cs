using UnityEngine;
using Platformer.Core;
public class GameModel : MonoBehaviour
{

    void Awake()
    {
        Simulation.SetModel(this); // ここで登録
    }
}
