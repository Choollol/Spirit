using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SaveManager : MonoBehaviour
{
    private string path;

    private SaveData data = new SaveData();

    private string characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,[]{}:\". ";
    private string encryptionKey = "`[/';.,:|@mzlapqnxksowbcjdievhfurgtyMZLAPQNXKSOWBCJDIEVHFURGTY-=*%!^&$ ";

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

        data.controlNames.Clear();
        data.controlButtons.Clear();
        data.controlAltButtons.Clear();

        data.playerExtraJumpForce = playerData.extraJumpForce;
        data.playerExtraSpeed = playerData.extraSpeed;
        data.playerExtraJumps = playerData.extraJumps;

        foreach (var control in InputManager.Controls)
        {
            data.controlNames.Add(control.Key);
            data.controlButtons.Add(control.Value[0].ToString());
            data.controlButtons.Add(control.Value[1].ToString());
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
        EventMessenger.TriggerEvent("UpdatePlayerData");

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
