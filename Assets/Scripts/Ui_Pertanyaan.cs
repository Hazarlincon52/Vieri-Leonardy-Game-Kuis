using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Pertanyaan : MonoBehaviour
{
   
    [SerializeField]
    public TextMeshProUGUI _tempatTeks = null;

    [SerializeField]
    private TextMeshProUGUI _tempatJudulLevel = null;

    [SerializeField]
    public Image _tempatGambar = null;

    
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Isi tempat teks yaitu:");
        Debug.Log(_tempatTeks.text);
    }

    // Update is called once per frame
    public void SetPertanyaan(string teksPertanyaan, string teksJudulLevel, Sprite gambarHint)
    {
        _tempatTeks.text = teksPertanyaan;
        _tempatJudulLevel.text = teksJudulLevel;
        _tempatGambar.sprite = gambarHint;
    }
}
