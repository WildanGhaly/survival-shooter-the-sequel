using System.Collections;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private GameObject firstScene;
    [SerializeField] private GameObject closeToPet;
    [SerializeField] private GameObject rescuedPetScene;
    void Start(){
        firstScene.SetActive(true);
        closeToPet.SetActive(true);
    }
}