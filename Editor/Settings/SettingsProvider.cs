#nullable enable
using UnityEditor;
using UnityEditor.SettingsManagement;

namespace NuclearBand.UnityProjectFolderIcons
{
    internal static class SettingsProvider
    {
        private const string PreferencesPath = "Preferences/Project folder icons";

        [SettingsProvider]
        private static UnityEditor.SettingsProvider CreateSettingsProvider()
        {
            var provider = new UserSettingsProvider(PreferencesPath,
                SettingsManager.Instance,
                new[] {typeof(SettingsProvider).Assembly});

            return provider;
        }
    }
}