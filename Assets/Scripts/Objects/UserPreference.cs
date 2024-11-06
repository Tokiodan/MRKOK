using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserPreference", menuName = "UserPreference")]
public class UserPreference : ScriptableObject
{
   public float FOV;
   public float sensitivity;
   public float volume;
   [SerializeField]
   public int defaultSave = -1;

   [System.NonSerialized]
   public int SelectedSave;

   public int ChosenSave
   {
      get { return SelectedSave; }
      set { SelectedSave = value; }
   }


   public SaveFile[] saves;

   private void OnEnable()
   {
      SelectedSave = defaultSave;
   }

   public void SelectSave(int save)
   {
      ChosenSave = save;
      saves[ChosenSave].UpdateTimeStamp();
      Debug.Log(ChosenSave);
   }
}
