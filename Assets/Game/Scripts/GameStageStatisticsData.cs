using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class GameStageStatisticsData
{
    /// <summary>
    /// 완주 시간
    /// </summary>
    public float FinishTime;
    /// <summary>
    /// 총 소요 시간
    /// </summary>
    public float TotalTime;
    /// <summary>
    /// 넘어진 횟수
    /// </summary>
    public int GameOverCount;
    /// <summary>
    /// 리셋 횟수
    /// </summary>
    public int ResetCount;
    /// <summary>
    /// 배달한 오브젝트
    /// </summary>
    public int DeliveredObjectCount;
    /// <summary>
    /// 떨어진 오브젝트
    /// </summary>
    public int FallenObjectCount;

    public GameStageStatisticsData Copy()
    {
        return (GameStageStatisticsData)MemberwiseClone();
    }
}