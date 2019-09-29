using UnityEngine;
using UnityEngine.XR;

public interface IGlobalInteraction
{
	void Raise(InputDevice inputDevice, Transform inputTransform);
}