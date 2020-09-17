using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverManager : MonoBehaviour, IStageResettable
{
    [SerializeField] PrefabList gameOverPrefabList;

    GameObject gameOverDirectorObject;

    bool isGameOver = false;
    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            if (isGameOver == false && value == true)
            {
                StartGameOverDirecting();
            }
            isGameOver = value;
        }
    }

    void StartGameOverDirecting()
    {
        Debug.Log("게임 오버 연출 시작");
        if (gameOverPrefabList.prefabs != null && gameOverPrefabList.prefabs.Length > 0)
        {
            var index = Random.Range(0, gameOverPrefabList.prefabs.Length);

            var gameOverDirectorPrefab = gameOverPrefabList.prefabs[index];

            // 디렉팅 시작 (자동 시작)
            gameOverDirectorObject = Instantiate(gameOverDirectorPrefab);
            gameOverDirectorObject.name = "GameOverDirector";
        }
        else
        {
            Debug.LogWarning($"{nameof(gameOverPrefabList.prefabs)}에 값이 할당되지 않았습니다.");
        }
    }

    public void StageReset()
    {
        IsGameOver = false;
        Destroy(gameOverDirectorObject);
    }

}
