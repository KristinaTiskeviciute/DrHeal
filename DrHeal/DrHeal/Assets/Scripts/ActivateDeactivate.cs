using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateDeactivate : MonoBehaviour {
    public GameObject Deactivate;
    public GameObject Activate;
    public Image item;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "hero")
        {
            item.GetComponent<Image>().color = Color.white;
            Deactivate.SetActive(false);
            Activate.SetActive(true);
        }
    }

}
