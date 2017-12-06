using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;

namespace V
{
	
	public class SystemInput : Singleton<SystemInput> {
		protected SystemInput(){}

		#if UNITY_IOS || UNITY_ANDROID
		[SerializeField] private string Button_A;
		#else
		[SerializeField] private KeyCode Key_A;
		#endif

		Action Key_A_Hold;
		Action Key_A_Down;
		Action Key_A_Up;


		Action Left_Mouse_Hold;
		Action Left_Mouse_Down;
		Action Left_Mouse_Up;

		Vector2 inputVH;
		Vector2 currentVelocity;
		Vector2 currentVH;


		float smoothTime = 0.0f;


		void Start()
		{
			
		}
			
		void Update()
		{
			#if UNITY_IOS || UNITY_ANDROID
			Vector2 input = TouchLib.GetSwipe2D ();
			inputVH = input;
			CrossPlateformButton();
			#else
			inputVH.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
			inputVH.y = CrossPlatformInputManager.GetAxisRaw("Vertical");
			#endif

			currentVH = Vector2.SmoothDamp (currentVH, inputVH, ref currentVelocity, smoothTime,
												 float.MaxValue, Time.deltaTime);
			StandaredKeyInput();

		}

		public Vector2 CurrentVH{get{ return currentVH;}}
		public float MouseScroll{get {return Input.GetAxis("Mouse ScrollWheel");}}
		public float MouseVertical{get {return  -Input.GetAxis("Mouse Y");}}
		public float MouseHorizontal{ get { return Input.GetAxis("Mouse X"); } }

		#if UNITY_IOS || UNITY_ANDROID
		void CrossPlateformButton()
		{
			//Hold Button A
			if (CrossPlatformInputManager.GetButton(Button_A))
			{

			}

			//Button A Up
			if (CrossPlatformInputManager.GetButtonUp(Button_A))
			{

			}

		}
		#else
		void StandaredKeyInput()
		{
			//Left Mouse Down
			if (Input.GetMouseButtonDown (0)) {
				if (Left_Mouse_Down != null) {
					Left_Mouse_Down ();
				}
			}

			//Left Mouse Hold
			if (Input.GetMouseButton(0)) {
				if (Left_Mouse_Hold != null) {
					Left_Mouse_Hold ();
				}
			}

			//Left Mouse Up
			if (Input.GetMouseButtonUp(0)) {
				if (Left_Mouse_Up != null) {
					Left_Mouse_Up();
				}
			}

			// Key A
			if (Input.GetKey (Key_A)) {
				if (Key_A_Hold != null) {
					Key_A_Hold ();
				}
			}

			if (Input.GetKeyDown (Key_A)) {
				if (Key_A_Down != null) {
					Key_A_Down();
				}
			}

			if (Input.GetKeyUp (Key_A)) {
				if (Key_A_Up != null) {
					Key_A_Up();
				}
			}
		}
		#endif

		public void OVERRIDE_SYSTEM_INPUT(EntityCore entity)
		{
			Debug.Log("Bind Key");
			Key_A_Hold = null;        Key_A_Hold = entity.Invoke_Key_A_Hold;  
			Key_A_Down = null;	      Key_A_Down = entity.Invoke_Key_A_Down;
			Key_A_Up = null;	      Key_A_Up = entity.Invoke_Key_A_Up;


			Left_Mouse_Hold = null;   Left_Mouse_Hold = entity.Invoke_L_Mouse_Hold;
			Left_Mouse_Down = null;   Left_Mouse_Down = entity.Invoke_L_Mouse_Down;
			Left_Mouse_Up = null;     Left_Mouse_Up = entity.Invoke_L_Mouse_Up;

		}

	}

	public static class TouchLib
	{
		static float startTime;
		static float endTime;

		static Vector2 startPos;
		static Vector2 endPos;
		static Vector2 move;

		static float minSwipDist = 20.0f;
		static float maxSwipDist = 100.0f;
		static float swipeDistance;

		static float maxTime = 1.0f;

		public static Vector2 GetSwipe2D()
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began)
				{
					startTime = Time.time;
					startPos = touch.position;
					return Vector2.zero;
				}
				else if (touch.phase == TouchPhase.Ended)
				{
					endTime = Time.time;
					endPos = touch.position;
					move = endPos - startPos;
					swipeDistance = move.magnitude;
					float swipeTime = endTime - startTime;
					if (swipeDistance > minSwipDist && swipeTime < maxTime)
					{
						return new Vector2(Mathf.Clamp(move.x / maxSwipDist, -1.0f, 1.0f),
							Mathf.Clamp(move.y / maxSwipDist, -1.0f, 1.0f));
					}
					return Vector2.zero;
				}
				return Vector2.zero;
			}
			return Vector2.zero;
		}

		static float perspectiveZoomSpeed = 0.5f;
		static float orthZoomSpeed = 0.5f;

		public static void ZoomInOut_ChangeFieldOfView()
		{
			if (Input.touchCount == 2)
			{
				float deltaMagnitudeDiff = GetDeltaMagnitudeDifferent();
				if (Camera.main.orthographic)
				{
					Camera.main.orthographicSize += deltaMagnitudeDiff * orthZoomSpeed;
					Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
				}
				else
				{
					Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
					Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
				}
			}
		}

		public static float GetDeltaMagnitudeDifferent()
		{
			Touch t1 = Input.GetTouch(0);
			Touch t2 = Input.GetTouch(1);
			Vector2 t1PrevPos = t1.position - t1.deltaPosition;
			Vector2 t2PrevPos = t2.position - t2.deltaPosition;

			float prevTouchMag = (t1PrevPos - t2PrevPos).magnitude;
			float currentMag = (t1.position - t2.position).magnitude;

			return prevTouchMag - currentMag;
		}
	}

	public static class GravitySensor
	{
		public static Vector3 GetAcceleration()
		{
			Vector3 dir = Vector3.zero;
			dir.x = -Input.acceleration.y;
			dir.z = Input.acceleration.x;
			if (dir.sqrMagnitude > 1)
				dir.Normalize();
			return dir;
		}
	}

    public enum InputIndex
    {
        A,
        B,
        C,
        D
    }
}
