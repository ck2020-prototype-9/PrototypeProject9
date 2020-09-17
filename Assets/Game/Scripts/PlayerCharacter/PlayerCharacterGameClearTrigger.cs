using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterGameClearTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            // 게임 클리어 처리

            GameStageManager.Instance.IsGameClear = true;
        }
    }
}
