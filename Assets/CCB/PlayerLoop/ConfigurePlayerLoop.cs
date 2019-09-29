using UnityEngine;
using UnityEngine.Experimental.LowLevel;
using System.Reflection;
using LowLevel = UnityEngine.Experimental.LowLevel;

namespace CCB.PlayerLoop
{
	// TODO: Delete this class if EarlyUpdate ever becomes baked in to Unity's default player loop.
	public class ConfigurePlayerLoop : MonoBehaviour
	{
		private void OnEnable()
		{
			PlayerLoopSystem playerLoop = LowLevel.PlayerLoop.GetDefaultPlayerLoop();

			for (int i = 0; i < playerLoop.subSystemList.Length; i++)
			{
				PlayerLoopSystem subSystem = playerLoop.subSystemList[i];

				if (subSystem.type == typeof(UnityEngine.Experimental.PlayerLoop.EarlyUpdate))
				{
					foreach (MonoBehaviour monoBehaviour in FindObjectsOfType<MonoBehaviour>())
					{
						MethodInfo methodInfo = monoBehaviour.GetType().GetMethod("EarlyUpdate", BindingFlags.NonPublic | BindingFlags.Instance);

						if (methodInfo != null)
						{
							playerLoop.subSystemList[i].updateDelegate += () => methodInfo.Invoke(monoBehaviour, null);
						}
					}
				}
			}

			LowLevel.PlayerLoop.SetPlayerLoop(playerLoop);
		}

		private void OnDisable()
		{
			LowLevel.PlayerLoop.SetPlayerLoop(LowLevel.PlayerLoop.GetDefaultPlayerLoop());
		}
	}
}