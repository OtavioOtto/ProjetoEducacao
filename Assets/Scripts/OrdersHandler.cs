using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersHandler : MonoBehaviour
{
    [SerializeField] private CraftingManager craftingManager;
    [SerializeField] private int minIngredients = 3;
    [SerializeField] private int maxIngredients = 5;

    private List<Ingredients> currentOrder = new List<Ingredients>();

    public List<Ingredients> ReturnOrder()
    {
        if (craftingManager == null)
        {
            Debug.LogError("CraftingManager not assigned!");
            return null;
        }

        // Get all available recipes
        List<Recipe> availableRecipes = craftingManager.GetAllRecipes();

        if (availableRecipes == null || availableRecipes.Count == 0)
        {
            Debug.LogError("No recipes available!");
            return null;
        }

        // Filter recipes by ingredient count (only include recipes within min/max range)
        List<Recipe> validRecipes = availableRecipes
            .Where(recipe => recipe.requiredIngredients.Count >= minIngredients
                          && recipe.requiredIngredients.Count <= maxIngredients)
            .ToList();

        // If no recipes match the range, fall back to all recipes
        if (validRecipes.Count == 0)
        {
            Debug.LogWarning($"No recipes found with {minIngredients}-{maxIngredients} ingredients. Using all recipes.");
            validRecipes = availableRecipes;
        }

        // Select a random recipe
        Recipe selectedRecipe = validRecipes[Random.Range(0, validRecipes.Count)];

        // ORDER THE COMPLETE RECIPE (not just a subset)
        currentOrder = new List<Ingredients>(selectedRecipe.requiredIngredients);

        Debug.Log($"Order created: {selectedRecipe.recipeName} with {currentOrder.Count} ingredients");

        // Optional: Log the ingredients for debugging
        string ingredientsList = string.Join(", ", currentOrder);
        Debug.Log($"Ingredients: {ingredientsList}");

        return currentOrder;
    }
}