using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.Threading;

public class GameStageManager : MonoBehaviour, IStageResettable
{
    [SerializeField] DirectorManager gameOverDirectorManager;
    [SerializeField] DirectorManager gameClearDirecterManager;
    [SerializeField] PlayerCharacterManager playerCharacterManager;
    [SerializeField] FocusCameraManager focusCameraManager;
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] GameObject payloadObject;
    [SerializeField] GameObject payloadPrefab;
    public static GameStageManager Instance { get; private set; }
    public DirectorManager GameOverDirectorManager => gameOverDirectorManager;
    public PlayerCharacterManager PlayerCharacterManager => playerCharacterManager;
    public FocusCameraManager FocusCameraManager => this.focusCameraManager;

    List<ResettableObject> resettableObjects = new List<ResettableObject>();

    Vector3 payloadInitPosition;
    bool isGameOver = false;
    bool isGameClear = false;

    [SerializeField] private GameObject menuSet;
    [SerializeField] private GameObject restartObject;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject Option;
    [SerializeField] private RectTransform menuTransform;
    [SerializeField] private Text timeBox;
    [SerializeField] private bool isPause = false;
    [SerializeField] private bool isPauseCheck = false;
    [SerializeField] private GameObject resultMenu;
    private float restartTime = 4.7f;
    private float currentRestartTime;

    [SerializeField] private Animator animator;

    [SerializeField] private bool optionCheck = false;
    [SerializeField] GameObject uiManager;
    [SerializeField] GameObject gameOverDirector;
    public bool PauseCheck { set { isPauseCheck = value; } get { return isPauseCheck; } }

    public bool Pause { set { isPause = value; } get { return isPause; } }

    public bool OptionChcek { set { optionCheck = value; } get { return optionCheck; } }
    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            if (!isGameClear && isGameOver == false && value == true)
            {
                GameStageStatisticsManager.Instance.Data.GameOverCount += 1;

                if (tutorialManager == null || !tutorialManager.gameObject.activeSelf)
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
            if (!isGameOver && isGameClear == false && value == true)
            {
                // TODO: 게임 클리어시 클리어 데이터 저장 구현 해야함
                //if (resultMenu != null)
                //    resultMenu.SetActive(true);
                // 디버그용
                var data = GameStageStatisticsManager.Instance.Data;
                // 게임 클리어 연출 시작
                gameClearDirecterManager.StartDirecting();
                if (uiManager != null)
                    uiManager.GetComponent<UiManager>().stageClearCheck = 1;
            }
            isGameClear = value;
        }
    }

    private void Awake()
    {
        currentRestartTime = restartTime;
        timeBox.text = currentRestartTime.ToString();
        menuTransform = menu.GetComponent<RectTransform>();
        animator = menu.GetComponent<Animator>();
        payloadInitPosition = payloadObject.transform.position;
    }

    private void Start()
    {
        var canvasObject = GameObject.Find("Canvas");
        if (canvasObject != null)
        {
            var resultTransform = canvasObject.transform.Find("Result");
            if (resultTransform != null)
                resultMenu = resultTransform.gameObject;
        }
        if (Instance == null)
            Instance = this;
        if (uiManager == null)
        {
            uiManager = GameObject.Find("UiManager").gameObject;
        }
        //restartObject = GameObject.Find("Canvas").transform.Find("Menu Set").transform.Find("restart").gameObject;
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
            gameOverDirector = GameObject.Find("GameOverDirector");
            if (gameOverDirector == null)
            {
                Debug.Log("비어있음");
            }
            else
            {
                Destroy(gameOverDirector);
            }
          
            if (menuSet.activeSelf && !optionCheck)
            {
                isPauseCheck = true;

            }
            else if (!isPauseCheck && !optionCheck)
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
        GameStageStatisticsManager.Instance.StageReset();
        payloadObject = Instantiate(payloadPrefab);
        payloadObject.transform.position = payloadInitPosition;
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
            animator.SetBool("ESCMenuOn", false);
            currentRestartTime -= Time.unscaledDeltaTime;
            if (currentRestartTime < 3.9f)
                restartObject.SetActive(true);

            if (currentRestartTime < -0.2f)
            {
                menu.SetActive(false);
                isPauseCheck = false;
                isPause = false;
                menuSet.SetActive(false);
                currentRestartTime = restartTime;
                StageReset();
                restartObject.SetActive(false);
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