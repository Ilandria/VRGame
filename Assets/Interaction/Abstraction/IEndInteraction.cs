using UnityEngine.XR;

namespace CCB.XR.Interaction.Abstraction
{
	public interface IEndInteraction
	{
		void Raise(InputDevice inputDevice);
	}
}