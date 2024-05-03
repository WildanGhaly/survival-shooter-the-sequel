using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenWizard : MonoBehaviour
{
    [SerializeField] private Animator wizardAnim;

    private void OnEnable()
    {
        StartCoroutine(AriseWizard());
    }

    IEnumerator AriseWizard()
    {
        yield return new WaitForSeconds(1);
        wizardAnim.SetBool("Arise", true);
    }
}
