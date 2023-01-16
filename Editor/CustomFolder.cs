#nullable enable
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Nuclear.ProjectFolderIcons
{
    [InitializeOnLoad]
    public class CustomFolder
    {
        static CustomFolder() => 
            EditorApplication.projectWindowItemOnGUI += DrawFolderIcon;

        private static Texture? FindTextureForDirectory(string name) =>
            SettingsManager.GetIconSettings().Where(iconSetting => name.Contains(iconSetting.Name))
                .Select(iconSetting => iconSetting.Texture).FirstOrDefault();

        private static void DrawFolderIcon(string guid, Rect rect)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);

            if (path == "" ||
                Event.current.type != EventType.Repaint ||
                !File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                return;
            }

            Rect imageRect;

            if (rect.height > 20)
            {
                imageRect = new(rect.x - 1, rect.y - 1, rect.width + 2, rect.width + 2);
            }
            else if (rect.x > 20)
            {
                imageRect = new(rect.x - 1, rect.y - 1, rect.height + 2, rect.height + 2);
            }
            else
            {
                imageRect = new(rect.x + 2, rect.y - 1, rect.height + 2, rect.height + 2);
            }

            var texture = FindTextureForDirectory(Path.GetFileName(path));
            if (texture == null)
            {
                return;
            }

            GUI.DrawTexture(imageRect, texture);
        }
    }
}