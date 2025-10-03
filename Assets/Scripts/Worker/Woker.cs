using UnityEngine;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;
using UnityEngine.AI;
public class Worker : GameAwareBehaviour
{
    public enum AnimState
    {
        Move = 0,
        Action = 1,
        Rotation = 2,
        Death = 3

    }

    #region Data
    public string workerName = "Worker";
    public int workerID = 0;
    public int sleapValue = 0;//3で寝る　アクション行うたびに+1
    public int lifeTime = 0; //秒数
    public float lifeValue = 0; //lifeTimeまでの経過時間

    //float productionSpeed = 3f;//生産にかかる時間

    //float moveSpeed = 3f;//移動速度

    //float sleepDesireUpRate = 10f;//眠くなる速度
    //float sleapDuration = 5f;//睡眠時間

    public int hungerValue = 0; //0~6
    public float boredValue = 0f; //0~1
    public float boredSpeed = 5f;
    #endregion

    public void InitSet()
    {
        workerID = GetInstanceID();
        workerName = "Worker" + workerID.ToString();
        hungerValue = 6;
        lifeTime =60;
        lifeValue = 0;
        //productionSpeed = Random.Range(2f, 5f);
        //moveSpeed = Random.Range(2f, 5f);
        //sleepDesireUpRate = Random.Range(5f, 15f);
        //sleapDuration = Random.Range(3f, 7f);
    }

    public MoveStateType GetRandomDestination()
    {
        List<MoveStateType> destinations = new List<MoveStateType>()
        {
            MoveStateType.生産所へ,
            MoveStateType.水辺へ,
            MoveStateType.牧場へ
        };
        int randomIndex = Random.Range(0, destinations.Count);
        return destinations[randomIndex];
    }

    void Start()
    {
        InitSet();
    }
}
