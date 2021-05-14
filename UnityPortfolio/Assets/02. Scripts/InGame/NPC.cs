﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class NPC : Character
    {
        static readonly int _animHashEmotion = Animator.StringToHash("Emotion");
        static readonly WaitForSeconds _waitDialogFade = new WaitForSeconds(1.5f);

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                GameEvent.instance.OnEventCreateInteractionHUD(this, m_hudPoint, OnEventInteraction);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameEvent.instance.OnEventRemoveInteractionHUD(this);
            }
        }

        void OnEventInteraction(InteractionHUD interactionHUD)
        {
            StartCoroutine(CoroutineDialog());

            //GameEvent.instance.OnEventPlayDialog(this);
        }

        IEnumerator CoroutineDialog()
        {
            CameraController cameraController = GameManager.instance.gameMode.mainCamera.GetComponent<CameraController>();
            Quaternion rotation = Quaternion.LookRotation(-transform.forward);
            cameraController.PlayEvent(0.7f, transform.position + Vector3.up * 1.5f + transform.forward * 1.5f, rotation,null);

            GameEvent.instance.OnEventFadeOut(0.8f);

            yield return _waitDialogFade;
            GameEvent.instance.OnEventPlayDialog(this);
            GameEvent.instance.OnEventFadeIn(0.8f);
        }
    }
}