using UnityEngine.XR;

namespace CCB.XR.Interaction.Abstraction
{
	public interface IInteractableObject
	{
		bool IsInUse { get; }

		void OnBeginInteraction(InputDevice inputDevice);

		void OnContinueInteraction(InputDevice inputDevice);

		void OnEndInteraction(InputDevice inputDevice);
	}
}