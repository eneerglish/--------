using UnityEngine;

public class EnterHome : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("なんかきた");
        GameObject otherObject = other.transform.parent.gameObject;
        Worker worker = otherObject.GetComponent<Worker>();
        worker.facility = transform.parent.gameObject;
        WorkerState state = otherObject.GetComponent<WorkerState>();
        state.ChangeFollowState(FollowStateType.睡眠);
    }
}
