using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class SetUserPopupView : MonoBehaviour, IPopUp
    {
        public TMP_InputField userIdInputField;
        public Button loadUserButton;
        public Button[] goBackButtons;

        public event Action<string> OnLoadUserRequested;
        public event Action OnGoBackRequested;
        
        private readonly MenuPopupType _popupType = MenuPopupType.LoadSpecificUserInformation;
        void Awake()
        {
            loadUserButton.onClick.AddListener(() => OnLoadUserRequested?.Invoke(GetUserId()));
            loadUserButton.onClick.AddListener(() => AudioManager.Play2DSound(AudioManager.AudioName.Pop));
            foreach (var goBackButton in goBackButtons)
            {
                goBackButton.onClick.AddListener(() => OnGoBackRequested?.Invoke());
                goBackButton.onClick.AddListener(() => AudioManager.Play2DSound(AudioManager.AudioName.Pop));
            }
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public string GetUserId() => userIdInputField.text;
        
        public MenuPopupType GetPopupType()
        {
            return _popupType;
        }
    }
}