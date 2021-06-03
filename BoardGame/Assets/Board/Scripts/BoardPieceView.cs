using General.View;
using TMPro;
using UnityEngine;

namespace Board.Scripts
{
    public class BoardPieceView : View3D
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private TextMeshPro textMeshPro;

        public void SetTexture(Texture texture)
        {
            meshRenderer.material.mainTexture = texture;
        }

        public void SetText(string text)
        {
            textMeshPro.text = text;
        }
    }
}