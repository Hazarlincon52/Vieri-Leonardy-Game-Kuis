using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelKuisList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private UI_OpsiLevelKuis _tombolLevel = null;

    [SerializeField]
    private RectTransform _content = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _gameplayScene = string.Empty;

    private void Start()
    {
        UnloadLevelPack(_levelPack);
      
        //Subscribe events 
        UI_OpsiLevelKuis.EventSaatKlik += UI_OpsiLevelKuis_EventSaatKlik;
    }
    private void OnDestroy()
    {
        //Unsubscribe events
        UI_OpsiLevelKuis.EventSaatKlik -= UI_OpsiLevelKuis_EventSaatKlik; 
    }
    private void UI_OpsiLevelKuis_EventSaatKlik(int index)
    {
        _inisialData.soalIndexKe = index;

        //TODO: Kirim data ke Gameplay
        _gameSceneManager.BukaScene(_gameplayScene);
    }

    //Membuka, memuat, dan menampilkan level dari isi Level Pack
    public void UnloadLevelPack(LevelPackKuis levelPack)
    {
        HapusIsiKonten();

        var levelTerbukaTerakhir = _playerProgress.progressData.progresLevel[levelPack.name] - 1;


		for (int i=0; i<levelPack.BanyakLevel; i++)  
        {
            //Memuat salianan objek dari prefab tombol level
            var t = Instantiate(_tombolLevel); 

            t.SetLevelKuis(levelPack.AmbiLevelKe(i),i);

            //Masukkan objek tombol sebagai anak dari objek "content"
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;

            if (i > levelTerbukaTerakhir)
            {
                t.InteraksiTombol = false;
            }
        }
    }

    private void HapusIsiKonten()
    {
        var cc = _content.childCount;

        for (int i=0; i<cc; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }

    
}
