using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[CreateAssetMenu
    (
        fileName = "Player Progress Baru",
        menuName = "Game Kuis/Player Progress"
    )]

public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progresLevel;
    }
    [SerializeField]
    private string _fileName = "playerprogres.txt";

    [SerializeField]
    private string _startingLevelPackname = string.Empty;

	public MainData progressData = new MainData();

    public void SimpanProgress()
    {
        //Sample Data
        
        //progressData.koin = 0;
        /*
         if (progressData.progresLevel == null)
         { 
         progressData.progresLevel = new();
         }
        */
        //progressData.progresLevel.Add("Level Pack 1", 3);
        //progressData.progresLevel.Add("Level Pack 3", 5);

        //simpan starting data saat objeck Dictionary tidak ada saat dimuat
        if (progressData.progresLevel == null)
        {
			
			progressData.progresLevel = new();
            progressData.koin = 0;
            progressData.progresLevel.Add(_startingLevelPackname,1);
        }

        //informasi peyimpanan data
#if UNITY_EDITOR
        string directory = Application.dataPath + "/Temporary/";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.presistentDataPath + "/ProgresLokal/";
#endif

        var path = directory + _fileName;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory created: " + directory);
        }

        //Membuat file baru
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }


        //Meyimpan data ke dalam file menggunakan binari
        var fileStream = File.Open(path, FileMode.OpenOrCreate);
        //var formatter = new BinaryFormatter();
        
        fileStream.Flush();
        //formatter.Serialize(fileStream, progressData);

        var writer = new BinaryWriter(fileStream);
        

        //var kontenData = $"{progressData.koin}\n";
        writer.Write(progressData.koin);
        
        foreach (var i in progressData.progresLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }
        writer.Dispose();
        

        fileStream.Dispose();

        Debug.Log("Data saved to file: " + path);
    }

    public bool MuatProgres()
    {
        //TODO: Prosudur untuk muat data

         var directory = Application.dataPath + "/Temporary/";
         var path = directory + _fileName;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory created: " + directory);
        }

        //Membuat file baru
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        //Memuat data dari file menggunakan binari formatter 
        var fileStream = File.Open(path, FileMode.OpenOrCreate);
        

        try
        {

            var reader = new BinaryReader(fileStream);

            try
            {
                progressData.koin = reader.ReadInt32();
                if (progressData.progresLevel == null)
                {
                    progressData.progresLevel = new();
                }

                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
					Debug.Log($"{namaLevelPack}:{levelKe}");
					progressData.progresLevel.Add(namaLevelPack, levelKe);
                }

                reader.Dispose();
            }

            catch(System.Exception e)
            {
                Debug.Log($"ERROR: Terjadi kesalahan saat memuat binari.\n{e.Message}");

                //Putuskan aliran memori dengan File
                reader.Dispose();
                fileStream.Dispose();

                return false;
            }
            //Memuat data dari file menggunakan binari formatter 
            var formatter = new BinaryFormatter();

            progressData = (MainData)formatter.Deserialize(fileStream);

            //Putuskan aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"{progressData.koin}; {progressData.progresLevel.Count}");

            return true;
        }

        catch (System.Exception e)
        {
            Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres.\n{e.Message}");

            //Putuskan aliran memori dengan File
            fileStream.Dispose();

            return false;
        }
        

    }
}
