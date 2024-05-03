using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PetShopInteractable : Interactable
{
    [SerializeField] private GameObject UI;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject petItemPrefab;

    private List<PetData> availablePets = new List<PetData>();

    protected override void Interact()
    {
        UI.SetActive(true);
        PopulatePetList();
        base.Interact();
    }

    private void PopulatePetList()
    {
        AddPetToShop(new PetData("Pet 1", 1231));
        AddPetToShop(new PetData("Pet 2", 122413)); 
        Debug.Log(availablePets.Count);
        foreach (PetData pet in availablePets)
        {
            GameObject newItem = Instantiate(petItemPrefab, contentPanel);
            TextMeshProUGUI nameText = newItem.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI priceText = newItem.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

            if (nameText != null && priceText != null)
            {
                nameText.text = pet.Name;
                priceText.text = pet.Price.ToString();
            }
        }
    }

    public void AddPetToShop(PetData pet)
    {
        availablePets.Add(pet);

    }


    public class PetData
    {
        public string Name;
        public float Price;

        // ctor
        public PetData(string _name, float _price)
        {
            Name = _name;
            Price = _price;
        }
    }
}
