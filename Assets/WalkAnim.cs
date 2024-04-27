using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnim : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    void Start()
    {
        animator.SetBool("Walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
