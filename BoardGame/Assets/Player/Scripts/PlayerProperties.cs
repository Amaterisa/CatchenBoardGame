using UnityEngine;

namespace Player.Scripts
{
    public class PlayerProperties: MonoBehaviour
    {
        private int piece = 0;
        private bool canPlay = true;

        public int Piece
        {
            get => piece;
            set => piece = value;
        }

        public bool CanPlay
        {
            get => canPlay;
            set => canPlay = value;
        }
    }
}