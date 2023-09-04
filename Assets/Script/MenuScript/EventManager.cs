using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager :EventTrigger
{
    Animator amr;

    // Start is called before the first frame update
    void Start()
    {

        amr = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.tag== "Button") 
        {
            amr.SetBool("IsOk",true);
            Debug.Log("OnPointerEnter");
            //amr.Play();
        
        }


        
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.tag == "Button")
        {
            amr.SetBool("IsOk", false);
            Debug.Log("OnPointerExit");
            //amr.Play();

        }

    }

}
