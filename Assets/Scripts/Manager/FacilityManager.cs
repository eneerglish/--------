using UnityEngine;
using System.Collections.Generic;
public class FacilityManager : MonoBehaviour
{
    public List<GameObject> facilityList = new List<GameObject>();
    public GameObject InstantiateFacility(int num, Transform transform)
    {
        GameObject facility = Instantiate(facilityList[num], transform.position, Quaternion.identity);
        return facility;
    }
}
