using General.View;
using UnityEngine;

namespace Board.Scripts
{
    public class BoardPieceView : View3D
    {
        [SerializeField] private MeshRenderer meshRenderer;

        public void SetTexture(Texture texture)
        {
            meshRenderer.material.mainTexture = texture;
        }
    }
}