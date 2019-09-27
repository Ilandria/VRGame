using UnityEngine;
using UnityEngine.XR;

namespace MechGame.Behaviours
{
	public class TrackWhileHeld : MonoBehaviour
	{
		private Vector3 grabOffset;
		private Quaternion rotationOffset;

		public void BeginGrab(InputDevice inputDevice)
		{
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 inputLocalPosition);
			grabOffset = transform.position - inputLocalPosition;
			//rotationOffset = transform.rotation * Quaternion.Inverse(interactionData.Rotation);
		}

		public void Hold(InputDevice inputDevice)
		{
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 inputLocalPosition);
			transform.position = inputLocalPosition + grabOffset;
			//transform.rotation = interactionData.Rotation * rotationOffset;
		}
	}
}