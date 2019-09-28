using CCB.XR.Interaction.Abstraction;
using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Interaction
{
	public class GlobalInteractable : MonoBehaviour, IGlobalInteractable
	{
		[SerializeField]
		private string interactableLayerName = "Interactable";

		private IGlobalInteraction[] globalInteractions = null;

		private int globalInteractionsCount;

		private void Awake()
		{
			globalInteractions = GetComponents<IGlobalInteraction>();
			globalInteractionsCount = globalInteractions.Length;
		}

		private void OnEnable()
		{
			int interactableLayer = LayerMask.NameToLayer(interactableLayerName);

			if (gameObject.layer != interactableLayer)
			{
				// TODO: Better logging & string lookups.
				Debug.LogWarning($"{gameObject.name} is not on the {interactableLayerName} layer but has an Interactable component!");
			}
		}

		public void OnGlobalInteraction(InputDevice inputDevice)
		{
			for (int i = 0; i < globalInteractionsCount; i++)
			{
				globalInteractions[i].Raise(inputDevice);
			}
		}
	}
}