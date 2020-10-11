using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]  private Text finishTime;
    [SerializeField] private Text totalTime;
    [SerializeField] private Text gameOverCount;
    [SerializeField] private Text resetCount;
    [SerializeField] private Text deliveredObjectCount;
    [SerializeField] private Text fallenObjectCount;
    void Awake()
    {
        finishTime = GameObject.Find("Canvas").transform.Find("Result").transform.Find("finishTime").transform.Find("Text").GetComponent<Text>();
        totalTime = GameObject.Find("Canvas").transform.Find("Result").transform.Find("total time").transform.Find("Text").GetComponent<Text>();
        gameOverCount = GameObject.Find("Canvas").transform.Find("Result").transform.Find("count").transform.Find("gameOverCount").GetComponent<Text>();
        resetCount= GameObject.Find("Canvas").transform.Find("Result").transform.Find("count").transform.Find("resetCount").GetComponent<Text>();
        deliveredObjectCount = GameObject.Find("Canvas").transform.Find("Result").transform.Find("count").transform.Find("deliveredObjectCount").GetComponent<Text>();
        fallenObjectCount= GameObject.Find("Canvas").transform.Find("Result").transform.Find("count").transform.Find("fallenObjectCount").GetComponent<Text>();
        
    }

    
    void Update()
    {
        finishTime.text = "완주 시간: "+ GameStageStatisticsManager.Instance.Data.Copy().FinishTime;
        totalTime.text = "총 소요 시간: " + GameStageStatisticsManager.Instance.Data.Copy().TotalTime;
        gameOverCount.text = "넘어진 횟수: " + GameStageStatisticsManager.Instance.Data.Copy().GameOverCount;
        resetCount.text = "R키를 눌러 반복한 횟수: " + GameStageStatisticsManager.Instance.Data.Copy().ResetCount;
        deliveredObjectCount.text = "배달한 오브젝트: " + GameStageStatisticsManager.Instance.Data.Copy().DeliveredObjectCount;
        fallenObjectCount.text = "떨어진 오브젝트: " + GameStageStatisticsManager.Instance.Data.Copy().FallenObjectCount;
    }
}
