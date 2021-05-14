﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    //게임의 룰 처리는 이곳에서 처리합니다.
    //스테이지 별로 다른 룰이 적용된다 하면 GameMode를 상속 받아서 구현합니다.
    public class GameMode : MonoBehaviour
    {
        [SerializeField] protected PlayerController m_playerController;
        [SerializeField] protected MovementObject m_playerObject;
        [SerializeField] protected Camera m_mainCamera;

        public PlayerController playerContoller { get { return m_playerController; } }
        public MovementObject playerObject { get { return m_playerObject; } }
        public Camera mainCamera { get { return m_mainCamera; } }

        public virtual void Start()
        {
            if (m_playerController == null)
                m_playerController = FindObjectOfType<PlayerController>();
            if (m_mainCamera == null)
                m_mainCamera = Camera.main;

            m_playerController.SetTarget(m_playerObject);

            GameEvent.instance.OnEventFadeIn(3f);
        }
    }
}