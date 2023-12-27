using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
public class SaveManager : MonoBehaviour
{
    private string path;

    private SaveData data = new SaveData();

    private string characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,[](){}:\". ";
    private string encryptionKey = "`[/';.,:|@()mzlapqnxksowbcjdievhfurgtyMZLAPQNXKSOWBCJDIEVHFURGTY-=*%!^&$ ";

    private int saveInterval = 180;

    private bool isFirstTime;

    [SerializeField] private PlayerData playerData;
    private void Awake()
    {
        path = Application.persistentDataPath + "/save.json";
    }
    private void OnEnable()
    {
        EventMessenger.StartListening("Save", Save);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("Save", Save);
    }
    private void Start()
    {
        File.Delete(path);
        Load();

        InvokeRepeating("Save", saveInterval, saveInterval);
    }
    private void Save()
    {
        data.sfxVolume = VolumeManager.sfxVolume;
        data.bgmVolume = VolumeManager.bgmVolume;

        data.playerExtraJumpForce = playerData.extraJumpForce;
        data.playerExtraSpeed = playerData.extraSpeed;
        data.playerExtraJumps = playerData.extraJumps;
        data.playerLevel = playerData.level;
        data.playerExp = playerData.exp;

        data.controlNames = new List<string>();
        data.controlButtons = new List<string>();
        data.controlAltButtons = new List<string>();

        foreach (var control in InputManager.Controls)
        {
            data.controlNames.Add(control.Key);
            data.controlButtons.Add(control.Value[0].ToString());
            data.controlAltButtons.Add(control.Value[1].ToString());
        }

        data.completedPuzzleCounts = new List<int>();
        data.completedPuzzleNames = new List<string>();
        data.completedPuzzleStatuses = new List<bool>();

        string[] worlds = Enum.GetNames(typeof(GameManager.World));
        for (int i = 0; i < worlds.Length; i++)
        {
            data.completedPuzzleCounts.Add(PuzzleManager.completedDict[((GameManager.World)i).ToString()].Count);
            foreach (var puzzle in PuzzleManager.completedDict[((GameManager.World)i).ToString()])
            {
                data.completedPuzzleNames.Add(puzzle.Key);
                data.completedPuzzleStatuses.Add(puzzle.Value);
            }
        }

        WriteData();
    }
    private void Load()
    {
        ReadData();

        VolumeManager.Instance.SetVolumes(data.sfxVolume, data.bgmVolume);

        if (!isFirstTime)
        {
            InputManager.SetControls(data.controlNames, data.controlButtons, data.controlAltButtons);
        }

        playerData.extraJumpForce = data.playerExtraJumpForce;
        playerData.extraSpeed = data.playerExtraSpeed;
        playerData.extraJumps = data.playerExtraJumps;
        playerData.level = data.playerLevel;
        playerData.exp = data.playerExp;
        EventMessenger.TriggerEvent("UpdatePlayerData");

        string[] worlds = Enum.GetNames(typeof(GameManager.World));
        int index = 0;
        for (int i = 0; i < worlds.Length; i++)
        {
            for (int j = 0; j < data.completedPuzzleCounts[i]; j++)
            {
                PuzzleManager.completedDict[((GameManager.World)i).ToString()].Add(data.completedPuzzleNames[index], 
                    data.completedPuzzleStatuses[index]);
                index++;
            }
        }

        EventMessenger.TriggerEvent("GameLoaded");

        isFirstTime = false;
    }
    private void WriteData()
    {
        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText(path, Encrypt(jsonData));
    }

    private void ReadData()
    {
        if (File.Exists(path))
        {
            string contents = File.ReadAllText(path);

            data = JsonUtility.FromJson<SaveData>(Decrypt(contents));
        }
        else
        {
            isFirstTime = true;
            VolumeManager.Instance.SetVolumes(0.7f, 0.7f);
            InputManager.Instance.LoadDefaultControls();

            Save();
            ReadData();
        }
    }
    private string Encrypt(string jsonData)
    {
        string encryptedData = "";
        for (int i = 0; i < jsonData.Length; i++)
        {
            encryptedData += encryptionKey[characters.IndexOf(jsonData[i])];
        }
        return encryptedData;
    }
    private string Decrypt(string contents)
    {
        string decryptedString = "";
        for (int i = 0; i < contents.Length; i++)
        {
            decryptedString += characters[encryptionKey.IndexOf(contents[i])];
        }
        return decryptedString;
    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
