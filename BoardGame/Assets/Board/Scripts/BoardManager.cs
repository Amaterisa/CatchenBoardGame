using System;
using System.Collections.Generic;
using Events;
using General.EventManager;
using UnityEngine;

namespace Board.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private List<BoardPieceData> boardPieceDatas = new List<BoardPieceData>();
        [SerializeField] private List<BoardPieceController> boardPieces = new List<BoardPieceController>();
        [SerializeField] private GameObject content;

        private void Awake()
        {
            Hide();
            PopulateBoard();
            EventManager.Register<List<Transform>>(BoardEvents.PositionPlayers, PositionPlayers);
            EventManager.Register(BoardEvents.Show, Show);
            EventManager.Register<int, Action<BoardPieceController>>(BoardEvents.GetBoardPiece, GetBoardPiece);
            EventManager.Register<int, Action<BoardPieceData>>(BoardEvents.GetBoardPieceData, GetBoardPieceData);
        }

        private void OnDestroy()
        {
            EventManager.Unregister<List<Transform>>(BoardEvents.PositionPlayers, PositionPlayers);
            EventManager.Unregister(BoardEvents.Show, Show);
            EventManager.Unregister<int, Action<BoardPieceController>>(BoardEvents.GetBoardPiece, GetBoardPiece);
            EventManager.Unregister<int, Action<BoardPieceData>>(BoardEvents.GetBoardPieceData, GetBoardPieceData);
        }

        private void Show()
        {
            content.SetActive(true);
        }

        private void Hide()
        {
            content.SetActive(false);
        }

        private void PopulateBoard()
        {
            for (var i = 0; i < boardPieces.Count; i++){
                var piece = boardPieces[i];
                piece.SetBoardPieceData(boardPieceDatas[i]);
                piece.Populate();
            }
        }
        
        private void PositionPlayers(List<Transform> players)
        {
            var firstPiecePosition = boardPieces[0].transform.position;
            foreach (var player in players)
            {
                player.position = firstPiecePosition;
                player.forward = -boardPieces[0].transform.forward;
            }
        }

        private void GetBoardPiece(int number, Action<BoardPieceController> action)
        {
            var index = Mathf.Clamp(number, 0, boardPieces.Count - 1);
            action?.Invoke(boardPieces[index]);
        }
        
        private void GetBoardPieceData(int number, Action<BoardPieceData> action)
        {
            var index = Mathf.Clamp(number, 0, boardPieceDatas.Count - 1);
            action?.Invoke(boardPieceDatas[index]);
        }
    }
}
