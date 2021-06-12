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
        [SerializeField] private float angleToRotate = 45f;
        private Transform cameraTransform;
        private Transform referenceTransform;
        private bool followTransform;

        private void Awake()
        {
            EventManager.Register<Transform>(CameraEvents.SetReferenceTransform, SetReferenceTransform);
            EventManager.Register<bool>(CameraEvents.EnableFollowTransform, EnableFollowTransform);
            EventManager.Register<float>(CameraEvents.SetLerpFactor, SetLerpFactor);
        }

        private void OnDestroy()
        {
            EventManager.Unregister<Transform>(CameraEvents.SetReferenceTransform, SetReferenceTransform);
            EventManager.Unregister<bool>(CameraEvents.EnableFollowTransform, EnableFollowTransform);
            EventManager.Unregister<float>(CameraEvents.SetLerpFactor, SetLerpFactor);
        }

        private void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            if (followTransform && referenceTransform != default)
            {
                SetPosition();
                SetRotation();
            }
        }

        private void SetPosition()
        {
            var position = referenceTransform.position + offset.y * Vector3.up - offset.z * Vector3.forward;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, position, lerpFactor * Time.deltaTime);
        }

        private void SetRotation()
        {
            var eulerAngles = cameraTransform.eulerAngles;
            eulerAngles.x = angleToRotate;
            cameraTransform.eulerAngles = eulerAngles;
        }

        private void SetReferenceTransform(Transform reference)
        {
            referenceTransform = reference;
        }

        private void EnableFollowTransform(bool enable)
        {
            followTransform = enable;
        }

        private void SetLerpFactor(float factor)
        {
            lerpFactor = factor;
        }
    }
}
