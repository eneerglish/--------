using UnityEngine;
using Platformer.Core;
using TMPro;

//色んなデータを保管する
public class GameModel : MonoBehaviour
{
    public ProductionList productionList;
    public PositionManager positionManager;
    public EffectManager effectManager;
    public WorkerManager workerManager;
    public FacilityManager facilityManager;
    public CameraManager cameraManager;
    public TMP_Text text;

    public Transform enemyAppertransform;

    void Awake()
    {
        Simulation.SetModel(this); // ここで登録
    }
}
