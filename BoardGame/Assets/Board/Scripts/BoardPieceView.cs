using General.UVCalculator;
using General.View;
using TMPro;
using UnityEngine;

namespace Board.Scripts
{
    public class BoardPieceView : View3D
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private TextMeshPro textMeshPro;
        [SerializeField] private TextMeshPro numberText;

        private void SetTexture(Texture texture)
        {
            meshRenderer.material.mainTexture = texture;
        }

        public void SetText(string text, string number)
        {
            textMeshPro.text = text;
            numberText.text = number;
        }

        private void SetUV(Rect rect)
        {
            var tilling = new Vector2(rect.width, rect.height);
            var offset = new Vector2(rect.x, rect.y);
            meshRenderer.material.mainTextureScale = tilling;
            meshRenderer.material.mainTextureOffset = offset;
        }

        public void Setup(Texture texture)
        {
            var textureSize = new Vector2(texture.width, texture.height);
            var localScale = meshRenderer.transform.lossyScale;
            var newUV = UVCalculator.CalculateNewUV(textureSize, localScale);
            SetTexture(texture);
            SetUV(newUV);
        }
    }
}