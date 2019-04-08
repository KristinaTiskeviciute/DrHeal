using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateDeactivate : MonoBehaviour {
    public GameObject Deactivate;
    public GameObject Activate;
    public Image item;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("syringe");
            //item.GetComponent<Image>().color = Color.white;
            Deactivate.SetActive(false);
            Activate.SetActive(true);
        }
    }

}
