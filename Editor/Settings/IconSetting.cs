#nullable enable
using System;
using UnityEngine;

namespace Nuclear.ProjectFolderIcons
{
    [Serializable]
    internal class IconSetting
    {
        public string Name;
        public Texture2D Texture;

        public IconSetting(string name, Texture2D texture)
        {
            Name = name;
            Texture = texture;
        }
    }
}