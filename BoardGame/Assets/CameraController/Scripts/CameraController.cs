using System;
using Events;
using General.EventManager;
using UnityEngine;

namespace CameraController.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 offset = new Vector3(0, 0.4f, 0.4f);
        [SerializeField] private float lerpFactor = 0.75f;
        private Transform cameraTransform;
        private Transform referenceTransform;
        private bool followTransform;

        private void Awake()
        {
            EventManager.Register<Transform>(CameraEvents.SetReferenceTransform, SetReferenceTransform);
            EventManager.Register<bool>(CameraEvents.EnableFollowTransform, EnableFollowTransform);
        }

        private void OnDestroy()
        {
            EventManager.Unregister<Transform>(CameraEvents.SetReferenceTransform, SetReferenceTransform);
            EventManager.Unregister<bool>(CameraEvents.EnableFollowTransform, EnableFollowTransform);
        }

        private void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            if (followTransform && referenceTransform != default)
            {
                SetPositionAndRotation();
            }
        }

        private void SetPositionAndRotation()
        {
            var position = referenceTransform.position + offset.y * Vector3.up + offset.z * referenceTransform.forward;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, position, lerpFactor * Time.deltaTime);
            cameraTransform.LookAt(referenceTransform.position);
        }

        private void SetReferenceTransform(Transform reference)
        {
            referenceTransform = reference;
        }

        private void EnableFollowTransform(bool enable)
        {
            followTransform = enable;
        }
    }
}
