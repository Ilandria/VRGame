using CCB.XR.Interaction;
using CCB.XR.Interaction.Abstraction;
using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Input
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
		private GlobalInteractable[] globalInteractables = null;
		private int globalInteractableCount;

		private IInteractableObject currentInteractable;
		private InputDevice currentInput;
		private InteractionState currentState;

		private void Awake()
		{
			globalInteractableCount = globalInteractables.Length;
		}

		private void OnEnable()
		{
			currentState = InteractionState.Idle;
			currentInput = InputDevices.GetDeviceAtXRNode(targetNode);
		}

		private void Update()
		{
			currentInput = InputDevices.GetDeviceAtXRNode(targetNode);

			// Rotation tracking isn't needed since we're assuming sphere colliders. This saves a bit of CPU time.
			currentInput.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
			transform.localPosition = position;

			for (int i = 0; i < globalInteractableCount; i++)
			{
				globalInteractables[i].OnGlobalInteraction(currentInput);
			}

			switch (currentState)
			{
				case InteractionState.Begin:
					currentInteractable.OnBeginInteraction(currentInput);
					currentState = InteractionState.Continue;
					break;

				case InteractionState.Continue:
					currentInteractable.OnContinueInteraction(currentInput);
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
			if (currentState != InteractionState.Idle && other.GetComponent<IInteractableObject>() == currentInteractable)
			{
				currentState = InteractionState.End;
			}
		}

		private void TryBeginInteration(Collider other)
		{
			if (currentState == InteractionState.Idle)
			{
				IInteractableObject interactable = other.GetComponent<IInteractableObject>();

				if (!interactable.IsInUse)
				{
					currentInteractable = interactable;
					currentState = InteractionState.Begin;
				}
			}
		}
	}
}