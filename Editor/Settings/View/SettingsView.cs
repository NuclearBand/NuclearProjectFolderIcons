#nullable enable
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nuclear.ProjectFolderIcons
{
    internal class SettingsView
    {
        private const string RelativeWindowPath = "Editor/UXML/SettingsWindow.uxml";
        private const string RelativeRowTemplatePath = "Editor/UXML/RowTemplate.uxml";
        private const string RelativeDefaultIconPath = "Editor/Icons/Default/Default.png";

        private readonly VisualTreeAsset _rowTemplateVisualTree;

        private readonly VisualElement _rowsContainer;

        internal static Texture2D DefaultFolderTexture { get; private set; } = null!;

        private readonly List<IconSetting> _iconSettings;
        
        internal static void Create(string _, VisualElement root)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    $"Packages/{SettingsManager.PackageName}/{RelativeWindowPath}");
            if (visualTree == null)
                visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"Assets/{RelativeWindowPath}");
            visualTree.CloneTree(root);
            
            DefaultFolderTexture = AssetDatabase.LoadAssetAtPath<Texture2D>($"Packages/{SettingsManager.PackageName}/{RelativeDefaultIconPath}");
            if (DefaultFolderTexture == null)
                DefaultFolderTexture = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/{RelativeDefaultIconPath}");

            var __ = new SettingsView(root);
        }

        private SettingsView(VisualElement root)
        {
            _rowsContainer = root.Q<VisualElement>("Rows");
            _rowTemplateVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                $"Packages/{SettingsManager.PackageName}/{RelativeRowTemplatePath}");
            if (_rowTemplateVisualTree == null)
                _rowTemplateVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"Assets/{RelativeRowTemplatePath}");
            
            var newRow = root.Q<VisualElement>("NewRow");
            var _ = new NewIconSettingView(newRow, OnAddButtonClickCallback);

            _iconSettings = SettingsManager.GetIconSettings();
            
            foreach (var iconSetting in _iconSettings) 
                AddIconSettingView(iconSetting);
        }

        private void OnRemoveButtonClickCallback(IconSettingView iconSettingView, IconSetting iconSetting)
        {
            _rowsContainer.Remove(iconSettingView.VisualElement);
            _iconSettings.Remove(iconSetting);
            SettingsManager.Save();
        }

        private void OnAddButtonClickCallback(IconSetting iconSetting)
        {
            AddIconSettingView(iconSetting);
            _iconSettings.Add(iconSetting);
            SettingsManager.Save();
        }

        private void AddIconSettingView(IconSetting iconSetting)
        {
            var rowVisualElement = _rowTemplateVisualTree.Instantiate();
            _rowsContainer.Add(rowVisualElement);

            var _ = new IconSettingView(rowVisualElement, iconSetting, SettingsManager.Save, OnRemoveButtonClickCallback);
        }
    }
}