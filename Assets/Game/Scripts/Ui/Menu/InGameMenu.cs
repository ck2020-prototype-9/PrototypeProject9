using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject menuSet;
    [SerializeField] private GameObject restartObject;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject menu;
    [SerializeField] GameObject pause;
    [SerializeField] TutorialManager tutorialCheck;
   // [SerializeField] GameStageManager stageReset;

    void Awake()
    {
       
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickContinue()
    {
        pause.GetComponent<GameStageManager>().PauseCheck = true;
        menuSet.SetActive(false);
        restartObject.SetActive(true);  
    }
    public void OnClickOption()
    {
        optionMenu.SetActive(true);
        menu.SetActive(false);

    }

    public void OnClickBack()
    {
        optionMenu.SetActive(false);
        menu.SetActive(true);
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

    public void OnTutorialBack()
    {
       // stageReset.GetComponent<GameStageManager>().StageReset();
        tutorialCheck.tutorialCheck = false;
        tutorialCheck.currentCheckTime = tutorialCheck.checkTime;
        if (tutorialCheck.count>0)
            tutorialCheck.count -= 1;
    }
    public void OnTutorialNext()
    {
        //stageReset.GetComponent<GameStageManager>().StageReset();
       tutorialCheck.tutorialCheck = false;
       tutorialCheck.currentCheckTime = tutorialCheck.checkTime;
        if (tutorialCheck.count < 5)
            tutorialCheck.count += 1;
    }

}
