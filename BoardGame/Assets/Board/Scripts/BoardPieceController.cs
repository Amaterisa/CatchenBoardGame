using System;
using System.Collections.Generic;
using Events;
using General.Consts;
using General.EventManager;
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
            AddCallback();
        }
    
        public void Populate()
        {
            view.Setup(data.Texture);
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

        private void AddCallback()
        {
            if (data.Equals(7) || data.Equals(15) || data.Equals(24))
            {
                Action<int> action = count => GoToNextPlayer();
                data.Callback = () =>
                {
                    RollDice(action);
                    EventManager.Trigger(TurnEvents.SetText, Consts.Drink);
                };
            }
            else if (data.Equals(12))
            {
                data.Callback = MoveOnePiece;
            } 
            else if (data.Equals(22))
            {
                data.Callback = () => RollDice(GoBackToTheBeginning);
            } 
            else if (data.Equals(25))
            {
                data.Callback = () =>
                {
                    RollDice(WalkByBoard);
                    EventManager.Trigger(TurnEvents.SetText, Consts.GoBack);
                };
            }
        }

        private void RollDice(Action<int> action)
        {
            EventManager.Trigger(DiceEvents.SetRollDiceCallback, action);
            EventManager.Trigger(DiceEvents.Show);
        }

        private void MoveOnePiece()
        {
            EventManager.Trigger(PlayerManagerEvents.StartMoveByConsequence, -1);
        }

        private void GoToNextPlayer()
        {
            EventManager.Trigger(DiceEvents.Hide);
            EventManager.Trigger(PlayerManagerEvents.GoToNextPlayer);
        }

        private void WalkByBoard(int count)
        {
            EventManager.Trigger(DiceEvents.Hide);
            EventManager.Trigger(PlayerManagerEvents.StartMoveByConsequence, -count);
        }

        private void GoBackToTheBeginning(int count)
        {
            EventManager.Trigger(DiceEvents.Hide);
            if (count == 6)
                EventManager.Trigger(PlayerManagerEvents.StartMoveByConsequence, -int.Parse(data.DisplayName));
            else
                GoToNextPlayer();
        }
    }
}
