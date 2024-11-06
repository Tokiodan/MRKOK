using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "saveFile", menuName = "saveFile")]
public class SaveFile : ScriptableObject
{
    public InventoryObject Inventory;
    public Vector3 playerPosition;
    public int CurrentLevel;
    public string LastPlayed;

    public void UpdateTimeStamp()
    {
        Debug.Log("Hi");
        LastPlayed = DateTime.Now.ToString("MM:HH dd MMMM yyyy");
    }
}
