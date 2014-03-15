using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	
	// Public Variables
	public static PlayerControls instance;
	
	public Transform target;
	public float distance = 5f;
	public float distanceMin = 1f;
	public float distanceMax = 10f;
	public float distanceSmooth = 0.05f;
	public float xMouseSensitivity = 5f;
	public float yMouseSensitivity = 5f;
	public float mouseWheelSensitivity = 5f;
	public float xSmooth = 0.05f;
	public float ySmooth = 0.1f;
	public float yMinLimit = -40f;
	public float yMaxLimit = 80f;
	
	// Private Variables
	private float mouseX = 0f;
	private float mouseY = 0f;
	private float velX = 0f;
	private float velY = 0f;
	private float velZ = 0f;
	private float velDistance = 0f;
	private float startDistance = 0f;
	private Vector3 position = Vector3.zero;
	private Vector3 desiredPosition = Vector3.zero;
	private float desiredDistance = 0f;	
	
	void Awake()
	{
		instance = this;
		//UseExistingOrCreateNewMainCamera();
	}
	// Use this for initialization
	void Start()
	{
		distance = Mathf.Clamp(distance, distanceMin, distanceMax);
		startDistance = distance;
		Reset();
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if(target == null)
		{
			return;
		}
		
		HandlePlayerInput();
		
		CalculateDesiredPosition();
		
		UpdatePosition();
		
	}
	
	void HandlePlayerInput()
	{
		var deadZone = 0.01f;
		
		if(Input.GetMouseButton(1))
		{
			// The right mouse button is down get mouse Axis input
			mouseX += Input.GetAxis("Mouse X") * xMouseSensitivity;
			mouseY -= Input.GetAxis("Mouse Y") * yMouseSensitivity;
		}
		
		// This is where the limit is on Mouse Y
		mouseY = Helper.ClampAngle(mouseY, yMinLimit, yMaxLimit);
		
		if(Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone)
		{
			desiredDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity, distanceMin, distanceMax);
		}
	}
	
	void CalculateDesiredPosition()
	{
		// Evaluate distance
		distance = Mathf.SmoothDamp(distance, desiredDistance, ref velDistance, distanceSmooth);
		
		// Calculate desired position
		desiredPosition = CalculatePosition(mouseY, mouseX, distance);
	}
	
	Vector3 CalculatePosition(float rotationX, float rotationY, float dist)
	{
		Vector3 direction = new Vector3(0, 0, -dist);
		Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
		return target.position + rotation * direction;
	}
	
	void UpdatePosition()
	{
		var posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, xSmooth);
		var posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, ySmooth);
		var posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, xSmooth);
		position = new Vector3(posX, posY, posZ);
		
		transform.position = position;
		
		transform.LookAt(target);
	}
	
	public void Reset()
	{
		mouseX = 0;
		mouseY = 10;
		distance = startDistance;
		desiredDistance = distance;
	}
	
	// If there isn't a main camera in the scene this will create one
	public static void UseExistingOrCreateNewMainCamera()
	{
		GameObject tempCamera;
		GameObject targetLookAt;
		GameObject targetLookAt2;
		PlayerControls myCamera;
		
		if(Camera.mainCamera != null)
		{
			tempCamera = Camera.mainCamera.gameObject;
		}
		else
		{
			tempCamera = new GameObject("MainCamera");
			tempCamera.AddComponent("Camera");
			tempCamera.tag = "MainCamera";
		}
		
		tempCamera.AddComponent("PlayerControls");
		myCamera = tempCamera.GetComponent("PlayerControls") as PlayerControls;
		
		targetLookAt = GameObject.Find("targetLookAt") as GameObject;
		targetLookAt2 = GameObject.Find("3rd Person Controller") as GameObject;
		
		if(targetLookAt == null)
		{
			targetLookAt = new GameObject("targetLookAt");
			targetLookAt.transform.position = Vector3.zero;

		}

		myCamera.target = targetLookAt.transform;
		
		//added by Fadi 9/25/2013
		if(Application.loadedLevelName == "PAVWalkthrough")
		{
			myCamera.target = targetLookAt2.transform;
		}
		
		
		
	}
}
