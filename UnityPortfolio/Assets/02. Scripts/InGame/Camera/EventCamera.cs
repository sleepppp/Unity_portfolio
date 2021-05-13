﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KSW
{
    public class EventCamera : MonoBehaviour
    {
        // =========================================================================
        public event Action EventEndEvent;
        // =========================================================================
        Vector3 m_targetPosition;
        Vector3 m_startPosition;
        Quaternion m_targetRotation;
        Quaternion m_startRotation;
        float m_durationTime;

        float m_currentTime;

        bool m_isPlay = false;
        // =========================================================================
        public bool isPlay { get { return m_isPlay; } }
        // =========================================================================
        private void LateUpdate()
        {
            if (m_isPlay)
            {
                m_currentTime += Time.deltaTime;
                float ratio = m_currentTime / m_durationTime;
                if (ratio >= 1f)
                {
                    transform.position = m_targetPosition;
                    transform.rotation = m_targetRotation;
                    m_isPlay = false;
                    EventEndEvent?.Invoke();
                    EventEndEvent = null;
                }
                else
                {
                    transform.position = Vector3.Slerp(m_startPosition, m_targetPosition, ratio);
                    transform.rotation = Quaternion.Slerp(m_startRotation, m_targetRotation, ratio);
                }
            }
        }
        // =========================================================================
        public void PlayEvent(float durationTime, Vector3 targetPostion, Quaternion targetRotation)
        {
            m_durationTime = durationTime;
            m_targetPosition = targetPostion;
            m_targetRotation = targetRotation;
            m_currentTime = 0f;
            m_isPlay = true;
        }
        // =========================================================================
        public void ReverseEvent()
        {
            m_currentTime = 0f;

            Vector3 tempVector = m_targetPosition;
            m_targetPosition = m_startPosition;
            m_startPosition = tempVector;

            Quaternion tempRotation = m_targetRotation;
            m_targetRotation = m_startRotation;
            m_startRotation = tempRotation;

            m_isPlay = true;
        }
    }
}