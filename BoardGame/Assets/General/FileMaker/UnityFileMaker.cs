using System.IO;
using Board.Scripts;
using UnityEditor;
using UnityEngine;

namespace General.FileMaker
{
    public class UnityFileMaker
    {
        [   MenuItem("BoardPiece/Make Files", false, 100)]
        public static void MakeFiles()
        {
            var paths = Directory.GetFiles(Application.dataPath + "/Resources/Textures/", "*.jpg", SearchOption.TopDirectoryOnly);
            var textures = Resources.LoadAll<Texture2D>("Textures");

            for (var i = 0; i < paths.Length; i++)
            {
                if (paths[i].Contains(Path.DirectorySeparatorChar.ToString()))
                {
                    var splitDirectories = paths[i].Split(Path.DirectorySeparatorChar);
                    var fileName = splitDirectories[splitDirectories.Length - 1];
                    var texture = textures[i];
                    CreateAsset(fileName, texture);
                }
                else if (paths[i].Contains("/"))
                {
                    var splitDirectories = paths[i].Split('/');
                    var fileName = splitDirectories[splitDirectories.Length - 1];
                    var filePath = Path.GetFullPath(paths[i]);
                    var texture = textures[i];
                    CreateAsset(fileName, texture);
                }
            }
        }

        private static void CreateAsset(string fileName, Texture2D texture)
        {
            var unityFile = ScriptableObject.CreateInstance<BoardPieceData>();
            unityFile.DisplayName = fileName.Replace(".jpg", "");
            unityFile.Texture = texture;
            CreateScriptable(unityFile);
        }

        private static void CreateScriptable(BoardPieceData asset)
        {
            AssetDatabase.CreateAsset(asset, $"Assets/Resources/Files/{asset.DisplayName}.asset");
        }
    }
}
