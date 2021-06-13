using System.Collections.Generic;
using Player.Scripts;
using UnityEngine;

namespace Board.Scripts
{
    public class BoardPieceController: MonoBehaviour
    {
        [SerializeField] private BoardPieceView view;
        [SerializeField] private List<Transform> points;
        private Dictionary<Transform, Transform> occupiedPositions = new Dictionary<Transform, Transform>();
        private BoardPieceData data;

        public void SetBoardPieceData(BoardPieceData boardPiece) 
        {
            data = boardPiece;
        }
    
        public void Populate()
        {
            view.SetTexture(data.Texture);
            view.SetText(data.Description, data.DisplayName);
        }

        public void AddPlayer(Transform player)
        {
            if (!occupiedPositions.ContainsKey(player))
            {
                foreach (var point in points)
                {
                    if (!occupiedPositions.ContainsValue(point))
                    {
                        occupiedPositions[player] = point;
                        return;
                    }
                }
            }
        }

        public void RemovePlayer(Transform player)
        {
            if (occupiedPositions.ContainsKey(player))
                occupiedPositions.Remove(player);
        }

        public Transform GetPoint(Transform player)
        {
            return occupiedPositions.ContainsKey(player) ? occupiedPositions[player] : null;
        }
    }
}
