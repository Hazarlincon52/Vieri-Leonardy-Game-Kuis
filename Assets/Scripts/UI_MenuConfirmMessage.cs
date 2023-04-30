using TMPro;
using UnityEngine;

public class UI_MenuConfirmMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;
    
    [SerializeField]
    private GameObject _pesanCukupKoin = null;
    
    [SerializeField]
    private GameObject _pesanTakCukupKoin = null;

    private UI_OpsiLevelPack _tombolLevelPack = null;
    private LevelPackKuis _levelPack = null;
	// Start is called before the first frame update
	void Start()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        //Subscribe events
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

	private void OnDestroy()
	{
		//Unsubscribe events
		UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
	}

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack,LevelPackKuis levelPack, bool terkunci)
    {
        if (!terkunci)
        {
            return;
        }

        gameObject.SetActive(true);

        if (_playerProgress.progressData.koin < levelPack.Harga)
        {
            //Jika tidak cukup
            _pesanCukupKoin.SetActive(false);
            _pesanTakCukupKoin.SetActive(true);
            return;
        }

        //Jika cukup
        _pesanTakCukupKoin.SetActive(false);
        _pesanCukupKoin.SetActive(true);


	    _tombolLevelPack = tombolLevelPack;
        _levelPack = levelPack;

    }

    public void BukaLevel()
    {
        _playerProgress.progressData.koin -=_levelPack.Harga;
        _playerProgress.progressData.progresLevel[_levelPack.name] = 1;

        _tempatKoin.text = $"{_playerProgress.progressData.koin}";
        _playerProgress.SimpanProgress();

        _tombolLevelPack.BukaLevelPack();
    }
}
