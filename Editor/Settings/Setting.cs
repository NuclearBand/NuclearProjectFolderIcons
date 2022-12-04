using UnityEditor;
using UnityEditor.SettingsManagement;

namespace NuclearBand.UnityProjectFolderIcons
{
    // Usually you will only have a single Settings instance, so it is convenient to define a UserSetting<T> implementation
    // that points to your instance. In this way you avoid having to pass the Settings parameter in setting field definitions.
    internal class Setting<T> : UserSetting<T>
    {
        public Setting(string key, T value, SettingsScope scope = SettingsScope.Project)
            : base(SettingsManager.Instance, key, value, scope)
        {}

        private Setting(UnityEditor.SettingsManagement.Settings settings, string key, T value, SettingsScope scope = SettingsScope.Project)
            : base(settings, key, value, scope) { }
    }
}
