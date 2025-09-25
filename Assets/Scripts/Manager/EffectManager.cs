using UnityEngine;
using System.Collections.Generic;
public class EffectManager : MonoBehaviour
{
    public List<GameObject> effectList = new List<GameObject>();

    public void InstantiateEffect(int num, Transform transform, int count)
    {
        GameObject effect = Instantiate(effectList[num], transform.position, Quaternion.identity);
        effect.transform.SetParent(transform, true);
        Destroy(effect, count);
    }
}
