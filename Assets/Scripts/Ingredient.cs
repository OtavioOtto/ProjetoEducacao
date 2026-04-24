using UnityEngine;

public class Ingredient
{
    private bool hasMeat;
    private int typeFood; // 1: saudavel 2: processado 3: ultra-processado
    private string allergy;
    private int price;

    public Ingredient(bool hasMeat, int typeFood, string allergy, int price)
    {
        this.hasMeat = hasMeat;
        this.typeFood = typeFood;
        this.allergy = allergy;
        this.price = price;
    }
}
