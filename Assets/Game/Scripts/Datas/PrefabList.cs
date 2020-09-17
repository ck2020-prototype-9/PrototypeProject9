using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PrefabList")]
public class PrefabList : ScriptableObject
{
    public GameObject[] prefabs;
}