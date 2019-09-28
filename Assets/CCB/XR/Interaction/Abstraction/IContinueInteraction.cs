using UnityEngine.XR;

namespace CCB.XR.Interaction.Abstraction
{
	public interface IContinueInteraction
	{
		void Raise(InputDevice inputDevice);
	}
}