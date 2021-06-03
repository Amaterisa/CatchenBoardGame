using UnityEngine;

namespace Board.Scripts
{
    public class BoardPieceController: MonoBehaviour
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
            view.SetText(data.Description);
        }
    }
}
