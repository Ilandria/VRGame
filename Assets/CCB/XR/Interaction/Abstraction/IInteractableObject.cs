using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Interaction.Abstraction
{
	public interface IInteractableObject
	{
		bool IsInUse { get; }

		void OnBeginInteraction(InputDevice inputDevice, Transform inputTransform);

		void OnContinueInteraction(InputDevice inputDevice, Transform inputTransform);

		void OnEndInteraction(InputDevice inputDevice, Transform inputTransform);
	}
}