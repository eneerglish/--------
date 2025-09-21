using UnityEngine;

public class EnterProductionSpace : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("なんかきた");
        if (other.gameObject.CompareTag("生産物")) return;
        GameObject otherObject = other.transform.parent.gameObject;
        Worker worker = otherObject.GetComponent<Worker>();
        worker.facility = transform.parent.gameObject;
        WorkerState state = otherObject.GetComponent<WorkerState>();
        state.ChangeFollowState(FollowStateType.生産);
    }
}
