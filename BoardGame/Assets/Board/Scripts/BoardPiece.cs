using UnityEngine;

namespace Board.Scripts
{
    public class BoardPiece: MonoBehaviour
    {
        [SerializeField] private BoardPieceView view;
        private BoardPieceData data;

        public void SetBoardPieceData(BoardPieceData boardPiece) 
        {
            data = boardPiece;
        }
    
        public void Populate()
        {
            view.SetTexture(data.Texture);
        }
    }
}
