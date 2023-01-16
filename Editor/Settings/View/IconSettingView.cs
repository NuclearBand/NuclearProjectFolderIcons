#nullable enable
using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Nuclear.ProjectFolderIcons
{
    internal class IconSettingView
    {
        private readonly VisualElement _icon;
        private readonly TextField _titleLabel;
        private readonly ObjectField _textureField;

        private readonly IconSetting _iconSetting;
        private readonly Action<IconSettingView, IconSetting> _removeCallback;
        private readonly Action _saveCallback;
        
        public VisualElement VisualElement { get; }

        internal IconSettingView(VisualElement visualElement, IconSetting iconSetting, 
            Action saveCallback,
            Action<IconSettingView, IconSetting> removeCallback)
        {
            VisualElement = visualElement;
            
            _saveCallback = saveCallback;
            _removeCallback = removeCallback;

            _icon = VisualElement.Q<VisualElement>("Icon");
            
            _titleLabel = VisualElement.Q<TextField>("PrefabNameField");
            _titleLabel.RegisterValueChangedCallback(OnTitleLabelChangedCallback);
            
            _textureField = VisualElement.Q<ObjectField>("IconField");
            _textureField.RegisterValueChangedCallback(OnTextureFieldChangedCallback);
            
            var removeButton = VisualElement.Q<Button>("RemoveButton");
            removeButton.clickable.clicked += OnRemoveButtonClickedCallback;

            _iconSetting = iconSetting;
            Update();
        }

        private void OnTitleLabelChangedCallback(ChangeEvent<string> evt)
        {
            _iconSetting.Name = evt.newValue;
            _saveCallback.Invoke();
        }

        private void OnTextureFieldChangedCallback(ChangeEvent<Object> evt)
        {
            _iconSetting.Texture = (Texture2D) evt.newValue;
            _icon.style.backgroundImage = _iconSetting.Texture != null ? 
                new(_iconSetting.Texture) : 
                new(SettingsView.DefaultFolderTexture);
            _saveCallback.Invoke();
        }

        private void OnRemoveButtonClickedCallback() => _removeCallback.Invoke(this, _iconSetting);

        private void Update()
        {
            _icon.style.backgroundImage = _iconSetting.Texture != null ? 
                new(_iconSetting.Texture) : 
                new(SettingsView.DefaultFolderTexture);
            _titleLabel.value = _iconSetting.Name;
            _textureField.value = _iconSetting.Texture;
        }
    }
}