﻿using UnityEngine;
using System.Collections;

namespace Assets.Game.Scripts
{
    public class GameOverTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Terrain")
            {
                // 게임오버 처리

                GameStageManager.Instance.GameOverManager.IsGameOver = true;
            }
        }
    }
}