﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: The bow
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class Longbow : MonoBehaviour
	{
		public enum Handedness { Left, Right };

		public Handedness currentHandGuess = Handedness.Left;
		private float timeOfPossibleHandSwitch = 0f;
		private float timeBeforeConfirmingHandSwitch = 1.5f;
		private bool possibleHandSwitch = false;

		public Transform pivotTransform;
		public Transform handleTransform;

		private Hand hand;
		//private ArrowHand arrowHand;

		public Transform nockTransform;
		public Transform nockRestTransform;

		public bool autoSpawnArrowHand = true;
		public ItemPackage arrowHandItemPackage;
		public GameObject arrowHandPrefab;

		public bool nocked;
		public bool pulled;

		private const float minPull = 0.05f;
		private const float maxPull = 0.5f;
		private float nockDistanceTravelled = 0f;
		private float hapticDistanceThreshold = 0.01f;
		private float lastTickDistance;
		private const float bowPullPulseStrengthLow = 100;
		private const float bowPullPulseStrengthHigh = 500;
		private Vector3 bowLeftVector;

		public float arrowMinVelocity = 3f;
		public float arrowMaxVelocity = 30f;
		private float arrowVelocity = 30f;

		private float minStrainTickTime = 0.1f;
		private float maxStrainTickTime = 0.5f;
		private float nextStrainTick = 0;

		private bool lerpBackToZeroRotation;
		private float lerpStartTime;
		private float lerpDuration = 0.15f;
		private Quaternion lerpStartRotation;

		private float nockLerpStartTime;

		private Quaternion nockLerpStartRotation;

		public float drawOffset = 0.06f;

		public LinearMapping bowDrawLinearMapping;
        
		private Vector3 lateUpdatePos;
		private Quaternion lateUpdateRot;

		public SoundBowClick drawSound;
		private float drawTension;
		public SoundPlayOneshot arrowSlideSound;
		public SoundPlayOneshot releaseSound;
		public SoundPlayOneshot nockSound;

		SteamVR_Events.Action newPosesAppliedAction;


		//-------------------------------------------------
		private void OnAttachedToHand( Hand attachedHand )
		{
			hand = attachedHand;
		}


		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
			// Reset transform since we cheated it right after getting poses on previous frame
			//transform.localPosition = Vector3.zero;
			//transform.localRotation = Quaternion.identity;

			// Update handedness guess
			EvaluateHandedness();
		}






		//-------------------------------------------------
		public void StartRotationLerp()
		{
			lerpStartTime = Time.time;
			lerpBackToZeroRotation = true;
			lerpStartRotation = pivotTransform.localRotation;

			Util.ResetTransform( nockTransform );
		}

		//-------------------------------------------------
		private void EvaluateHandedness()
		{
            var handType = hand.handType;

			if ( handType == SteamVR_Input_Sources.LeftHand )// Bow hand is further left than arrow hand.
			{
				// We were considering a switch, but the current controller orientation matches our currently assigned handedness, so no longer consider a switch
				if ( possibleHandSwitch && currentHandGuess == Handedness.Left )
				{
					possibleHandSwitch = false;
				}

				// If we previously thought the bow was right-handed, and were not already considering switching, start considering a switch
				if ( !possibleHandSwitch && currentHandGuess == Handedness.Right )
				{
					possibleHandSwitch = true;
					timeOfPossibleHandSwitch = Time.time;
				}

				// If we are considering a handedness switch, and it's been this way long enough, switch
				if ( possibleHandSwitch && Time.time > ( timeOfPossibleHandSwitch + timeBeforeConfirmingHandSwitch ) )
				{
					currentHandGuess = Handedness.Left;
					possibleHandSwitch = false;
				}
			}
			else // Bow hand is further right than arrow hand
			{
				// We were considering a switch, but the current controller orientation matches our currently assigned handedness, so no longer consider a switch
				if ( possibleHandSwitch && currentHandGuess == Handedness.Right )
				{
					possibleHandSwitch = false;
				}

				// If we previously thought the bow was right-handed, and were not already considering switching, start considering a switch
				if ( !possibleHandSwitch && currentHandGuess == Handedness.Left )
				{
					possibleHandSwitch = true;
					timeOfPossibleHandSwitch = Time.time;
				}

				// If we are considering a handedness switch, and it's been this way long enough, switch
				if ( possibleHandSwitch && Time.time > ( timeOfPossibleHandSwitch + timeBeforeConfirmingHandSwitch ) )
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
			if ( currentHandGuess == Handedness.Left )
			{
				pivotTransform.localScale = new Vector3( 1f, 1f, 1f );
			}
			else
			{
				pivotTransform.localScale = new Vector3( 1f, -1f, 1f );
			}
		}

		//-------------------------------------------------
		private void ShutDown()
		{
			if ( hand != null && hand.otherHand.currentAttachedObject != null )
			{
				if ( hand.otherHand.currentAttachedObject.GetComponent<ItemPackageReference>() != null )
				{
					if ( hand.otherHand.currentAttachedObject.GetComponent<ItemPackageReference>().itemPackage == arrowHandItemPackage )
					{
						hand.otherHand.DetachObject( hand.otherHand.currentAttachedObject );
					}
				}
			}
		}


		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}


		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
			gameObject.SetActive( true );
			OnAttachedToHand( hand );
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			Destroy( gameObject );
		}


		//-------------------------------------------------
		void OnDestroy()
		{
			ShutDown();
		}
	}
}
