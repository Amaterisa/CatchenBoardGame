using General.View;
using TMPro;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerView : View3D
    {
        [SerializeField] private TextMeshPro nameText;
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        public void SetName(string text)
        {
            nameText.text = text;
        }

        public void RotateNameToCamera()
        {
            var cameraTransform = Camera.main.transform;
            //var projectedForward = Vector3.Project(cameraTransform.forward, Vector3.up);
            nameText.transform.forward = cameraTransform.forward;
        }

        public void SetColor(Color color)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.SetColor(Color1, color);
            }
        }
    }
}
