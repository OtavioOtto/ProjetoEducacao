// Create this as a ScriptableObject for easy editing in Unity
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IngredientMapping", menuName = "Game/Ingredient Mapping")]
public class IngredientMapping : ScriptableObject
{
    [System.Serializable]
    public struct IngredientPrefab
    {
        public string ingredientName;
        public GameObject prefab;
    }

    public IngredientPrefab[] ingredientPrefabs;

    private Dictionary<string, GameObject> prefabDictionary;

    public void Initialize()
    {
        prefabDictionary = new Dictionary<string, GameObject>();
        foreach (var item in ingredientPrefabs)
        {
            if (!prefabDictionary.ContainsKey(item.ingredientName))
                prefabDictionary.Add(item.ingredientName, item.prefab);
        }
    }

    public GameObject GetPrefab(string ingredientName)
    {
        ingredientName = ingredientName.ToUpper();
        if (prefabDictionary == null) Initialize();

        if (prefabDictionary.TryGetValue(ingredientName, out GameObject prefab))
            return prefab;

        Debug.LogWarning($"No prefab found for ingredient: {ingredientName}");
        return null;
    }
}