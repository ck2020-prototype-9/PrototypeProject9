using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ClickType
{
    Start,
    Tutorial,
    Option,
    Sound,
    OptionBack,
    TutorialBack,
    Quit
}
public class MainMenu : MonoBehaviour
{
    public ClickType currentType;

    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup tutorialGroup;

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
                SceneManager.LoadScene("ControlTestScene");
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
                if (!soundButtonCheck)
                {
                    textName.text = "소리 끄기";
                    soundButtonCheck = true;
                }
                else
                {
                    textName.text = "소리 켜기";
                    soundButtonCheck = false;
                }
                   
                break;

            case ClickType.OptionBack:
                Debug.Log("뒤로가기");
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
