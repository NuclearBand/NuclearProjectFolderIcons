using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Nuclear.ProjectFolderIcons
{
    internal static class DefaultIcons
    {
        internal static readonly List<IconSetting> List = new()
        {
            new("Animation", GetDefaultTextureFor("Animation")),
            new("Audio", GetDefaultTextureFor("Audio")),
            new("Editor", GetDefaultTextureFor("Editor")),
            new("Font", GetDefaultTextureFor("Font")),
            new("Material", GetDefaultTextureFor("Material")),
            new("Model", GetDefaultTextureFor("Model")),
            new("Plugin", GetDefaultTextureFor("Plugin")),
            new("Prefab", GetDefaultTextureFor("Prefab")),
            new("Preset", GetDefaultTextureFor("Preset")),
            new("Resource", GetDefaultTextureFor("Resource")),
            new("Scene", GetDefaultTextureFor("Scene")),
            new("Script", GetDefaultTextureFor("Script")),
            new("Setting", GetDefaultTextureFor("Setting")),
            new("Shader", GetDefaultTextureFor("Shader")),
            new("Sprite", GetDefaultTextureFor("Sprite")),
            new("Texture", GetDefaultTextureFor("Texture")),
        };

        private const string PackageManagerIconPath = "Packages/com.nuclearband.projectfoldericons/Editor/Icons";

        private static Texture2D GetDefaultTextureFor(string name) =>
            (Texture2D) AssetDatabase.LoadAssetAtPath(Path.Combine(PackageManagerIconPath, $"{name}.png"),
                typeof(Texture2D));
    }
}