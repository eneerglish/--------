using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : System.Enum
{
    public T currentState { get; private set; }
    public float currentStateTime { get; private set; } = 0;

    //このフェイズを0, 1, 2..など書いていって動きを管理する
    public int phase { get; private set; } = 0;
    public int maxPhase;

    public StateMachine(int _maxPhase)
    {
        maxPhase = _maxPhase;
    }

    public void ChangeState(T newState)
    {
        currentState = newState;
        currentStateTime = 0;

        ChangePhase(0);
    }

    public void ChangePhase(int newPhase)
    {
        phase = newPhase;
    }

    public void OnUpdate()
    {
        currentStateTime += Time.deltaTime;
    }
}
