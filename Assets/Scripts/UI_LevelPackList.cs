using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack = null;

    [SerializeField]
    private RectTransform _content = null;

    [SerializeField]
    private UI_LevelKuisList _levelList = null;

    
    private void Start()
    {
        //LoadLevelPack();
        if (_inisialData.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik(null, _inisialData.levelPack, false);
        }

        //Subscribe events
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }
    private void OnDestroy()
    {
        //Unsubscribe events
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    //methode untuk memuat  semua level pack sebelum ditampilkan 
    public void LoadLevelPack(LevelPackKuis[] levelPack, PlayerProgress.MainData playerData)
    {
        foreach (var lp in levelPack)
        {
            //Membuat salian objek dari prefab tombol level pack
            var t = Instantiate(_tombolLevelPack);

            t.SetLevelPack(lp);

            //Masukan objek tombol sebagai anak dari objek "content"
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;

            //cek apakah level pack terdaftar di Dictionary progres pemain
            if (!playerData.progresLevel.ContainsKey(lp.name))
            {
                t.KunciLevelPack();
            }
        }
    }

   

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack, LevelPackKuis levelPack, bool terkunci)
    {
        if (terkunci)
        {
            return;
        }

        //Buka Menu Levels
        //_levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        //Tutup Menu Level Packs
        //gameObject.SetActive(false);

        _inisialData.levelPack = levelPack;

        _animator.SetTrigger("KeLevels");

}
}
