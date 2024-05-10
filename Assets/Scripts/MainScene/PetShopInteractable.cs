using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class PetShopInteractable : Interactable
{
    [SerializeField] private GameObject UI;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject petItemPrefab;
    [SerializeField] private TextMeshProUGUI coinText;
    private int c;
    private List<PetData> availablePets = new List<PetData>();

    void Start()
    {
        Debug.Log("Load");
        Sprite tejoSprite = Resources.Load<Sprite>("Tejo");
        if (tejoSprite != null)
        {
            AddPetToShop(new PetData(0, "Tejo", 256, tejoSprite));
        }

        Sprite healerSprite = Resources.Load<Sprite>("Healer");
        if (healerSprite != null)
        {
            AddPetToShop(new PetData(1, "Healer", 128, healerSprite));
        }
        else
        {
            Debug.LogError("Failed to load sprite for Healer");
        }

        Sprite agusSprite = Resources.Load<Sprite>("Agus");
        if (agusSprite != null)
        {
            AddPetToShop(new PetData(2, "Agus", 200, agusSprite));
        }
        else
        {
            Debug.LogError("Failed to load sprite for Agus");
        }

    }

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
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (PetData pet in availablePets)
        {
            GameObject newItem = Instantiate(petItemPrefab, contentPanel);
            newItem.GetComponent<PetBuy>().SetId(pet.id);

            TextMeshProUGUI nameText = newItem.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            Image petImage = newItem.transform.Find("Image").GetComponent<Image>();
            TextMeshProUGUI priceText = newItem.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

            if (nameText != null && priceText != null && petImage != null)
            {
                nameText.text = pet.Name;
                priceText.text = pet.Price.ToString();
                petImage.sprite = pet.Image;
            }
        }
    }

    public void AddPetToShop(PetData pet)
    {
        availablePets.Add(pet);
    }
    private void FixedUpdate()
    {
        c = GameManager.INSTANCE.coin;
        coinText.text = $"Coins: {c}";
    }
    public class PetData
    {
        public int id;
        public string Name;
        public float Price;
        public Sprite Image;

        // ctor
        public PetData(int _id, string _name, float _price, Sprite _image)
        {
            id = _id;
            Name = _name;
            Price = _price;
            Image = _image;
        }
    }
}