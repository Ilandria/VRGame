using UnityEngine;
using UnityEngine.XR;

namespace MechGame.Interaction
{
	[RequireComponent(typeof(Interactable))]
	public abstract class Interaction : MonoBehaviour
	{
		public InteractionEvent onBeginInput;
		public InteractionEvent onInput;
		public InteractionEvent onEndInput;

		private Interactable interactable;

		private void OnEnable()
		{
			interactable = GetComponent<Interactable>();
			interactable.onBeginInteraction += OnBeginInteraction;
			interactable.onInteraction += OnInteraction;
			interactable.onEndInteraction += OnEndInteraction;
		}

		private void OnDisable()
		{
			interactable.onBeginInteraction -= OnBeginInteraction;
			interactable.onInteraction -= OnInteraction;
			interactable.onEndInteraction -= OnEndInteraction;
		}

		public abstract void OnBeginInteraction(InputDevice inputDevice);

		public abstract void OnInteraction(InputDevice inputDevice);

		public abstract void OnEndInteraction(InputDevice inputDevice);
	}
}