using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameStageManager : MonoBehaviour, IStageResettable
{
    [SerializeField] DirectorManager gameOverDirectorManager;
    [SerializeField] DirectorManager gameClearDirecterManager;
    [SerializeField] PlayerCharacterManager playerCharacterManager;
    [SerializeField] FocusCameraManager focusCameraManager;

    public static GameStageManager Instance { get; private set; }
    public DirectorManager GameOverDirectorManager => gameOverDirectorManager;
    public PlayerCharacterManager PlayerCharacterManager => playerCharacterManager;
    public FocusCameraManager FocusCameraManager => this.focusCameraManager;

    List<ResettableObject> resettableObjects = new List<ResettableObject>();

    bool isGameOver = false;
    bool isGameClear = false;

    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            if (!isGameClear)
            {
                if (isGameOver == false && value == true)
                {
                    GameOverDirectorManager.StartDirecting();
                }
            }
            isGameOver = value;
        }
    }

    public bool IsGameClear
    {
        get => isGameClear;
        set
        {
            if (!isGameOver)
            {
                if (isGameClear == false && value == true)
                {
                    // TODO: 게임 클리어시 클리어 데이터 저장 구현 해야함

                    // 게임 클리어 연출 시작
                    gameClearDirecterManager.StartDirecting();
                }
            }
            isGameClear = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        // 씬 리셋
        if (Keyboard.current[Key.R].wasPressedThisFrame)
        {
            Debug.Log("스테이지 리셋");
            StageReset();
        }
    }

    public void RegisterResettableObject(ResettableObject resettableObject)
    {
        resettableObjects.Add(resettableObject);
    }

    public void StageReset()
    {
        IsGameOver = false;
        IsGameClear = false;

        for (int i = 0; i < resettableObjects.Count; i++)
        {
            ResettableObject resettableObject = resettableObjects[i];

            if (resettableObject != null)
            {
                resettableObject.StageReset();
            }
            else
            {
                resettableObjects.RemoveAt(i--);
            }
        }
        gameClearDirecterManager.StageReset();
        GameOverDirectorManager.StageReset();
        PlayerCharacterManager.StageReset();
    }
}