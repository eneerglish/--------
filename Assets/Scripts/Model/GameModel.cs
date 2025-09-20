using UnityEngine;
using Platformer.Core;
public class GameModel : MonoBehaviour
{
    public ProductionList productionList; 
    void Awake()
    {
        Simulation.SetModel(this); // ここで登録
    }
}
