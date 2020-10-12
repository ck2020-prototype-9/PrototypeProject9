using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ClickType
{
    Null,
    Start,
    Tutorial,
    Option,
    Sound,
    OptionBack,
    TutorialBack,
    Quit,
    MouseInverse
}

public enum StageNumber
{
    Null,
    Tutorial,
    StageBack,
    Stage1,
    Stage11,
    Stage12,
    Stage13,
    Stage14,
    Stage15,
    Stage16,
    Stage1Back,

    Stage2,
    Stage21,
    Stage22,
    Stage23,
    Stage24,
    Stage25,
    Stage26,
    Stage2Back
}
public class Menu : MonoBehaviour
{
    public ClickType currentType;
    public StageNumber currentStage;

    [SerializeField] StageClear stageClear;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private CanvasGroup mainGroup;
    [SerializeField] private GameObject optionGroup;
    [SerializeField] private GameObject menu;
    [SerializeField] private CanvasGroup tutorialGroup;

    [SerializeField] private CanvasGroup stageGroup;

    [SerializeField] private GameObject stage1Button;
    [SerializeField] private GameObject stage2Button;


    [SerializeField] private GameObject stage1Group;
    [SerializeField] private GameObject stage2Group;


    [SerializeField] private Text textName;
    [SerializeField] private bool soundButtonCheck;

    [SerializeField] float stage1ChangeTime=1f;
    [SerializeField] float currentStage1ChangeTime;
    [SerializeField] bool stage1Check;

    [SerializeField] float stage2ChangeTime=1f;
    [SerializeField] float currentStage2ChangeTime;
    [SerializeField] bool stage2Check;

    [SerializeField] GameObject uiManager;
    [SerializeField] GameObject optionCheck;
    private void Awake()
    {
        currentStage1ChangeTime = stage1ChangeTime;
        currentStage2ChangeTime = stage2ChangeTime;
        uiManager = GameObject.Find("UiManager").gameObject;
        stageClear = GameObject.Find("ClearCheckBox").GetComponent<StageClear>(); 
    }

    private void Start()
    {
         
    }
    private void Update()
    {
      
        if (stage1Check)
        {
            currentStage1ChangeTime += Time.deltaTime;
            if(currentStage1ChangeTime>2)
            {
                stage1Button.SetActive(false);
                stage1Group.SetActive(true);
                stage1Check = false;
            }
        }
       else if(!stage1Check)
        {
            currentStage1ChangeTime = stage1ChangeTime;
        }

        if (stage2Check)
        {
            currentStage2ChangeTime += Time.deltaTime;
            if (currentStage2ChangeTime > 2)
            {
                stage2Button.SetActive(false);
                stage2Group.SetActive(true);
                stage2Check = false;
            }
        }
        else if (!stage2Check)
        {
            currentStage2ChangeTime = stage2ChangeTime;
        }


        if (optionGroup == null)
        {
            optionGroup = GameObject.Find("UiManager").transform.Find("Canvas").transform.Find("Option Menu").gameObject;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (mainMenu == null)
                mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        }
    }
    public void OnClick()
    {
        switch(currentType)
        {
            case ClickType.Start:
                CanvasGroupOn(stageGroup);
                stage1Button.SetActive(true);
                stage2Button.SetActive(true);
                CanvasGroupOff(mainGroup);
                    Debug.Log("새게임");
                break;

            case ClickType.Tutorial:
                Debug.Log("튜토리얼");
                CanvasGroupOn(tutorialGroup);
                CanvasGroupOff(mainGroup);
                break;

            case ClickType.Option:
                Debug.Log("옵션설정");   
                optionGroup.SetActive(true);
                CanvasGroupOff(mainGroup);
                break;

            case ClickType.Sound:
                Debug.Log("사운드 조정");
                if (!GameManager.Instance.ConfigData.IsSoundMute)
                {
                    textName.text = "소리 끄기";
                    GameManager.Instance.ConfigData.IsSoundMute = true;
                }
                else
                {
                    textName.text = "소리 켜기";
                    GameManager.Instance.ConfigData.IsSoundMute = false;
                }
                   
                break;

            case ClickType.OptionBack:
                Debug.Log("뒤로가기");
                GameManager.Instance.ConfigSave();
                optionGroup.SetActive(false);
                // if (menu != null)    
                if (SceneManager.GetActiveScene().buildIndex!=0)
                {
                    optionCheck = GameObject.Find("==공용 오브젝트==").transform.Find("GameStageManager").gameObject;
                    optionCheck.GetComponent<GameStageManager>().OptionChcek = false;
                    menu = GameObject.Find("==공용 오브젝트==").transform.Find("Canvas").transform.Find("Menu Set").transform.Find("Menu").gameObject;
                    menu.SetActive(true);
                }
               else
                {
                    mainGroup = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<CanvasGroup>();
                    CanvasGroupOn(mainGroup);
                }
                break;

            case ClickType.TutorialBack:
                Debug.Log("뒤로가기");
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(tutorialGroup);
                break;
            case ClickType.Quit:
                Application.Quit();
                break;
            case ClickType.MouseInverse:
                Debug.Log("마우스 Y축 설정");
                if (!GameManager.Instance.ConfigData.IsMouseYInverse)
                {
                    textName.text = "마우스 Y축 반전";
                    GameManager.Instance.ConfigData.IsMouseYInverse = true;
                }
                else
                {
                    textName.text = "마우스 Y축 표준";
                    GameManager.Instance.ConfigData.IsMouseYInverse = false;
                }
                break;

            case ClickType.Null:
                break;
        }

        switch (currentStage)
        {
            case StageNumber.Tutorial:
                SceneManager.LoadScene("TutorialScene");
                break;

            case StageNumber.Stage1:
                stage1Check = true;
                break;

            case StageNumber.Stage11:
                if(stageClear.tutorial == 1)
                {
                    SceneManager.LoadScene("Stage1_2");
                }
               
                break;

            case StageNumber.Stage12:
                if (stageClear.stage1_1==1)
                {
                    SceneManager.LoadScene("Stage1_3");
                }
                break;

            case StageNumber.Stage13:
                if (stageClear.stage1_2 == 1)
                {
                    SceneManager.LoadScene("Stage1_4");
                }          
                break;

            case StageNumber.Stage14:
                if (stageClear.stage1_3 == 1)
                {
                    SceneManager.LoadScene("Stage1_5");
                }
                break;

            case StageNumber.Stage15:
                if (stageClear.stage1_4 == 1)
                {
                    SceneManager.LoadScene("Stage1_6");
                }
                break;
            case StageNumber.Stage16:
                if (uiManager.GetComponent<UiManager>().savedStageCleckCheck == 6)
                {
                  
                }
                break;

            case StageNumber.Stage2:
                stage2Check = true;
                break;

            case StageNumber.Stage21:

                break;

            case StageNumber.Stage22:

                break;

            case StageNumber.Stage23:

                break;

            case StageNumber.Stage24:

                break;

            case StageNumber.Stage25:

                break;

            case StageNumber.Stage26:

                break;

            case StageNumber.Null:

                break;

            case StageNumber.StageBack:
                stage1Group.SetActive(false);
                stage2Group.SetActive(false);
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(stageGroup);
                break;

            case StageNumber.Stage1Back:
                CanvasGroupOn(stageGroup);
               // CanvasGroupOff(stage1Group);
                break;

            case StageNumber.Stage2Back:
                CanvasGroupOn(stageGroup);
              //  CanvasGroupOff(stage2Group);
                break;

        }
    }
    public void CanvasGroupOn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
