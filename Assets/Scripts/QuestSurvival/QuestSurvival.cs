using UnityEngine;
using System.Collections;

public class QuestSurvival : MonoBehaviour
{
    [SerializeField] private int waktu = 120;
    void Start()
    {
        StartCoroutine(OpenDoor(waktu));
    }

    void Update()
    {

    }

    private IEnumerator OpenDoor(int waktu)
    {
        yield return new WaitForSeconds(waktu);
        gameObject.GetComponent<Animator>().SetBool("isOpen", true);
    }
}
