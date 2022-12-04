#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEngine;

namespace NuclearBand.UnityProjectFolderIcons
{
    internal class Settings : EditorWindow
    {
        private const string PackageManagerIconPath = "Packages/com.nuclearband.unityprojectfoldericons/Editor/Icons";
        
        [Serializable]
        public class IconSetting
        {
            public string Name = string.Empty;
            public Texture2D Texture = null!;
        }

        internal static readonly List<Settings.IconSetting> DefaultIconList = new List<Settings.IconSetting>()
        {
            new() {Name = "Animation", Texture = GetDefaultTextureFor("Animation")},
            new() {Name = "Audio", Texture = GetDefaultTextureFor("Audio")},
            new() {Name = "Editor", Texture = GetDefaultTextureFor("Editor")},
            new() {Name = "Font", Texture = GetDefaultTextureFor("Font")},
            new() {Name = "Material", Texture = GetDefaultTextureFor("Material")},
            new() {Name = "Model", Texture = GetDefaultTextureFor("Model")},
            new() {Name = "Plugin", Texture = GetDefaultTextureFor("Plugin")},
            new() {Name = "Prefab", Texture = GetDefaultTextureFor("Prefab")},
            new() {Name = "Preset", Texture = GetDefaultTextureFor("Preset")},
            new() {Name = "Resource", Texture = GetDefaultTextureFor("Resource")},
            new() {Name = "Scene", Texture = GetDefaultTextureFor("Scene")},
            new() {Name = "Script", Texture = GetDefaultTextureFor("Script")},
            new() {Name = "Setting", Texture = GetDefaultTextureFor("Setting")},
            new() {Name = "Shader", Texture = GetDefaultTextureFor("Shader")},
            new() {Name = "Sprite", Texture = GetDefaultTextureFor("Sprite")},
            new() {Name = "Texture", Texture = GetDefaultTextureFor("Texture")},
        };

        [UserSetting]
        private static readonly Setting<List<IconSetting>> Icons =
            new (SettingsManager.IconsPath, DefaultIconList);

        private static string _iconRegEx = string.Empty;
        private static Texture2D? _texture;
        
        [UserSettingBlock("Icons settings")]
        static void ConditionalValueGUI(string searchContext)
        {
            EditorGUI.BeginChangeCheck();

            var list = Icons.value;

            for (var i = 0; i < list.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"{i + 1} Name", GUILayout.Width(75));
                list[i].Name = EditorGUILayout.TextField(list[i].Name, GUILayout.MinWidth(200));
                list[i].Texture = (Texture2D)EditorGUILayout.ObjectField("Texture", list[i].Texture, typeof(Texture2D), false);
                if (GUILayout.Button("Remove"))
                {
                    list.RemoveAt(i);
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("New name", GUILayout.Width(75));
            _iconRegEx = EditorGUILayout.TextField(_iconRegEx,  GUILayout.MinWidth(200));
            _texture = (Texture2D)EditorGUILayout.ObjectField("New texture", _texture, typeof(Texture2D), false);
            if (GUILayout.Button("Add"))
            {
                list.Add(new IconSetting {Name = _iconRegEx, Texture = _texture});
                _iconRegEx = string.Empty;
                _texture = null;
            }
            EditorGUILayout.EndHorizontal();
            
            if (EditorGUI.EndChangeCheck())
                Icons.ApplyModifiedProperties();

            SettingsGUILayout.DoResetContextMenuForLastRect(Icons);
            if (EditorGUI.EndChangeCheck())
                SettingsManager.Save();
        }

        private static Texture2D GetDefaultTextureFor(string name)
        {
            return (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(PackageManagerIconPath, $"{name}.png"),
                typeof(Texture2D));
        }
    }
}