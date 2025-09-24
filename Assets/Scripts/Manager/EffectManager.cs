using UnityEngine;
using System.Collections.Generic;
public class EffectManager : MonoBehaviour
{
    public List<GameObject> effectList = new List<GameObject>();

    public void InstantiateEffect(int num, Transform pos, int count)
    {
        GameObject effect = Instantiate(effectList[num], pos.position, Quaternion.identity);
        effect.transform.SetParent(pos, true);
        Destroy(effect, count);
    }
}
