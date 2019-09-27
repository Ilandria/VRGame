﻿using MechGame.Interaction;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace MechGame.Input
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	public class XrInputNode : MonoBehaviour
	{
		private enum InteractionState
		{
			Idle,
			Begin,
			Continue,
			End
		}

		[SerializeField]
		private XRNode targetNode = XRNode.RightHand;

		[SerializeField]
		private Interactable[] globalInteractables = null;

		private Interactable currentInteractable;
		private InputDevice currentInput;
		private InteractionState currentState;

		private void OnEnable()
		{
			currentState = InteractionState.Idle;
			currentInput = InputDevices.GetDeviceAtXRNode(targetNode);

			foreach (Interactable interactable in globalInteractables)
			{
				interactable.OnBeginInteraction(currentInput);
			}
		}

		private void Update()
		{
			currentInput = InputDevices.GetDeviceAtXRNode(targetNode);

			// Rotation tracking isn't needed since we're assuming sphere colliders. This saves a bit of CPU time.
			currentInput.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
			transform.localPosition = position;

			foreach (Interactable interactable in globalInteractables)
			{
				interactable.OnInteraction(currentInput);
			}

			switch (currentState)
			{
				case InteractionState.Begin:
					currentInteractable.OnBeginInteraction(currentInput);
					currentState = InteractionState.Continue;
					break;

				case InteractionState.Continue:
					currentInteractable.OnInteraction(currentInput);
					break;

				case InteractionState.End:
					currentInteractable.OnEndInteraction(currentInput);
					currentState = InteractionState.Idle;
					break;

				default:
					break;
			}
		}

		private void OnDisable()
		{
			currentInput = InputDevices.GetDeviceAtXRNode(targetNode);

			foreach (Interactable interactable in globalInteractables)
			{
				interactable.OnEndInteraction(currentInput);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			TryBeginInteration(other);
		}

		private void OnTriggerStay(Collider other)
		{
			TryBeginInteration(other);
		}

		private void OnTriggerExit(Collider other)
		{
			if (currentState != InteractionState.Idle && other.GetComponent<Interactable>() == currentInteractable)
			{
				currentState = InteractionState.End;
			}
		}

		private void TryBeginInteration(Collider other)
		{
			if (currentState == InteractionState.Idle)
			{
				Interactable interactable = other.GetComponent<Interactable>();

				if (!interactable.IsInUse)
				{
					currentInteractable = interactable;
					currentState = InteractionState.Begin;
				}
			}
		}
	}
}