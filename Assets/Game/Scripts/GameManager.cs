using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] ConfigData configData = ConfigData.Default;

    public ConfigData ConfigData
    {
        get => configData;
        set => configData = value;
    }

    public void ConfigSave()
    {
        Debug.Log("설정 저장");
        try
        {
            File.WriteAllText($"{Application.persistentDataPath}.config.dat", JsonUtility.ToJson(ConfigData));
        }
        catch (Exception e)
        {
            Debug.LogWarning($"설정 저장 실패 : {e}");
        }
    }

    public void ConfigLoad()
    {
        Debug.Log("설정 불러오기");
        try
        {
            ConfigData = JsonUtility.FromJson<ConfigData>(File.ReadAllText($"{Application.persistentDataPath}.config.dat"));
        }
        catch (Exception e)
        {
            Debug.LogWarning($"설정 불러오기 실패 : {e}");
        }
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            Initialize();
            DontDestroyOnLoad(this.gameObject);
        }
        Destroy(this.gameObject);
    }

    public void Initialize()
    {
        ConfigLoad();
    }
}