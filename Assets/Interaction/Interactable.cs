using UnityEngine;
using UnityEngine.XR;

namespace MechGame.Interaction
{
	public class Interactable : MonoBehaviour
	{
		public bool IsInUse { get; private set; }
		public delegate void InteractionHandler(InputDevice inputDevice);

		[SerializeField]
		private string interactableLayerName = "Interactable";

		public event InteractionHandler onBeginInteraction = delegate { };
		public event InteractionHandler onInteraction = delegate { };
		public event InteractionHandler onEndInteraction = delegate { };

		private void OnEnable()
		{
			int interactableLayer = LayerMask.NameToLayer(interactableLayerName);

			if (gameObject.layer != interactableLayer)
			{
				// TODO: Better logging & string lookups.
				Debug.LogWarning($"{gameObject.name} is not on the {interactableLayerName} layer but has an Interactable component!");
			}
		}

		public void OnBeginInteraction(InputDevice inputDevice)
		{
			IsInUse = true;
			onBeginInteraction(inputDevice);
		}

		public void OnInteraction(InputDevice inputDevice)
		{
			onInteraction(inputDevice);
		}

		public void OnEndInteraction(InputDevice inputDevice)
		{
			IsInUse = false;
			onEndInteraction(inputDevice);
		}
	}
}