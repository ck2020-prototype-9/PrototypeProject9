using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameStageManager : MonoBehaviour, IStageResettable
{
    [SerializeField] DirectorManager gameOverDirectorManager;
    [SerializeField] PlayerCharacterManager playerCharacterManager;
    [SerializeField] FocusCameraManager focusCameraManager;

    public static GameStageManager Instance { get; private set; }
    public DirectorManager GameOverDirectorManager => gameOverDirectorManager;
    public PlayerCharacterManager PlayerCharacterManager => playerCharacterManager;
    public FocusCameraManager FocusCameraManager => this.focusCameraManager;

    bool isGameOver = false;
    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            if (isGameOver == false && value == true)
            {
                GameOverDirectorManager.StartDirecting();
            }
            isGameOver = value;
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

    public void StageReset()
    {
        IsGameOver = false;
        GameOverDirectorManager.StageReset();
        PlayerCharacterManager.StageReset();
    }
}