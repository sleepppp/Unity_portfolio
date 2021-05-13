﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KSW
{
    //Stick입력 처리는 해당 스크립트가 처리합니다 
    public class PanelJoystick : UIBase, IPointerDownHandler,IDragHandler,IPointerUpHandler
    {
        [SerializeField] float m_maxDistance = 100f;

        RectTransform m_origin;
        Image m_stickImage;

        bool m_isDown = false;

        protected override void Start()
        {
            base.Start();

            m_origin = transform.Find("Origin") as RectTransform;
            m_stickImage = transform.Find("Stick").GetComponent<Image>();
        }

        void Update()
        {
            if(m_isDown)
            {
                Vector2 dir;
                float value;
                GetStickInfo(out dir, out value);
                GameEvent.instance.OnEventUpdateStickDrag(dir, value);
            }
        }

        public void GetStickInfo(out Vector2 dir, out float value)
        {
            dir = m_stickImage.transform.position - m_origin.position;
            value = dir.magnitude / m_maxDistance;
            dir.Normalize();
        }

        void SetStickPosition(Vector3 position)
        {
            Vector3 point = TransformScreenPointInCameraCanvas(position, m_stickImage.rectTransform);

            Vector3 dir = point - m_origin.position;
            float distance = dir.magnitude;

            if(distance > m_maxDistance)
            {
                point = m_origin.position + dir.normalized * m_maxDistance;
            }

            m_stickImage.rectTransform.position = point;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetStickPosition(eventData.position);
            m_isDown = true;

            Vector2 dir;
            float value;
            GetStickInfo(out dir, out value);

            GameEvent.instance.OnEventStartStickDrag(dir, value);
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetStickPosition(eventData.position);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            m_stickImage.rectTransform.position = m_origin.position;
            m_isDown = false;

            GameEvent.instance.OnEventEndStickDrag();
        }
    }
}
