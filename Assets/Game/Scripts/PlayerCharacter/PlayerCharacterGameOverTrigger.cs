﻿using UnityEngine;
using System.Collections;

public class PlayerCharacterGameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            // 게임오버 처리

            GameStageManager.Instance.IsGameOver = true;
        }
    }
}