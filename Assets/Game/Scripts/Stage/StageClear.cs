using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Main,
    Tutorial,
    Stage11,
    Stage12,
    Stage13,
    Stage14,
    Stage15,
    Stage16,

    Stage21,
    Stage22,
    Stage23,
    Stage24,
    Stage25,
    Stage26
}
public class StageClear : MonoBehaviour
{
    public StageType currentStage;
    [SerializeField] public int tutorial=0;
    [SerializeField] public int stage1_1=0;
    [SerializeField] public int stage1_2=0;
    [SerializeField] public int stage1_3=0;
    [SerializeField] public int stage1_4=0;
    [SerializeField] public int stage1_5=0;
    [SerializeField] public int stage1_6=0;

    [SerializeField] public int stage2_1 = 0;
    [SerializeField] public int stage2_2 = 0;
    [SerializeField] public int stage2_3 = 0;
    [SerializeField] public int stage2_4 = 0;
    [SerializeField] public int stage2_5 = 0;
    [SerializeField] public int stage2_6 = 0;



    [SerializeField] GameObject uiManager;

    [SerializeField] GameObject stage1_1Lock;
    [SerializeField] GameObject stage1_2Lock;
    [SerializeField] GameObject stage1_3Lock;
    [SerializeField] GameObject stage1_4Lock;
    [SerializeField] GameObject stage1_5Lock;
    [SerializeField] GameObject stage1_6Lock;

    [SerializeField] GameObject stage2_1Lock;
    [SerializeField] GameObject stage2_2Lock;
    [SerializeField] GameObject stage2_3Lock;
    [SerializeField] GameObject stage2_4Lock;
    [SerializeField] GameObject stage2_5Lock;
    [SerializeField] GameObject stage2_6Lock;

    void Awake()
    {
        if(uiManager=null)
        {

        }
        uiManager = GameObject.Find("UiManager").gameObject;

        tutorial = PlayerPrefs.GetInt("TutorialClear", 0);
        stage1_1= PlayerPrefs.GetInt("Stage1_1", 0);
        stage1_2 = PlayerPrefs.GetInt("Stage1_2", 0);
        stage1_3 = PlayerPrefs.GetInt("Stage1_3", 0);
        stage1_4 = PlayerPrefs.GetInt("Stage1_4", 0);
        stage1_5 = PlayerPrefs.GetInt("Stage1_5", 0);
        stage1_6 = PlayerPrefs.GetInt("Stage1_6", 0);
        stage2_1 = PlayerPrefs.GetInt("Stage2_1", 0);
        stage2_1 = PlayerPrefs.GetInt("Stage2_2", 0);
        stage2_1 = PlayerPrefs.GetInt("Stage2_3", 0);
        stage2_1 = PlayerPrefs.GetInt("Stage2_4", 0);
        stage2_1 = PlayerPrefs.GetInt("Stage2_5", 0);
        stage2_1 = PlayerPrefs.GetInt("Stage2_6", 0);


        stage1_1Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage1Menu").transform.Find("Stage1-1 Button").transform.Find("Image").gameObject;
        stage1_2Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage1Menu").transform.Find("Stage1-2 Button").transform.Find("Image").gameObject;
        stage1_3Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage1Menu").transform.Find("Stage1-3 Button").transform.Find("Image").gameObject;
        stage1_4Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage1Menu").transform.Find("Stage1-4 Button").transform.Find("Image").gameObject;
        stage1_5Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage1Menu").transform.Find("Stage1-5 Button").transform.Find("Image").gameObject;
        stage1_6Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage1Menu").transform.Find("Stage1-6 Button").transform.Find("Image").gameObject;

        stage2_1Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage2Menu").transform.Find("Stage2-1 Button").transform.Find("Image").gameObject;
        stage2_2Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage2Menu").transform.Find("Stage2-2 Button").transform.Find("Image").gameObject;
        stage2_3Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage2Menu").transform.Find("Stage2-3 Button").transform.Find("Image").gameObject;
        stage2_4Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage2Menu").transform.Find("Stage2-4 Button").transform.Find("Image").gameObject;
        stage2_5Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage2Menu").transform.Find("Stage2-5 Button").transform.Find("Image").gameObject;
        stage2_6Lock = GameObject.Find("Canvas").transform.Find("Menu1").transform.Find("StageMenu").transform.Find("Stage2Menu").transform.Find("Stage2-6 Button").transform.Find("Image").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStage)
        {
            case StageType.Tutorial:
                if (tutorial == 0)
                {
                    if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                    {
                        tutorial = 1;
                        PlayerPrefs.SetInt("TutorialClear", 1);
                    }
                }
                break;

            case StageType.Stage11:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage1_1 = 1;
                    PlayerPrefs.SetInt("Stage1_1", 1);
                }
                break;

