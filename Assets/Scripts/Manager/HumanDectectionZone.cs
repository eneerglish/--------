using UnityEngine;

//人間が施設に入ってきたのを検知するもの
public class HumanDetectionZone : MonoBehaviour
{
    public Facility parentFacility;

    void Start()
    {
        parentFacility = transform.parent.gameObject.GetComponent<Facility>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("人間kannti");
        if (other.gameObject.CompareTag("人間"))
        {
            Debug.Log("人間が施設に入りました");
            Human human = other.gameObject.GetComponent<Human>();
            Debug.Log(human);
            parentFacility.HumanStartProcess(human);
        }

    }
}
