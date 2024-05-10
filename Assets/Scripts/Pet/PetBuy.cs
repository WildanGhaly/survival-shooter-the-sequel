using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBuy : MonoBehaviour
{
    int idPet;

    public void BuyPet()
    {
        GameManager.INSTANCE.AddPet(idPet);
    }

    public void SetId(int id)
    {
        idPet = id;
    }
}
