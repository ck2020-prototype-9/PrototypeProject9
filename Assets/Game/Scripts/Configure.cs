using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ConfigData
{
    public bool IsSoundMute;
    public bool IsMouseYInverse;

    public static ConfigData Default => new ConfigData()
    {
        IsMouseYInverse = false,
        IsSoundMute = false
    };

    public string Save() => JsonUtility.ToJson(this);
}