using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Behaviours
{
	public class TrackWhileHeld : MonoBehaviour
	{
		public void BeginGrab(InputDevice inputDevice)
		{

		}

		public void Hold(InputDevice inputDevice)
		{
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 devicePosition);
			inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion deviceRotation);
			transform.rotation = deviceRotation;
			transform.position = devicePosition;
		}
	}
}