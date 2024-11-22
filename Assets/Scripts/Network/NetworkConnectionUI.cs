using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class NetworkConnectionUI : MonoBehaviour
    {
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _clientButton;

        private void Awake()
        {
            _hostButton.onClick.AddListener(OnHostButtonPressed);
            _clientButton.onClick.AddListener(OnClientButtonPressed);
        }

        private void OnHostButtonPressed()
        {
            NetworkManager.Singleton.StartHost();
            HideUI();
        }

        private void OnClientButtonPressed()
        {
            NetworkManager.Singleton.StartClient();
            HideUI();
        }

        public void ShowUI()
        {
            gameObject.SetActive(true);
        }

        public void HideUI()
        {
            gameObject.SetActive(false);
        }
    }
}