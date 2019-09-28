using CCB.XR.Interaction.Abstraction;
using System;
using UnityEngine;
using UnityEngine.XR;

namespace CCB.XR.Interaction.Interactions.Global
{
	[Serializable]
	[RequireComponent(typeof(IGlobalInteractable))]
	public class Always : MonoBehaviour, IGlobalInteraction
	{
		public InteractionEvent onRaised;

		public void Raise(InputDevice inputDevice)
		{
			onRaised.Invoke(inputDevice);
		}
	}
}