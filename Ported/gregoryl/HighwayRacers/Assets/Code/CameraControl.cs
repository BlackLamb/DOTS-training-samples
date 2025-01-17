﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace HighwayRacers {

	public class CameraControl : MonoBehaviour {

		public Rect bounds;
		public float moveSpeed = 20;
		public float transitionDuration = 3;

        public Vector3 CarCameraOffset = new Vector3(0, 1.5f, -1);

		Vector3 topDownPosition = new Vector3();
		Quaternion topDownRotation = new Quaternion();
		Vector3 carPosition = new Vector3();
		Quaternion carRotation = new Quaternion();
		Entity carRef = Entity.Null;

		public static CameraControl instance { get; private set; }

		public enum State {
			TOP_DOWN,
			TO_CAR,
			CAR,
			TO_TOP_DOWN,
		}
		public State state { get; private set; }
		private float time = 0;

		public void ToCarView(Entity car)
        {
			carRef = car;
			state = State.TO_CAR;
			time = 0;
			topDownPosition = transform.position;
			topDownRotation = transform.rotation;
		}

		public void ToTopDownView()
        {
			if (state != State.CAR)
				return;
			state = State.TO_TOP_DOWN;
			time = 0;
			carRef = Entity.Null;
		}

		void Awake() {
			if (instance != null) {
				Destroy (gameObject);
				return;
			}
			instance = this;
		}

		// Use this for initialization
		void Start () {

			topDownPosition = transform.position;
			topDownRotation = transform.rotation;

		}

		// Update is called once per frame
		void LateUpdate ()
        {
			float dt = Time.unscaledDeltaTime;
			time += dt;

			switch (state)
            {

			case State.TOP_DOWN:

				Vector2 v = new Vector2 ();

				if (Input.GetKey (KeyCode.LeftArrow)) {
					v.x = -moveSpeed;
				} else if (Input.GetKey (KeyCode.RightArrow)) {
					v.x = moveSpeed;
				} else {
					v.x = 0;
				}

				if (Input.GetKey (KeyCode.DownArrow)) {
					v.y = -moveSpeed;
				} else if (Input.GetKey (KeyCode.UpArrow)) {
					v.y = moveSpeed;
				} else {
					v.y = 0;
				}

				transform.position = new Vector3 (
					Mathf.Clamp (transform.position.x + v.x * dt, bounds.xMin, bounds.xMax),
					transform.position.y,
					Mathf.Clamp (transform.position.z + v.y * dt, bounds.yMin, bounds.yMax)
				);
				topDownPosition = transform.position;
				break;

			case State.TO_CAR:
			case State.CAR:
            {
                var em = World.Active?.EntityManager;
                if (em == null || carRef == Entity.Null)
                    break;
                var carState = em.GetComponentData<CarState>(carRef);
                Highway.instance.DotsHighway.GetWorldPosition(
                    carState.PositionOnTrack, carState.Lane, out float3 pos, out quaternion rot);
                pos += math.mul(rot, CarCameraOffset);
				carPosition = pos;
				carRotation = rot;

				if (time >= transitionDuration || state == State.CAR)
                {
					if (state == State.TO_CAR)
						state = State.CAR;
					transform.position = carPosition;
					transform.rotation = carRotation;
				}
                else
                {
					transform.position = Vector3.Lerp(topDownPosition, carPosition, time / transitionDuration);
					transform.rotation = Quaternion.Slerp(topDownRotation, carRotation, time / transitionDuration);
				}
				break;
            }

			case State.TO_TOP_DOWN:

				transform.position = Vector3.Lerp (carPosition, topDownPosition, time / transitionDuration);
				transform.rotation = Quaternion.Slerp (carRotation, topDownRotation, time / transitionDuration);

				if (time >= transitionDuration || state == State.CAR) {
					state = State.TOP_DOWN;
				}
				break;

			}

		}

		void OnDestroy() {

			if (instance == this) {
				instance = null;
			}

		}
	}

}
