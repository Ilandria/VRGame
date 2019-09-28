using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Behaviours
{
	public class SnapTo : MonoBehaviour
	{
		public void Track(InputDevice inputDevice)
		{
			inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 inputPosition);
			inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion inputRotation);

			transform.position = inputPosition;
			transform.rotation = inputRotation;
		}
	}
}