            case StageType.Stage12:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage1_2 = 1;
                    PlayerPrefs.SetInt("Stage1_2", 1);
                }
                break;

            case StageType.Stage13:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage1_3 = 1;
                    PlayerPrefs.SetInt("Stage1_3", 1);
                }
                break;

            case StageType.Stage14:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage1_4 = 1;
                    PlayerPrefs.SetInt("Stage1_4", 1);
                }
                break;

            case StageType.Stage15:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage1_5 = 1;
                    PlayerPrefs.SetInt("Stage1_5", 1);
                }
                break;

            case StageType.Stage16:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage1_6 = 1;
                    PlayerPrefs.SetInt("Stage1_6", 1);
                }
                break;

            case StageType.Stage21:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage2_1 = 1;
                    PlayerPrefs.SetInt("Stage2_1", 1);
                }
                break;

            case StageType.Stage22:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage2_2 = 1;
                    PlayerPrefs.SetInt("Stage2_2", 1);
                }
                break;

            case StageType.Stage23:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage2_3 = 1;
                    PlayerPrefs.SetInt("Stage2_3", 1);
                }
                break;

            case StageType.Stage24:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage2_4 = 1;
                    PlayerPrefs.SetInt("Stage2_4", 1);
                }
                break;

            case StageType.Stage25:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage2_5 = 1;
                    PlayerPrefs.SetInt("Stage2_5", 1);
                }
                break;

            case StageType.Stage26:
                if (uiManager.GetComponent<UiManager>().stageClearCheck == 1)
                {
                    stage2_6 = 1;
                    PlayerPrefs.SetInt("Stage2_6", 1);
                }
                break;
        }

        if(tutorial ==1)
        {
            stage1_1Lock.SetActive(false);
        }
        else
            stage1_1Lock.SetActive(true);

        if (stage1_1==1)
        {
            stage1_2Lock.SetActive(false);
        }
        else
            stage1_2Lock.SetActive(true);

        if (stage1_2 == 1)
        {
            stage1_3Lock.SetActive(false);
        }
        else
            stage1_3Lock.SetActive(true);

        if (stage1_3 == 1)
        {
            stage1_4Lock.SetActive(false);
        }
        else
            stage1_4Lock.SetActive(true);

        if (stage1_4 == 1)
        {
            stage1_5Lock.SetActive(false);
        }
        else
            stage1_5Lock.SetActive(true);

        if (stage1_5 == 1)
        {
            stage1_6Lock.SetActive(false);
        }
        else
            stage1_6Lock.SetActive(true);

        if (stage1_6== 1)
        {
            stage2_1Lock.SetActive(false);
        }
        else
            stage2_1Lock.SetActive(true);

        if (stage2_1 == 1)
        {
            stage2_2Lock.SetActive(false);
        }
        else
            stage2_2Lock.SetActive(true);

        if (stage2_2 == 1)
        {
            stage2_3Lock.SetActive(false);
        }
        else
            stage2_3Lock.SetActive(true);

        if (stage2_3 == 1)
        {
            stage2_4Lock.SetActive(false);
        }
        else
            stage2_4Lock.SetActive(true);

        if (stage2_4 == 1)
        {
            stage2_5Lock.SetActive(false);
        }
        else
            stage2_5Lock.SetActive(true);

        if (stage2_5 == 1)
        {
            stage2_6Lock.SetActive(false);
        }
        else
            stage2_6Lock.SetActive(true);
    }

   public void OnClickReSet()
    {
        PlayerPrefs.SetInt("TutorialClear", 0);
        PlayerPrefs.SetInt("Stage1_1", 0);
        PlayerPrefs.SetInt("Stage1_2", 0);
        PlayerPrefs.SetInt("Stage1_3", 0);
        PlayerPrefs.SetInt("Stage1_4", 0);
        PlayerPrefs.SetInt("Stage1_5", 0);
        PlayerPrefs.SetInt("Stage1_6", 0);

    }
}
