#nullable enable
using UnityEditor;
using UnityEditor.SettingsManagement;

namespace Nuclear.ProjectFolderIcons
{
    internal static class SettingsProvider
    {
        private const string PreferencesPath = "Preferences/Nuclear Project Folder Icons";

        [SettingsProvider]
        private static UnityEditor.SettingsProvider CreateSettingsProvider()
        {
            var provider = new UserSettingsProvider(PreferencesPath, 
                SettingsManager.Instance,
                new [] {typeof(SettingsProvider).Assembly})
            {
                activateHandler = SettingsView.Create
            };

            return provider;
        }
    }
}