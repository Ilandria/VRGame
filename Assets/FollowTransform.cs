using UnityEngine;

public class FollowTransform : MonoBehaviour
{
	[SerializeField]
	private Transform targetObject = null;

	public void Update()
	{
		transform.position = targetObject.position;
		transform.rotation = targetObject.rotation;
	}
}