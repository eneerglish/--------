using UnityEngine;

//施設にワーカーが入ってきたのを検知するもの
//同時に親オブジェクトのメソッド行う
public class WorkerDetectionZone : MonoBehaviour
{
    public Facility parentFacility;

    void Start()
    {
        parentFacility = transform.parent.gameObject.GetComponent<Facility>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("なんかきた");
        if (other.gameObject.CompareTag("ワーカー"))
        {
            GameObject otherObject = other.transform.parent.gameObject;
            parentFacility.DoStartProcess(otherObject, parentFacility);
        }

    }
}
