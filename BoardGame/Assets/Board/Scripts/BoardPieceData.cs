using System;
using UnityEngine;

namespace Board.Scripts
{
    [Serializable]
    public class BoardPieceData : ScriptableObject
    {
        [SerializeField] private string displayName;
        [SerializeField] private bool canPlayOnNextRound = true;
        [SerializeField] private int spacesToMove = 0;
        [SerializeField] private Texture texture;
        [SerializeField] private string description;

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public bool CanPlayOnNextRound
        {
            get => canPlayOnNextRound;
            set => canPlayOnNextRound = value;
        }

        public int SpacesToMove
        {
            get => spacesToMove;
            set => spacesToMove = value;
        }

        public Texture Texture 
        { 
            get => texture;
            set => texture = value; 
        }

        public string Description 
        { 
            get => description;
            set => description = value; 
        }
    }
}
