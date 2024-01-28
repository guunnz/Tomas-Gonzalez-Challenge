using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Views
{
    public class MenuView : MonoBehaviour, IView
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private Button userInfoButton;

        public delegate void OpenPopupAction(MenuPopupType menuPopupType);

        public delegate void ChangeMenuAction(MenuViewType menuPopupType);

        public event ChangeMenuAction OnChangeMenuPressed;
        public event OpenPopupAction OnOpenPopupPressed;

        public void Start()
        {
            exitButton.onClick.AddListener(() => OpenPopupPressed(MenuPopupType.ExitPopup));
            exitButton.onClick.AddListener(() => AudioManager.Play2DSound(AudioManager.AudioName.Pop));
            
            userInfoButton.onClick.AddListener(() => ChangeMenuPressed(MenuViewType.UserMenu));
            userInfoButton.onClick.AddListener(() => AudioManager.Play2DSound(AudioManager.AudioName.Pop));
        }

        private void OnDestroy()
        {
            exitButton.onClick.RemoveAllListeners();
            userInfoButton.onClick.RemoveAllListeners();
        }

        private void ChangeMenuPressed(MenuViewType menuScreen)
        {
            OnChangeMenuPressed?.Invoke(menuScreen);
        }

        private void OpenPopupPressed(MenuPopupType popupType)
        {
            OnOpenPopupPressed?.Invoke(popupType);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}