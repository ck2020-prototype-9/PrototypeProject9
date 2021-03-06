﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public GameObject save;
    // Start is called before the first frame update
    [SerializeField] private GameObject menuSet;
    [SerializeField] private GameObject restartObject;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject menu;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject option;
    [SerializeField] TutorialManager tutorialCheck;
    [SerializeField] GameObject optionButtomNormal;
    // [SerializeField] GameStageManager stageReset;

    [SerializeField] GameObject uiManager;
    [SerializeField] GameObject gameOverDirector;
    void Awake()
    {
        pause = GameObject.Find("GameStageManager");
        option= GameObject.Find("GameStageManager");

    }

    void Start()
    {
        optionMenu = GameObject.Find("UiManager").transform.Find("Canvas").transform.Find("Option Menu").gameObject;
    }
    private void Update()
    {
        if(optionMenu==null)
        {
          
        }

        if (uiManager == null)
        {
            uiManager = GameObject.Find("UiManager").gameObject;
        }
    }

    public void OnClickExit()
    {
        pause.GetComponent<GameStageManager>().Pause = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickContinue()
    {
        pause.GetComponent<GameStageManager>().PauseCheck = true;

        //menuSet.SetActive(false);
        //restartObject.SetActive(true);  
    }
    public void OnClickOption()
    {
        optionMenu.SetActive(true);
        menu.SetActive(false);
        optionButtomNormal.SetActive(false);
        if (optionMenu.activeSelf==true)
        {
            option.GetComponent<GameStageManager>().OptionChcek = true;
        }
           
      

    }

    public void OnClickBack()
    {
        option.GetComponent<GameStageManager>().OptionChcek = false;
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
    
    public void OnClickSave()
    {
        save.GetComponent<UiManager>().SoundSave();
    }

    public void OnClickESC()
    {
        gameOverDirector = GameObject.Find("GameOverDirector");
        if (gameOverDirector == null)
        {
            Debug.Log("비어있음");
        }
        else
        {
            Destroy(gameOverDirector);
        }

        if (pause.GetComponent<GameStageManager>().Pause == false)
        {
            pause.GetComponent<GameStageManager>().Pause = true;
            menu.SetActive(true);
            menuSet.SetActive(true);
        }
    }

    public void OnClickNextStage()
    {
        Scene scene = SceneManager.GetActiveScene();

        int curScene = scene.buildIndex;

        int nextScene = curScene + 1;

        SceneManager.LoadScene(nextScene);

        uiManager.GetComponent<UiManager>().stageClearCheck = 0;
    }
}
