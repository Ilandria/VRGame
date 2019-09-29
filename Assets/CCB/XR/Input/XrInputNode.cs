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

		private void EarlyUpdate()
		{
			currentInput = InputDevices.GetDeviceAtXRNode(targetNode);

			currentInput.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
			currentInput.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
			transform.localPosition = position;
			transform.localRotation = rotation;
		}

		private void Update()
		{
			for (int i = 0; i < globalInteractableCount; i++)
			{
				globalInteractables[i].OnGlobalInteraction(currentInput, transform);
			}

			switch (currentState)
			{
				case InteractionState.Begin:
					Debug.Log("Begin");
					currentInteractable.OnBeginInteraction(currentInput, transform);
					currentState = InteractionState.Continue;
					break;

				case InteractionState.Continue:
					Debug.Log("Continue");
					currentInteractable.OnContinueInteraction(currentInput, transform);
					break;

				case InteractionState.End:
					Debug.Log("End");
					currentInteractable.OnEndInteraction(currentInput, transform);
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