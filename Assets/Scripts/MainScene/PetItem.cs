using UnityEngine;
using TMPro;
using static PetShopInteractable;

public class PetItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshPro priceText;

    //Setup the pet item with data
    private void Awake()
    {
        nameText = new TextMeshProUGUI();
        priceText = new TextMeshPro();
    }
    public void Setup(PetData data)
    {
        nameText.text = data.Name;
        priceText.text = "$" + data.Price.ToString("F2");

        Debug.Log("Success To Update" + nameText.text + priceText.text);
    }
}
