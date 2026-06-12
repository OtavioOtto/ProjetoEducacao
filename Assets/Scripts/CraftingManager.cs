using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CraftingManager : MonoBehaviour
{
    [SerializeField] private List<Recipe> allRecipes;

    public GameObject CraftItem(List<Ingredients> selectedIngredients)
    {
        if (selectedIngredients.Count > 5)
        {
            Debug.Log("Too many ingredients! Max 5.");
            return null;
        }

        foreach (Recipe recipe in allRecipes)
        {
            if (MatchesRecipe(selectedIngredients, recipe.requiredIngredients))
            {
                Debug.Log($"Crafted: {recipe.recipeName}!");
                return Instantiate(recipe.craftedFoodPrefab);
            }
        }

        Debug.Log("No matching recipe found!");
        return null;
    }

    public string GetRecipeName(List<Ingredients> selectedIngredients)
    {
        if (selectedIngredients.Count > 5)
        {
            Debug.Log("Too many ingredients! Max 5.");
            return null;
        }

        foreach (Recipe recipe in allRecipes)
        {
            if (MatchesRecipe(selectedIngredients, recipe.requiredIngredients))
            {
                return recipe.recipeName;
            }
        }

        Debug.Log("No matching recipe found!");
        return null;
    }

    private bool MatchesRecipe(List<Ingredients> playerIngredients, List<Ingredients> recipeIngredients)
    {
        if (playerIngredients.Count != recipeIngredients.Count)

            return false;

        var sortedPlayer = playerIngredients.OrderBy(i => i).ToList();
        var sortedRecipe = recipeIngredients.OrderBy(i => i).ToList();

        for (int i = 0; i < sortedPlayer.Count; i++)
        {
            if (sortedPlayer[i] != sortedRecipe[i])
                return false;
        }
        return true;
    }

    public List<Recipe> GetAllRecipes()

    {
        return allRecipes;
    }
}