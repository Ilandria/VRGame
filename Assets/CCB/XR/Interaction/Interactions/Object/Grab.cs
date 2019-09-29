﻿using CCB.XR.Interaction.Abstraction;
using System;
using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Interaction.Interactions.Object
{
	[Serializable]
	[RequireComponent(typeof(IInteractableObject))]
	public class Grab : MonoBehaviour, IBeginInteraction, IContinueInteraction, IEndInteraction
	{
		public InteractionEvent onBeginGrab;
		public InteractionEvent onContinueGrab;
		public InteractionEvent onReleaseGrab;

		private bool wasGrabbingBeforeInteraction = false;
		private bool isGrabbing = false;

		void IBeginInteraction.Raise(InputDevice inputDevice, Transform inputTransform)
		{
			inputDevice.TryGetFeatureValue(CommonUsages.grip, out float grip);
			wasGrabbingBeforeInteraction = grip >= 0.5f;
		}

		void IContinueInteraction.Raise(InputDevice inputDevice, Transform inputTransform)
		{
			inputDevice.TryGetFeatureValue(CommonUsages.grip, out float grip);

			if (wasGrabbingBeforeInteraction)
			{
				if (grip < 0.5f)
				{
					wasGrabbingBeforeInteraction = false;
				}
			}
			else
			{
				if (isGrabbing)
				{
					if (grip >= 0.5f)
					{
						onContinueGrab.Invoke(inputDevice, inputTransform);
					}
					else
					{
						onReleaseGrab.Invoke(inputDevice, inputTransform);
						isGrabbing = false;
						wasGrabbingBeforeInteraction = false;
					}
				}
				else if (grip >= 0.5f)
				{
					onBeginGrab.Invoke(inputDevice, inputTransform);
					isGrabbing = true;
				}
			}
		}

		void IEndInteraction.Raise(InputDevice inputDevice, Transform inputTransform)
		{
			if (isGrabbing)
			{
				onReleaseGrab.Invoke(inputDevice, inputTransform);
				isGrabbing = false;
			}

			wasGrabbingBeforeInteraction = false;
		}
	}
}