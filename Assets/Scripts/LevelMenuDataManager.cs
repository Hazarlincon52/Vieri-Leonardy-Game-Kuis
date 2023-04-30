using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private UI_LevelPackList _levelPackList = null;
  
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

	[Space, SerializeField]
	private LevelPackKuis[] _levelPack = new LevelPackKuis[0];

	// Start is called before the first frame update
	void Start()
    {
		if (!_playerProgress.MuatProgres())
		{
			_playerProgress.SimpanProgress();
		}
		
		_levelPackList.LoadLevelPack(_levelPack, _playerProgress.progressData);

		_tempatKoin.text = $"{_playerProgress.progressData.koin}";

		AudioManager.instance.PlayBGM(0);
	}

   
}
