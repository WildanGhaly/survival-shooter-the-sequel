using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PetShopInteractable : Interactable
{
    [SerializeField] private GameObject UI;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject petItemPrefab;

    private List<PetData> availablePets = new List<PetData>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UI.activeSelf)
        {
            CloseUI();
        }
    }

    protected override void Interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        UI.SetActive(true);
        PopulatePetList();
        base.Interact();
    }

    private void CloseUI()
    {
        UI.SetActive(false);
    }

    private void PopulatePetList()
    {
        // remove existing game object
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        availablePets.Clear();
        AddPetToShop(new PetData(1, "Pet 1", 256));
        AddPetToShop(new PetData(2, "Pet 2", 128));
        Debug.Log(availablePets.Count);

        foreach (PetData pet in availablePets)
        {
            GameObject newItem = Instantiate(petItemPrefab, contentPanel);
            petItemPrefab.GetComponent<PetBuy>().SetId(pet.id);

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
        public int id;
        public string Name;
        public float Price;

        // ctor
        public PetData(int _id, string _name, float _price)
        {
            id = _id;
            Name = _name;
            Price = _price;
        }
    }
}