using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Behaviours
{
	public class SnapTo : MonoBehaviour
	{
		public void Track(InputDevice inputDevice, Transform inputTransform)
		{
			transform.position = inputTransform.position;
			transform.rotation = inputTransform.rotation;
		}
	}
}