
using UnityEngine;
using System.Collections;

namespace Rhythmify {
	public class Pulse : _AbstractRhythmObject {
		public Vector3[] scaleVectors;
		public int[] indices;
		public int offset;
		public bool local;
		public bool rigid;
		public bool spherical;
		public float speed = 10f;
		
		private Rigidbody rigidBody;
		
		override protected void init() {
			if (rigid) {
				rigidBody = gameObject.GetComponent<Rigidbody>();
				if (rigidBody == null) {
					Debug.LogError("The GameObject " + gameObject + " has no RigidBody component attached!");
					Debug.Break();
				}
			}
		}
		
		override protected void rhythmUpdate(int beat) {
			beat *= 2;
			int size = scaleVectors.Length;
			
			if (size <= 1) {
				return;
			}
			
			int idx = beat + offset;
			
			Vector3 startScale;
			Vector3 endScale;
			
			if (indices.Length > 0) {
				int idxA = indices[idx % indices.Length];
				int idxB = indices[(idx + 1) % indices.Length];
				startScale = (scaleVectors [idxA % size]);
				endScale = (scaleVectors [idxB % size]);
			}
			else {
				startScale = (scaleVectors [idx % size]);
				endScale = (scaleVectors [(idx + 1) % size]);
			}
			
			StartCoroutine(scale(startScale, endScale, secondsPerBeat));
		}
		
		private IEnumerator scale(Vector3 startScale, Vector3 endScale, float duration) {
			float startTime = Time.time;

				while (Time.time <= startTime + duration) {
					float lerpPercent = Mathf.Clamp01(speed*((Time.time - startTime) / duration));
					transform.localScale = Vector3.Slerp(startScale, endScale, lerpPercent);
					yield return null;
				}
			}
	}
}