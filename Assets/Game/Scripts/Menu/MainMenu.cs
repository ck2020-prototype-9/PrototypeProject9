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
    Quit,
    MouseInverse
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
