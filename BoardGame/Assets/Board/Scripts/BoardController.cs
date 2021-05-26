using System.Collections.Generic;
using UnityEngine;

namespace Board.Scripts
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private BoardPieceView pieceView;
        [SerializeField] private List<BoardPieceData> boardPieceDatas = new List<BoardPieceData>();
        [SerializeField] private List<BoardPiece> boardPieces = new List<BoardPiece>();

        private void Start()
        {
            PopulateBoard();
        }

        private void PopulateBoard()
        {
            for (var i = 0; i < boardPieces.Count; i++){
                var piece = boardPieces[i];
                piece.SetBoardPieceData(boardPieceDatas[i]);
                piece.Populate();
            }
        }

        private void ShowCurrentBoardImage()
        {
            pieceView.Show();
        }

        private void HideCurrentBoardImage()
        {
            pieceView.Hide();
        }
    }
}
