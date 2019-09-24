using UnityEngine.XR;

namespace MechGame.Interaction.InteractionTypes
{
	public class Grab : Interaction
	{
		private bool wasGrabbingBeforeInteraction = false;
		private bool isGrabbing = false;

		public override void OnBeginInteraction(InputDevice inputDevice)
		{
			inputDevice.TryGetFeatureValue(CommonUsages.grip, out float grip);
			wasGrabbingBeforeInteraction = grip >= 0.5f;
		}

		public override void OnEndInteraction(InputDevice inputDevice)
		{
			if (isGrabbing)
			{
				onEndInput.Invoke(inputDevice);
				isGrabbing = false;
			}

			wasGrabbingBeforeInteraction = false;
		}

		public override void OnInteraction(InputDevice inputDevice)
		{
			inputDevice.TryGetFeatureValue(CommonUsages.grip, out float grip);

			if (wasGrabbingBeforeInteraction)
			{
				if (grip < 0.5f)
				{
					wasGrabbingBeforeInteraction = false;
				}
			}
			else
			{
				if (isGrabbing)
				{
					if (grip >= 0.5f)
					{
						onInput.Invoke(inputDevice);
					}
					else
					{
						onEndInput.Invoke(inputDevice);
						isGrabbing = false;
						wasGrabbingBeforeInteraction = false;
					}
				}
				else if (grip >= 0.5f)
				{
					onBeginInput.Invoke(inputDevice);
					isGrabbing = true;
				}
			}
		}
	}
}