using UnityEngine;
using Platformer.Core;
using TMPro;
public class GameModel : MonoBehaviour
{
    public ProductionList productionList;
    public PositionManager positionManager;
    public TMP_Text text;
    void Awake()
    {
        Simulation.SetModel(this); // ここで登録
    }
}
