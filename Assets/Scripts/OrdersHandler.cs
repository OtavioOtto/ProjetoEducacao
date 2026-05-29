using UnityEngine;

public class OrdersHandler : MonoBehaviour
{
    private string[] allIngredients = { "ALFACE", "ARROZ", "BATATA", "BATATA_DOCE", "BERINJELA", "BROCOLIS", "CARNE", "CENOURA", "FEIJAO"};
    private string[] selectedIngredients = new string[5];

    public string[] ReturnIngredients()
    {
        if (allIngredients == null || allIngredients.Length == 0)
        {
            Debug.LogError("A lista de palavras está vazia!");
            return null;
        }

        int maxSelectable = Mathf.Min(5, allIngredients.Length);
        int wordCount = Random.Range(3, maxSelectable + 1);

        string[] randomIngredients = (string[])allIngredients.Clone();

        for (int i = 0; i < randomIngredients.Length; i++)
        {
            string temp = randomIngredients[i];
            int randomIndex = Random.Range(i, randomIngredients.Length);
            randomIngredients[i] = randomIngredients[randomIndex];
            randomIngredients[randomIndex] = temp;
        }

        selectedIngredients = new string[wordCount];
        for (int i = 0; i < wordCount; i++)
        {
            selectedIngredients[i] = randomIngredients[i];
        }

        return selectedIngredients;
    }
}