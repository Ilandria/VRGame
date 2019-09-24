using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace MechGame.Interaction
{
	[Serializable]
	public class InteractionEvent : UnityEvent<InputDevice> { }
}