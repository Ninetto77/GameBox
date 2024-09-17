using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
	[SerializeField] private LayerMask enemyMask;
	private Camera _camera;
	void Start()
	{
		_camera = GetComponent<Camera>();

		//Cursor.lockState = CursorLockMode.Locked; //çàêðåïëÿåò â öåíòð êóðñîð
		//Cursor.visible = false;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, enemyMask)) 
			{
				GameObject hitObject = hit.transform.gameObject;
				//ReactiveTarget target = hit.transform.GetComponent<ReactiveTarget>();
				if (hitObject != null)
				{
					//target.ReactHit();
				}
				else
				{
				}

			}

		}
	}
}
