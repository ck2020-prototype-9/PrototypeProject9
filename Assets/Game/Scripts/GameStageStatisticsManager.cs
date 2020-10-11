using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageStatisticsManager : MonoBehaviour, IStageResettable
{
    public static GameStageStatisticsManager Instance { get; private set; }
    [SerializeField] GameStageStatisticsData data = new GameStageStatisticsData();

    public GameStageStatisticsData Data
    {
        get
        {
            // Payload 통계

            var payloads = GameObject.FindGameObjectsWithTag("Payload");

            data.FallenObjectCount = payloads.Length;

            foreach(var payload in payloads)
            {
                var payloadObject = payload.GetComponent<PayloadObject>();

                if (payloadObject == null)
                    continue;

                if (!payloadObject.IsOutBasket)
                {
                    data.DeliveredObjectCount += 1;
                    data.FallenObjectCount -= 1;
                }
            }

            return this.data;
        }

        set => this.data = value;
    }




    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (!GameStageManager.Instance.IsGameClear)
        {
            data.FinishTime += Time.deltaTime;
        }

        data.TotalTime += Time.deltaTime;
    }

    public void StageReset()
    {
        data.FinishTime = 0;
        data.DeliveredObjectCount = 0;
        data.ResetCount += 1;
    }
}
