using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : System.Enum
{
    public T currentState { get; private set; }
    public float currentStateTime { get; private set; } = 0;
    int stateFlame = 0;
    public bool stateEnter
    {
        get
        {
            stateFlame++;
            return stateFlame == 1 ? true : false;
        }
    }

    //このフェイズを0, 1, 2..など書いていって動きを管理する
    public int phase { get; private set; } = 0;
    public float phaseTime { get; private set; } = 0f;

    public void ChnageState(T newState)
    {
        stateFlame = 0;
        currentState = newState;
        currentStateTime = 0;

        ChangePhase(0);
    }

    public void ChangePhase(int newPhase)
    {
        phase = newPhase;
        phaseTime = 0f;
    }

    public void OnUpdate()
    {
        currentStateTime += Time.deltaTime;
    }
}
