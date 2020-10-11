using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private Text finishTime;
    [SerializeField] private Text totalTime;
    [SerializeField] private Text gameOverCount;
    [SerializeField] private Text resetCount;
    [SerializeField] private Text deliveredObjectCount;
    [SerializeField] private Text fallenObjectCount;


    void Start()
    {
       
            var statisticsData = GameStageStatisticsManager.Instance.Data.Copy();
            if (finishTime != null)
                finishTime.text = "완주 시간: " + statisticsData.FinishTime.ToString("N3");
            if (totalTime != null)
                totalTime.text = "총 소요 시간: " + statisticsData.TotalTime.ToString("N3");
            if (gameOverCount != null)
                gameOverCount.text = "넘어진 횟수: " + statisticsData.GameOverCount;
            if (resetCount != null)
                resetCount.text = "R키를 눌러 반복한 횟수: " + statisticsData.ResetCount;
            if (deliveredObjectCount != null)
                deliveredObjectCount.text = "배달한 오브젝트: " + statisticsData.DeliveredObjectCount;
            if (fallenObjectCount != null)
                fallenObjectCount.text = "떨어진 오브젝트: " + statisticsData.FallenObjectCount;
        
      

        //finishTime = GameObject.Find("GameClearDummy").transform.Find("Canvas").transform.Find("Result").transform.Find("finishTime").transform.Find("Text").GetComponent<Text>();
        //totalTime = GameObject.Find("GameClearDummy").transform.Find("Canvas").transform.Find("Result").transform.Find("total time").transform.Find("Text").GetComponent<Text>();
        //gameOverCount = GameObject.Find("GameClearDummy").transform.Find("Canvas").transform.Find("Result").transform.Find("count").transform.Find("gameOverCount").GetComponent<Text>();
        //resetCount = GameObject.Find("GameClearDummy").transform.Find("Canvas").transform.Find("Result").transform.Find("count").transform.Find("resetCount").GetComponent<Text>();
        //deliveredObjectCount = GameObject.Find("GameClearDummy").transform.Find("Canvas").transform.Find("Result").transform.Find("count").transform.Find("deliveredObjectCount").GetComponent<Text>();
        //fallenObjectCount = GameObject.Find("GameClearDummy").transform.Find("Canvas").transform.Find("Result").transform.Find("count").transform.Find("fallenObjectCount").GetComponent<Text>();
    }
}
