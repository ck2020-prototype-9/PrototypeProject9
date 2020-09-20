using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameStageManager : MonoBehaviour, IStageResettable
{
    [SerializeField] DirectorManager gameOverDirectorManager;
    [SerializeField] DirectorManager gameClearDirecterManager;
    [SerializeField] PlayerCharacterManager playerCharacterManager;
    [SerializeField] FocusCameraManager focusCameraManager;
    [SerializeField] GameObject payloadPrefab;

    public static GameStageManager Instance { get; private set; }
    public DirectorManager GameOverDirectorManager => gameOverDirectorManager;
    public PlayerCharacterManager PlayerCharacterManager => playerCharacterManager;
    public FocusCameraManager FocusCameraManager => this.focusCameraManager;

    List<ResettableObject> resettableObjects = new List<ResettableObject>();

    bool isGameOver = false;
    bool isGameClear = false;

    [SerializeField] private GameObject menuSet;
    [SerializeField] private GameObject restartObject;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject Option;
    [SerializeField] private Text timeBox;
    [SerializeField]bool isPause = false;
    [SerializeField]bool isPauseCheck = false;

    private float restartTime = 3.99f;
    private float currentRestartTime;

    

    public bool PauseCheck { set { isPauseCheck = value; } get { return isPauseCheck; } }
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

        currentRestartTime = restartTime;
        timeBox.text = currentRestartTime.ToString();

    }

    private void Update()
    {
        timeBox.text = Mathf.Floor(currentRestartTime).ToString();
        // 씬 리셋
        if (!isPause)
        {
            if (Keyboard.current[Key.R].wasPressedThisFrame)
            {
                Debug.Log("스테이지 리셋");
                StageReset();
            }
        }

        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            Debug.Log("esc눌림");
            if (menuSet.activeSelf)
            {
                menu.SetActive(false);
                restartObject.SetActive(true);
                Option.SetActive(false);
                isPauseCheck = true;

            }
            else if(!isPauseCheck)
            {
                menuSet.SetActive(true);
                menu.SetActive(true);
                restartObject.SetActive(false);
                isPause = true;
            }

        }
        IsPause();
        ReStart();
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
        Instantiate(payloadPrefab);
    }

    public void IsPause()
    {
        //일시정지
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void ReStart() 
    {
        //일시정지가 풀리기 전 3초 지연시간
        if (isPauseCheck && isPause)
        {
            currentRestartTime -= Time.unscaledDeltaTime;
            if (currentRestartTime < 1)
            {
                isPauseCheck = false;
                isPause = false;
                menuSet.SetActive(false);
                currentRestartTime = restartTime;
                StageReset();
            }
        }
    }
    public void OnContinueButton()
    {
        menu.SetActive(false);
        restartObject.SetActive(true);
        isPauseCheck = true;
    }
}