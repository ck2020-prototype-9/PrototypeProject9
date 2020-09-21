using UnityEngine;
using System.Collections;
using System.Linq;

[CreateAssetMenu(fileName = "PrefabList")]
public class PrefabList : ScriptableObject
{
    [SerializeField] PrefabListData[] prefabDatas;

    public GameObject this[string key]
    {
        get
        {
            var query = from item in prefabDatas
                        where item.key == key
                        select item;
            return query.GetEnumerator().Current?.prefab;
        }
    }

    public GameObject this[int index] => prefabDatas[index].prefab;

    public int Length => prefabDatas.Length;
}