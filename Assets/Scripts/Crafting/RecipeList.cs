using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeList", menuName = "Inventory/RecipeList")]
public class RecipeList : ScriptableObject
{
    public ItemRecipe[] Recipies = new ItemRecipe[0];


    // goes through the list of recipies using a HashSet and returns the new item that has to be created.
    public ItemObject FindRecipe(int _A, int _B)
    {
        HashSet<int> targetSet = new HashSet<int> { _A, _B };

        for (int i = 0; i < Recipies.GetLength(0); i++)
        {
            HashSet<int> currentSet = new HashSet<int> { Recipies[i].recipe[0].Id, Recipies[i].recipe[1].Id };

            if (currentSet.SetEquals(targetSet))
            {
                return Recipies[i].result;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ItemRecipe
{
    public ItemObject[] recipe = new ItemObject[2];
    public ItemObject result;
}
