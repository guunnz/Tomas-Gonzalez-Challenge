using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class ExitPopupView : MonoBehaviour, IView, IPopUp
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private Button[] goBackButtons;

        public event Action OnExitRequested;
        public event Action OnGoBackRequested;
        private readonly MenuPopupType _popupType = MenuPopupType.ExitPopup;

        void Awake()
        {
            exitButton.onClick.AddListener(Exit);
            foreach (var goBackButton in goBackButtons)
            {
                goBackButton.onClick.AddListener(() => OnGoBackRequested?.Invoke());
                goBackButton.onClick.AddListener(() => AudioManager.Play2DSound(AudioManager.AudioName.Pop));
            }
        }

        private void Exit()
        {
            OnExitRequested?.Invoke();
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public MenuPopupType GetPopupType()
        {
            return _popupType;
        }
    }
}