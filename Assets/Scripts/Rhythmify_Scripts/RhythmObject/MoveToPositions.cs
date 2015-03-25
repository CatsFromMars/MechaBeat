/* @Author: Gkxd
 * 
 * Moves a GameObject to a list of specified positions
 * 
 * */
using UnityEngine;
using System.Collections;

namespace Rhythmify {
    public class MoveToPositions : _AbstractRhythmObject {
        public Vector3[] positions;
        public int[] indices;
        public int offset;
        public bool local;
        public bool relative;
        public bool rigid;
        public bool spherical;

        private Vector3 startPosition;
        private Rigidbody rigidBody;


        override protected void init() {
            if (relative) {
                if (local) {
                    startPosition = gameObject.transform.localPosition;
                }
                else {
                    startPosition = gameObject.transform.position;
                }
            }
            else {
                startPosition = Vector3.zero;
            }

            if (rigid) {
                rigidBody = gameObject.GetComponent<Rigidbody>();
                if (rigidBody == null) {
                    Debug.LogError("The GameObject " + gameObject + " has no RigidBody component attached!");
                    Debug.Break();
                }
            }
        }

        override protected void rhythmUpdate() {
            int size = positions.Length;
        
            if (size <= 1) {
                return;
            }
            
            int idx = getBeat() + offset;
            if (indices.Length > 0) {
                int idxA = indices[idx % indices.Length];
                int idxB = indices[(idx + 1) % indices.Length];
                StartCoroutine(move(positions [idxA % size], positions [idxB % size], secondsPerBeat));
            }
            else {
                StartCoroutine(move(positions [idx % size], positions [(idx + 1) % size], secondsPerBeat));
            }
        }
    
        private IEnumerator move(Vector3 startPos, Vector3 endPos, float duration) {
            float startTime = Time.time;
            if (rigid && rigidBody != null) {
                while (Time.time <= startTime + duration) {
                    float lerpPercent = Mathf.Clamp01((Time.time - startTime) / duration);
                    if (spherical) {
                        rigidBody.MovePosition(Vector3.Slerp(startPos, endPos, lerpPercent) + startPosition);
                    }
                    else {
                        rigidBody.MovePosition(Vector3.Lerp(startPos, endPos, lerpPercent) + startPosition);
                    }
                    yield return null;
                }
            }
            else if (local) {
                while (Time.time <= startTime + duration) {
                    float lerpPercent = Mathf.Clamp01((Time.time - startTime) / duration);
                    if (spherical) {
                        transform.localPosition = Vector3.Slerp(startPos, endPos, lerpPercent);
                    }
                    else {
                        transform.localPosition = Vector3.Lerp(startPos, endPos, lerpPercent);
                    }
                    transform.localPosition += startPosition;
                    yield return null;
                }
            }
            else {
                while (Time.time <= startTime + duration) {
                    float lerpPercent = Mathf.Clamp01((Time.time - startTime) / duration);
                    if (spherical) {
                        transform.position = Vector3.Slerp(startPos, endPos, lerpPercent);
                    }
                    else {
                        transform.position = Vector3.Lerp(startPos, endPos, lerpPercent);
                    }
                    transform.position += startPosition;
                    yield return null;
                }
            }
        }
    }
}
