#nullable enable
using System.Collections.Generic;
using UnityEditor;

namespace NuclearBand.UnityProjectFolderIcons
{
    internal static class SettingsManager
    {
        private const string PackageName = "com.nuclearband.unityprojectfoldericons";
        internal const string IconsPath = "IconsList";

        private static UnityEditor.SettingsManagement.Settings? _instance;

        internal static UnityEditor.SettingsManagement.Settings Instance => 
            _instance ??= new UnityEditor.SettingsManagement.Settings(PackageName);

        public static void Save()
        {
            Instance.Save();
        }

        public static List<Settings.IconSetting> GetIconSettings()
        {
            return Instance.Get(IconsPath, SettingsScope.Project, Settings.DefaultIconList);
        }
    }
}
