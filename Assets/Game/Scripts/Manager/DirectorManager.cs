using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DirectorManager : MonoBehaviour, IStageResettable
{
    [SerializeField] string directorObjectName;
    [SerializeField] PrefabList directingPrefabList;

    GameObject gameOverDirectorObject;

    public void StartDirecting()
    {
        Debug.Log($"{directorObjectName} 연출 시작");
        if (directingPrefabList != null && directingPrefabList.Length > 0)
        {
            var index = Random.Range(0, directingPrefabList.Length);

            var gameOverDirectorPrefab = directingPrefabList[index];

            // 디렉팅 시작 (자동 시작)
            gameOverDirectorObject = Instantiate(gameOverDirectorPrefab);
            gameOverDirectorObject.name = directorObjectName;
        }
        else
        {
            Debug.LogWarning($"{nameof(directingPrefabList)}에 값이 할당되지 않았습니다.");
        }
    }

    public void StageReset()
    {
        Destroy(gameOverDirectorObject);
    }

}
