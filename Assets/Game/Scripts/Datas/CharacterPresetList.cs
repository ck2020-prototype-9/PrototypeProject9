using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CharacterPresetList")]
public class CharacterPresetList : ScriptableObject
{
    public CharacterPresetData[] characterPreset;
}