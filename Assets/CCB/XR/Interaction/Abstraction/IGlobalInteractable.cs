using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Interaction.Abstraction
{
	public interface IGlobalInteractable
	{
		void OnGlobalInteraction(InputDevice inputDevice, Transform inputTransform);
	}
}