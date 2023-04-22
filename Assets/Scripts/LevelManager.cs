using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField] 
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private Ui_Pertanyaan _tempatPertanyaan = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private UI_PoinJawaban[] _tempatPilihanJawaban = new UI_PoinJawaban[0];

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _gameplayScene = string.Empty;

    private int _indexSoal = -1;


    private void Start()
    {
        
        if (!_playerProgress.MuatProgres())
        {
            _playerProgress.SimpanProgress();
        }
        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.soalIndexKe - 1;
        
        //_playerProgress.SimpanProgress();
        NextLevel();

        //subscribe event
        UI_PoinJawaban.EventJawabSoal += UI_PointJawaban_EventJawabaSoal;
    }

    private void OnDestroy()
    {
        //Unsubscribe event
        UI_PoinJawaban.EventJawabSoal -= UI_PointJawaban_EventJawabaSoal;
    }

    private void OnApplicationQuit()
    {
        _inisialData.SaatKalah = false;    
    }

    private void UI_PointJawaban_EventJawabaSoal(string jawaban, bool adalahBenar)
    {

        if (adalahBenar)
        {
            _playerProgress.progressData.koin += 20;
        }
    }

    public void NextLevel()
    {
        //Soal index selanjutnya
        _indexSoal++;
        
        //Jika index melampaui soal terakhir ulang dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            _gameSceneManager.BukaScene(_gameplayScene);
        }

        else
        {
            //Ambil data Pertanyaan
            LevelSoalKuis soal = _soalSoal.AmbiLevelKe(_indexSoal);

            //Set informasi soal

            _tempatPertanyaan.SetPertanyaan(soal.pertanyaan, $"Level {_indexSoal + 1}", soal.hint);

            for (int i = 0; i < _tempatPilihanJawaban.Length; i++)
            {
                UI_PoinJawaban point = _tempatPilihanJawaban[i];
                LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
                point.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
            }
        }
       
    }
}
