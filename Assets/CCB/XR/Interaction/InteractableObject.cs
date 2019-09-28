using CCB.XR.Interaction.Abstraction;
using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Interaction
{
	[RequireComponent(typeof(Collider))]
	public class InteractableObject : MonoBehaviour, IInteractableObject
	{
		public bool IsInUse { get; private set; }

		[SerializeField]
		private string interactableLayerName = "Interactable";

		private IBeginInteraction[] beginInteractions;
		private IContinueInteraction[] continueInteractions;
		private IEndInteraction[] endInteractions;
		private int beginInteractionsCount;
		private int continueInteractionsCount;
		private int endInteractionsCount;

		private void Awake()
		{
			beginInteractions = GetComponents<IBeginInteraction>();
			continueInteractions = GetComponents<IContinueInteraction>();
			endInteractions = GetComponents<IEndInteraction>();
			beginInteractionsCount = beginInteractions.Length;
			continueInteractionsCount = continueInteractions.Length;
			endInteractionsCount = endInteractions.Length;
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

		public void OnBeginInteraction(InputDevice inputDevice)
		{
			IsInUse = true;

			for (int i = 0; i < beginInteractionsCount; i++)
			{
				beginInteractions[i].Raise(inputDevice);
			}
		}

		public void OnContinueInteraction(InputDevice inputDevice)
		{
			for (int i = 0; i < continueInteractionsCount; i++)
			{
				continueInteractions[i].Raise(inputDevice);
			}
		}

		public void OnEndInteraction(InputDevice inputDevice)
		{
			IsInUse = false;

			for (int i = 0; i < endInteractionsCount; i++)
			{
				endInteractions[i].Raise(inputDevice);
			}
		}
	}
}