using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    //Untuk memuat target Scene dengan name.
    //Nama Scene ini sensitif

    public void BukaScene(string namaScene)
    {
        SceneManager.LoadScene(namaScene);
    }
}
