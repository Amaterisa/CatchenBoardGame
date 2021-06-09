using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Board.Scripts
{
    public class BoardPopulator : MonoBehaviour
    {
        [SerializeField] private Transform contentParent;
        [SerializeField] private int lineWidth = 7;
        [SerializeField] private int lineHeight = 5;
        [SerializeField] private float spacing = 0.1f;
        [SerializeField] private GameObject boardPiece;
        [SerializeField] private int numberOfPieces = 32;
        private List<Transform> boardPieces = new List<Transform>();
        private int index;
        private float pieceSpacing;

        public void PopulateBoard()
        {
            InstantiatePieces();
            index = 0;
            var localPosition = Vector3.zero;
            var localScale = Vector3.one;
            pieceSpacing = localScale.x + spacing;
            boardPieces[index].localPosition = localPosition;

            PositionWithinLine(ref localPosition);

            PositionWithinColumn(ref localPosition);

            PositionWithinLine(ref localPosition, 0, 1, false);

            PositionWithinColumn(ref localPosition, 1, 0, true);

            PositionWithinLine(ref localPosition, 1);

            PositionWithinColumn(ref localPosition, 2, 1);

            PositionWithinLine(ref localPosition, 2, 2, false);

            localScale.z = 2 + spacing;
            boardPieces[index].localScale = localScale;
            localPosition.x -= (localScale.z - 1)/ 2;
            boardPieces[index].localPosition = localPosition;
            boardPieces[index].forward = -transform.right;
            index +=1;
            localScale = new Vector3(1, 1, 3 + spacing * 2);
            localPosition = new Vector3(boardPieces[8].localPosition.x, 0, 3 * pieceSpacing);
            boardPieces[index].localScale = localScale;
            boardPieces[index].localPosition = localPosition;
        }

        private void PositionWithinLine(ref Vector3 localPosition, int linesToRemove = 0, int linesToPosition = 0, bool movingForward = true)
        {
            for (var i = 1; i < lineWidth - linesToRemove; i++)
            {
                index += 1;
                localPosition.z = (movingForward ?  i : lineWidth - linesToPosition - i) * pieceSpacing;
                boardPieces[index].localPosition = localPosition;
                boardPieces[index].forward = movingForward ? transform.forward : -transform.forward;
            }
        }

        private void PositionWithinColumn(ref Vector3 localPosition, int columnsToRemove = 0, int increment = 0, bool movingAscending = false)
        {
            for (var i = 1; i < lineHeight - columnsToRemove; i++)
            {
                index += 1;
                localPosition.x = (movingAscending ? lineHeight - columnsToRemove - i : i + increment) * pieceSpacing;
                boardPieces[index].localPosition = localPosition;
                boardPieces[index].forward = movingAscending ? -transform.right : transform.right;
            }
        }

        private void InstantiatePieces()
        {
            ClearBoardPieces();
            for (var i = 0; i < numberOfPieces; i++)
            {
                var piece = Instantiate(boardPiece, Vector3.zero, Quaternion.identity, contentParent);
                boardPieces.Add(piece.transform);
            }
        }

        private void ClearBoardPieces()
        {
            boardPieces = new List<Transform>();
            var childCount = contentParent.childCount;
            for (var i = childCount - 1; i > 0; i--)
            {
                var child = contentParent.GetChild(i);
                child.SetParent(null);
                DestroyImmediate(child.gameObject);
            }

        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BoardPopulator), true)]
    public class BoardPopulatorEditor : Editor {
        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();
            
            BoardPopulator editorObj = target as BoardPopulator;
            if (editorObj == null)
                return;

            if (GUILayout.Button("Populate board"))
            {
                editorObj.PopulateBoard();
                EditorUtility.SetDirty(editorObj);
            }
        }
    }
#endif
}