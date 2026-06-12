using System.Collections.Generic;
using UnityEngine;

public enum Ingredients
{
    Atum,
    Iogurte,
    Massa,
    MolhoDeTomate,
    Pao,
    Alface,
    Arroz,
    Batata,
    BatataDoce,
    Beringela,
    Brocolis,
    Carne,
    Cenoura,
    Feijao,
    Frango,
    Leite,
    Ovo,
    Peixe,
    Pimentao,
    Queijo,
    Tomate,
    Refrigerante,
    Salsicha,
    SucoArtificial
}

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public List<Ingredients> requiredIngredients;
    public GameObject craftedFoodPrefab;
}