using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameStageManager : MonoBehaviour, IStageResettable
{
    [SerializeField] GameOverManager gameOverManager;

    public static GameStageManager Instance { get; private set; }
    public GameOverManager GameOverManager { get => gameOverManager; }

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
    }
}