using UnityEngine;

public class WorkerState
{
    public StateType state{get; private set;}
    public float value;
    public WorkerState(StateType initialState)
    {
        state = initialState;
        value = 0f;
    }
}
public enum StateType
{
    待機,
    生産,
    運搬,
    睡眠,
    暴走
}


