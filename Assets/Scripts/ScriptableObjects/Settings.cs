using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObject/Settings")] 
    public class Settings : SingletonScriptableObject<Settings>
    {
        
        [NonSerialized] private AudioMixer audioMixer;
        public const string PATH_TO_SETTINGS = "/Settings.json";
        public const string PATH_TO_BOARD = "/Save";

        private Quality quality;
        private int gameSound;
        public int gameResolution;
        public float settingsVersion;

        public int GameSound
        {
            get => gameSound;
            set
            {
                if (audioMixer == null)
                {
                    audioMixer = Resources.Load<AudioMixer>("Sound/GameAudio");
                }
                gameSound = value;
                audioMixer.SetFloat("GameVol", Mathf.Log10(Mathf.Clamp(gameSound/100f,0.0001f,1f))*20);
            }
        }

        public Quality Quality
        {
            get => quality;
            set => quality = value;
        }

        [NonSerialized] public Resolution[] availableResolutions;
        [NonSerialized] public SavableBoard savableBoard = new SavableBoard();
        public void SetResolution()
        {
            Screen.SetResolution(Instance.availableResolutions[Instance.gameResolution].width,
                Instance.availableResolutions[Instance.gameResolution].height,true);
        }

        public void SetQuality()
        {
            QualitySettings.SetQualityLevel((int)Instance.Quality);
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitSettings()
        {
            SetSettings();
            
            void SetSettings()
            {
                if (!File.Exists(Application.persistentDataPath+PATH_TO_SETTINGS))
                {
                    CreateSettings();
                    string savedSettings = JsonConvert.SerializeObject(Instance);
                    File.WriteAllText(Application.persistentDataPath+PATH_TO_SETTINGS,savedSettings);
                }
                var loadedSettings = File.ReadAllText(Application.persistentDataPath + PATH_TO_SETTINGS);
                Settings settings = JsonConvert.DeserializeObject<Settings>(loadedSettings);
                
                Instance.Quality = settings.quality;
                Instance.GameSound = settings.gameSound;
                Instance.gameResolution = settings.gameResolution;
                Instance.availableResolutions = Screen.resolutions.Select(res => new Resolution { width = res.width, height = res.height }).Distinct().ToArray();
            }
            
        }

        private static void CreateSettings()
        {
            Instance.availableResolutions = Screen.resolutions.Select(res => new Resolution { width = res.width, height = res.height }).Distinct().ToArray();
            Instance.gameSound = 50;
            Instance.quality = Quality.Ultra;
            Instance.gameResolution = Instance.availableResolutions.Length-1;
        }

        public void SaveSettings()
        {
            try
            {
                string savedSettings = JsonConvert.SerializeObject(Instance, Formatting.Indented);
                File.WriteAllText(Application.persistentDataPath + PATH_TO_SETTINGS, savedSettings);
            }
            catch(IOException e)
            {
                Debug.Log(e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public string[] GetFileSaves()
        {
            if (!Directory.Exists(Application.persistentDataPath + PATH_TO_BOARD))
            {
                return new string[0];
            }
            
            return Directory.GetFiles(Application.persistentDataPath+PATH_TO_BOARD).Select(Path.GetFileName).ToArray();
        }

        public SavableBoard GetSaveFile(string _path)
        {
            try
            {
                string saveFile = File.ReadAllText(_path);
                return JsonConvert.DeserializeObject<SavableBoard>(saveFile);
            }
            catch (IOException e)
            {
                Debug.Log(e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public void SaveBoard()
        {
            if (!Directory.Exists(Application.persistentDataPath + PATH_TO_BOARD))
            {
                Directory.CreateDirectory(Application.persistentDataPath + PATH_TO_BOARD);
            }
            string date = $"/{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year} {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.json";
            SavableBoard boardToSave = savableBoard;
            string savedBoard = JsonConvert.SerializeObject(boardToSave);
            try
            {
                File.WriteAllText(Application.persistentDataPath+PATH_TO_BOARD+date,savedBoard);
            }
            catch (IOException e)
            {
                Debug.Log(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

    }
    [Serializable]
    public class SavableBoard
    {
        public List<SavableBall> savableBalls = new List<SavableBall>();
        public List<SavablePot> savablePots = new List<SavablePot>();
    }
    
    [Serializable]
    public class SavableBall
    {
        public int id;
        public int[] coord = new int[2];
        public Extensions.SavableVector3 savableVector3;
    }

    [Serializable]
    public class SavablePot
    {
        public int id;
        public int potStatus;
    }


    
    
    
}
