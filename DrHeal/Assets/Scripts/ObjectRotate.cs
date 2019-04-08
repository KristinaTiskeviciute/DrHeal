using UnityEngine;
using System.Collections;

public class ObjectRotate : MonoBehaviour
{

    public int spinx = 0;
    public int spiny = 7;
    public int spinz = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinx, spiny, spinz);
    }
}