using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserPreference", menuName = "UserPreference")]
public class UserPreference : ScriptableObject
{
   public float FOV;
   public float sensitivity;
   public float volume;
}
