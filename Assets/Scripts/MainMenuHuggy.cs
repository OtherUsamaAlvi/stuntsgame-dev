using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHuggy : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Animator animator;
    TouchPhase touchPhase = TouchPhase.Ended;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit h;

                if (Physics.Raycast(ray, out h))
                {

                    if (h.collider.tag == "Huggy")
                    {
                        animator.SetBool("isWaving", true);
                        StartCoroutine(Waitforsec());
                    }
                }
            }
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag == "Huggy")
                    {
                        animator.SetBool("isWaving", true);
                        StartCoroutine(Waitforsec());
                    }

                }
            }
        }
    }
    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("isWaving", false);
    }
}
