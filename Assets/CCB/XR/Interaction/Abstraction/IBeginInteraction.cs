using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Interaction.Abstraction
{
	public interface IBeginInteraction
	{
		void Raise(InputDevice inputDevice, Transform inputTransform);
	}
}