using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    public static event System.Action EventWaktuHabis;
    /*[SerializeField]
    private UI_PesanLevel _tempatPesan = null;
    */
    [SerializeField]
    private Slider _timeBar = null;

    [SerializeField]
    private float _waktuJawab = 30.0f;//Dalam Detik
    private float _sisaWaktu = 0;
    private bool _waktuBerjalan = false;

    public bool WaktuBerjalan
    {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;

    }
    private void Start()
    {
        UlangWaktu();    
        _waktuBerjalan = true;
    }

    private void Update()
    {
        if (!_waktuBerjalan)
        {
            return;

        }
        //Dikurangi sebanyak waktu menggambar satu frame (Second per Frame)

        _sisaWaktu -= Time.deltaTime;
        _timeBar.value = _sisaWaktu/_waktuJawab;

        

        if (_sisaWaktu <= 0f)
        {
            /*_tempatPesan.Pesan = "Waktu Habis";
            _tempatPesan.gameObject.SetActive(true);*/

            //Debug.Log("Waktu Habis");
            EventWaktuHabis?.Invoke();
            _waktuBerjalan = false;
            return;
        }
        //Debug.Log(_sisaWaktu);
    }
    public void UlangWaktu()
    {
        _sisaWaktu = _waktuJawab;
    }
}
