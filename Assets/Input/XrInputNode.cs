using MechGame.Interaction;
using UnityEngine;
using UnityEngine.XR;

namespace MechGame.Input
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	public class XrInputNode : MonoBehaviour
	{
		[SerializeField]
		private XRNode targetNode = XRNode.RightHand;

		private Interactable currentInteractable;
		private InputDevice currentInput;
		private bool isInteracting;

		private void Update()
		{
			currentInput = InputDevices.GetDeviceAtXRNode(targetNode);

			// Rotation tracking isn't needed since we're assuming sphere colliders. This saves a bit of CPU time.
			currentInput.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
			transform.localPosition = position;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!isInteracting)
			{
				TryBeginInteration(other);
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (isInteracting)
			{
				if (other.GetComponent<Interactable>() == currentInteractable)
				{
					currentInteractable.OnInteraction(currentInput);
				}
			}
			else
			{
				TryBeginInteration(other);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (isInteracting && other.GetComponent<Interactable>() == currentInteractable)
			{
				currentInteractable.OnEndInteraction(currentInput);
				isInteracting = false;
			}
		}

		private void TryBeginInteration(Collider other)
		{
			Interactable interactable = other.GetComponent<Interactable>();

			if (!interactable.IsInUse)
			{
				currentInteractable = other.GetComponent<Interactable>();
				currentInteractable.OnBeginInteraction(currentInput);
				isInteracting = true;
			}
		}
	}
}