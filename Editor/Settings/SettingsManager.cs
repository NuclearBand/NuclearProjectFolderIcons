#nullable enable
using System.Collections.Generic;

namespace Nuclear.ProjectFolderIcons
{
    internal static class SettingsManager
    {
        public const string PackageName = "com.nuclearband.projectfoldericons";
        private const string IconsPath = "IconsList";

        private static UnityEditor.SettingsManagement.Settings? _instance;

        private static List<IconSetting>? _iconsList;

        internal static UnityEditor.SettingsManagement.Settings Instance => 
            _instance ??= new(PackageName);

        public static void Save()
        {
            Instance.Set(IconsPath, _iconsList);
            Instance.Save();
        }

        public static List<IconSetting> GetIconSettings()
        {
            if (_iconsList != null)
                return _iconsList;
            
            _iconsList = Instance.Get<List<IconSetting>>(IconsPath);
            if (_iconsList == null)
            {
                _iconsList = DefaultIcons.List;
                Save();
            }
            return _iconsList;
        }
    }
}
