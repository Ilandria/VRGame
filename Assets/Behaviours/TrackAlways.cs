using UnityEngine;
using UnityEngine.XR;

public class TrackAlways : MonoBehaviour
{
    public void Track(InputDevice inputDevice)
	{
		inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 inputPosition);
		inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion inputRotation);

		transform.position = inputPosition;
		transform.rotation = inputRotation;
	}
}