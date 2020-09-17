using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameStageManager : MonoBehaviour, IStageResettable
{
    [SerializeField] GameOverManager gameOverManager;
    [SerializeField] PlayerCharacterManager playerCharacterManager;
    [SerializeField] FocusCameraManager focusCameraManager;

    public static GameStageManager Instance { get; private set; }
    public GameOverManager GameOverManager => gameOverManager;
    public PlayerCharacterManager PlayerCharacterManager => playerCharacterManager;
    public FocusCameraManager FocusCameraManager => this.focusCameraManager;

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
            StageReset();
        }
    }

    public void StageReset()
    {
        GameOverManager.StageReset();
        PlayerCharacterManager.StageReset();
    }
}