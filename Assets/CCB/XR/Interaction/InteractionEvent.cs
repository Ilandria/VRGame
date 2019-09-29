using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace CCB.XR.Interaction
{
	[Serializable]
	public class InteractionEvent : UnityEvent<InputDevice, Transform>
	{
		
	}
}