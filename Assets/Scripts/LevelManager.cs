using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] 
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private Ui_Pertanyaan _tempatPertanyaan = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private UI_PoinJawaban[] _tempatPilihanJawaban = new UI_PoinJawaban[0];


    

    private int _indexSoal = -1;


    private void Start()
    {
        
        if (!_playerProgress.MuatProgres())
        {
            _playerProgress.SimpanProgress();
        }
        
        //_playerProgress.SimpanProgress();
        NextLevel();
    }
    public void NextLevel()
    {
        //Soal index selanjutnya
        _indexSoal++;

        //Jika index melampaui soal terakhir ulang dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            _indexSoal = 0;
        }

        //Ambil data Pertanyaan
        LevelSoalKuis soal = _soalSoal.AmbiLevelKe(_indexSoal);

        //Set informasi soal

        _tempatPertanyaan.SetPertanyaan(soal.pertanyaan,$"Level {_indexSoal + 1}", soal.hint);

        for (int i=0; i<_tempatPilihanJawaban.Length; i++)
        {
            UI_PoinJawaban point = _tempatPilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            point.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
    }
}
