using UnityEngine.XR;

namespace MechGame.Interaction.InteractionTypes
{
	public class Always : Interaction
	{
		public override void OnBeginInteraction(InputDevice inputDevice)
		{
			onBeginInput.Invoke(inputDevice);
		}

		public override void OnEndInteraction(InputDevice inputDevice)
		{
			onEndInput.Invoke(inputDevice);
		}

		public override void OnInteraction(InputDevice inputDevice)
		{
			onInput.Invoke(inputDevice);
		}
	}
}