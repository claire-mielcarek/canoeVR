﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class Oar : MonoBehaviour
    {
        public enum Handedness { Left, Right };

        public Handedness currentHandGuess = Handedness.Left;
        private float timeOfPossibleHandSwitch = 0f;
        private float timeBeforeConfirmingHandSwitch = 1.5f;
        private bool possibleHandSwitch = false;

        public Transform pivotTransform;

        private Hand hand;

        private Vector3 lateUpdatePos;
        private Quaternion lateUpdateRot;

        SteamVR_Events.Action newPosesAppliedAction;

        [SerializeField] public Canoe canoe;

        //-------------------------------------------------
        
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("topRight")) {
                Debug.Log("triggerRight");
                canoe.moveTopRight();
            }

            if (other.gameObject.CompareTag("topLeft"))
            {
                Debug.Log("triggerLeft");
                canoe.moveTopLeft();
            }
        }

        //-------------------------------------------------

        private void OnAttachedToHand(Hand attachedHand)
        {
            hand = attachedHand;
        }

        //-------------------------------------------------

        private void HandAttachedUpdate(Hand hand)
        {
            // Reset transform since we cheated it right after getting poses on previous frame
            //transform.localPosition = Vector3.zero;
            //transform.localRotation = Quaternion.identity;

            // Update handedness guess
            EvaluateHandedness();
        }

        //-------------------------------------------------
        private void EvaluateHandedness()
        {
            var handType = hand.handType;

            if (handType == SteamVR_Input_Sources.LeftHand)// Bow hand is further left than arrow hand.
            {
                // We were considering a switch, but the current controller orientation matches our currently assigned handedness, so no longer consider a switch
                if (possibleHandSwitch && currentHandGuess == Handedness.Left)
                {
                    possibleHandSwitch = false;
                }

                // If we previously thought the bow was right-handed, and were not already considering switching, start considering a switch
                if (!possibleHandSwitch && currentHandGuess == Handedness.Right)
                {
                    possibleHandSwitch = true;
                    timeOfPossibleHandSwitch = Time.time;
                }

                // If we are considering a handedness switch, and it's been this way long enough, switch
                if (possibleHandSwitch && Time.time > (timeOfPossibleHandSwitch + timeBeforeConfirmingHandSwitch))
                {
                    currentHandGuess = Handedness.Left;
                    possibleHandSwitch = false;
                }
            }
            else // Bow hand is further right than arrow hand
            {
                // We were considering a switch, but the current controller orientation matches our currently assigned handedness, so no longer consider a switch
                if (possibleHandSwitch && currentHandGuess == Handedness.Right)
                {
                    possibleHandSwitch = false;
                }

                // If we previously thought the bow was right-handed, and were not already considering switching, start considering a switch
                if (!possibleHandSwitch && currentHandGuess == Handedness.Left)
                {
                    possibleHandSwitch = true;
                    timeOfPossibleHandSwitch = Time.time;
                }

                // If we are considering a handedness switch, and it's been this way long enough, switch
                if (possibleHandSwitch && Time.time > (timeOfPossibleHandSwitch + timeBeforeConfirmingHandSwitch))
                {
                    currentHandGuess = Handedness.Right;
                    possibleHandSwitch = false;
                }
            }
        }


        //-------------------------------------------------
        private void DoHandednessCheck()
        {
            // Based on our current best guess about hand, switch bow orientation and arrow lerp direction
            if (currentHandGuess == Handedness.Left)
            {
                pivotTransform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                pivotTransform.localScale = new Vector3(1f, -1f, 1f);
            }
        }

        //-------------------------------------------------
        private void OnHandFocusLost(Hand hand)
        {
            gameObject.SetActive(false);
        }


        //-------------------------------------------------
        private void OnHandFocusAcquired(Hand hand)
        {
            gameObject.SetActive(true);
            OnAttachedToHand(hand);
        }


        //-------------------------------------------------
        private void OnDetachedFromHand(Hand hand)
        {
            Destroy(gameObject);
        }

    }
}
