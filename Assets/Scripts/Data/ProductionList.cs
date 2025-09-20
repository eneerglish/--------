using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ProductionList", menuName = "ProductionList", order = 0)]
public class ProductionList : ScriptableObject
{
    public List<GameObject> productionList = new List<GameObject>();

    public GameObject GetProduction()
    {
        return productionList[0];
    }   
}
