using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Views;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private MenuView menuView;
        [SerializeField] private UserView userView;
        [SerializeField] private ExitPopupView exitPopupView;
        [SerializeField] private SetUserPopupView setUserPopupView;
        [SerializeField] private UserDataBase userDatabase;

        private readonly List<IPopUp> _popUpViews = new List<IPopUp>();
        private MenuState _menuState;
        private UserData _currentUser;

        void Start()
        {
            _menuState = new MenuState();
            //Initialize Exit Popup
            exitPopupView.OnGoBackRequested += () => exitPopupView.Hide();
            exitPopupView.OnExitRequested += ExitApplication;
            //Initialize Find User Popup
            setUserPopupView.OnGoBackRequested += () => setUserPopupView.Hide();
            setUserPopupView.OnLoadUserRequested += LoadUserOnPopup;
            //Initialize Menu View
            menuView.OnChangeMenuPressed += ChangeMenu;
            menuView.OnOpenPopupPressed += OpenPopup;
            //Initialize UserView
            userView.OnLoadRandomUserPressed += LoadRandomUser;
            userView.OnOpenPopupPressed += OpenPopup;
            userView.OnChangeMenuPressed += ChangeMenu;

            LoadRandomUser();
            ChangeMenu(MenuViewType.MainMenu);
            InitializePopupList();
        }

        private void InitializePopupList()
        {
            _popUpViews.Add(exitPopupView);
            _popUpViews.Add(setUserPopupView);
        }

        private void ChangeMenu(MenuViewType screen)
        {
            _menuState.currentMenuScreen = screen;

            switch (screen)
            {
                case MenuViewType.MainMenu:
                    userView.Hide();
                    menuView.Show();
                    break;
                case MenuViewType.UserMenu:
                    menuView.Hide();
                    userView.Show();
                    break;
            }
        }

        private void OpenPopup(MenuPopupType popupType)
        {
            foreach (var popUp in _popUpViews.Where(x => x.GetPopupType() != popupType))
            {
                popUp.Hide();
            }

            _popUpViews.Single(x => x.GetPopupType() == popupType).Show();
        }

        private void ExitApplication()
        {
            Application.Quit();
        }

        private void LoadRandomUser()
        {
            _currentUser = userDatabase.GetRandomUser();
            DisplayUserOnUserView();
        }

        private void DisplayUserOnUserView()
        {
            userView.DisplayUserInfo(_currentUser);
        }

        private void LoadUserOnPopup(string userId)
        {
            _currentUser = userDatabase.GetUserByID(userId);
            DisplayUserOnUserView();
            setUserPopupView.Hide();
        }
    }
}