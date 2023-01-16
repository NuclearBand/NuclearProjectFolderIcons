#nullable enable
using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Nuclear.ProjectFolderIcons
{
    internal class NewIconSettingView
    {
        private readonly VisualElement _icon;
        private readonly TextField _titleLabel;
        private readonly ObjectField _textureField;
        
        private readonly Action<IconSetting> _addCallback;

        private IconSetting _iconSetting;

        public NewIconSettingView(VisualElement visualElement, Action<IconSetting> addCallback)
        {
            _addCallback = addCallback;
            
            _icon = visualElement.Q<VisualElement>("Icon");

            _titleLabel = visualElement.Q<TextField>("PrefabNameField");
            _titleLabel.RegisterValueChangedCallback(OnTitleLabelChangedCallback);

            _textureField = visualElement.Q<ObjectField>("IconField");
            _textureField.RegisterValueChangedCallback(OnTextureFieldChangedCallback);
            
            var addButton = visualElement.Q<Button>("AddButton");
            addButton.clickable.clicked += OnAddButtonClickedCallback;

            _iconSetting = new("", null!);

            Update();
        }

        private void OnAddButtonClickedCallback()
        {
            _addCallback.Invoke(_iconSetting);
            
            _iconSetting = new("", null!);
            Update();
        }

        private void OnTextureFieldChangedCallback(ChangeEvent<Object> evt)
        {
            _iconSetting.Texture = (Texture2D)evt.newValue;
            _icon.style.backgroundImage = _iconSetting.Texture != null ? 
                new(_iconSetting.Texture) : 
                new(SettingsView.DefaultFolderTexture);
        }

        private void OnTitleLabelChangedCallback(ChangeEvent<string> evt) => _iconSetting.Name = evt.newValue;

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