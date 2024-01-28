using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Views
{
    public class UserView : MonoBehaviour, IView
    {
        [SerializeField] private Button backToMenuButton;
        [SerializeField] private Button selectRandomUserButton;
        [SerializeField] private Button loadSpecificUserInfoPopupButton;
        [SerializeField] private TextMeshProUGUI usernameText;
        [SerializeField] private TextMeshProUGUI firstNameText;
        [SerializeField] private TextMeshProUGUI lastNameText;
        [SerializeField] private TextMeshProUGUI ageText;
        [SerializeField] private TextMeshProUGUI idText;

        public delegate void ButtonAction();

        public delegate void OpenPopupAction(MenuPopupType menuPopupType);

        public delegate void ChangeMenuAction(MenuViewType menuPopupType);

        public event ButtonAction OnLoadRandomUserPressed;
        public event OpenPopupAction OnOpenPopupPressed;
        public event ChangeMenuAction OnChangeMenuPressed;

        public void Start()
        {
            selectRandomUserButton.onClick.AddListener(SelectRandomUser);
            selectRandomUserButton.onClick.AddListener(() => AudioManager.Play2DSound(AudioManager.AudioName.Pop));

            loadSpecificUserInfoPopupButton.onClick.AddListener(() =>
                OpenPopupPressed(MenuPopupType.LoadSpecificUserInformation));
            loadSpecificUserInfoPopupButton.onClick.AddListener(() =>
                AudioManager.Play2DSound(AudioManager.AudioName.Pop));

            backToMenuButton.onClick.AddListener(() => ChangeMenuPressed(MenuViewType.MainMenu));
            backToMenuButton.onClick.AddListener(() => AudioManager.Play2DSound(AudioManager.AudioName.Pop));
        }

        private void OnDestroy()
        {
            selectRandomUserButton.onClick.RemoveAllListeners();
            loadSpecificUserInfoPopupButton.onClick.RemoveAllListeners();
            backToMenuButton.onClick.RemoveAllListeners();
        }

        private void ChangeMenuPressed(MenuViewType menuScreen)
        {
            OnChangeMenuPressed?.Invoke(menuScreen);
        }

        private void OpenPopupPressed(MenuPopupType popupType)
        {
            OnOpenPopupPressed?.Invoke(popupType);
        }

        private void SelectRandomUser()
        {
            OnLoadRandomUserPressed?.Invoke();
        }

        public void DisplayUserInfo(UserData userData)
        {
            usernameText.text = $"<color=\"yellow\">User:</color> {userData.Username}";
            firstNameText.text = $"<color=\"yellow\">First Name:</color> {userData.FirstName}";
            lastNameText.text = $"<color=\"yellow\">Last Name:</color> {userData.LastName}";
            ageText.text = $"<color=\"yellow\">Age:</color> {userData.Age}";
            idText.text = $"<color=\"yellow\">Id:</color> {userData.Id}";
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