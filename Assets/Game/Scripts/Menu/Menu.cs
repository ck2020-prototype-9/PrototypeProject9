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
    StageBack,
    Stage1,
    Stage11,
    Stage12,
    Stage13,
    Stage14,
    Stage15,
    Stage1Back,

    Stage2,
    Stage21,
    Stage22,
    Stage23,
    Stage24,
    Stage25,
    Stage2Back
}
public class Menu : MonoBehaviour
{
    public ClickType currentType;
    public StageNumber currentStage;

    [SerializeField] private CanvasGroup mainGroup;
    [SerializeField] private CanvasGroup optionGroup;
    [SerializeField] private CanvasGroup tutorialGroup;

    [SerializeField] private CanvasGroup stageGroup;
    [SerializeField] private CanvasGroup stage1Group;
    [SerializeField] private CanvasGroup stage2Group;


    [SerializeField] private Text textName;
    [SerializeField] private bool soundButtonCheck;

    private void Update()
    {
       
    }
    public void OnClick()
    {
        switch(currentType)
        {
            case ClickType.Start:
                CanvasGroupOn(stageGroup);
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
                CanvasGroupOn(optionGroup);
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
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
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
            case StageNumber.Stage1:
                CanvasGroupOn(stage1Group);
                CanvasGroupOff(stageGroup);
                break;
            case StageNumber.Stage11:
                SceneManager.LoadScene("ControlTestScene");
                break;

            case StageNumber.Stage12:

                break;

            case StageNumber.Stage13:

                break;

            case StageNumber.Stage14:

                break;

            case StageNumber.Stage15:

                break;

            case StageNumber.Stage2:
                CanvasGroupOn(stage2Group);
                CanvasGroupOff(stageGroup);
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

            case StageNumber.Null:

                break;

            case StageNumber.StageBack:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(stageGroup);
                break;

            case StageNumber.Stage1Back:
                CanvasGroupOn(stageGroup);
                CanvasGroupOff(stage1Group);
                break;

            case StageNumber.Stage2Back:
                CanvasGroupOn(stageGroup);
                CanvasGroupOff(stage2Group);
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
