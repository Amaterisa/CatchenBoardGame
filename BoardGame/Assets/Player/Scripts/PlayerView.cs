using System.Collections.Generic;
using General.View;
using TMPro;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerView : View3D
    {
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private List<MeshRenderer> eyesRenderer = new List<MeshRenderer>();
        private static readonly int ColorProperty = Shader.PropertyToID("_Color");

        public void SetName(string text)
        {
            nameText.text = text;
        }
        
        public string GetName()
        {
            return nameText.text;
        }

        public void RotateNameToCamera()
        {
            var cameraTransform = Camera.main.transform;
            nameText.transform.forward = cameraTransform.forward;
        }

        public void SetColor(Color color)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                if (!eyesRenderer.Contains(meshRenderer))
                    meshRenderer.material.SetColor(ColorProperty, color);
            }
        }
    }
}
