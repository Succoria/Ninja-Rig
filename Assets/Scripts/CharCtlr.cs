using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCtlr : MonoBehaviour {

    void Update()
    {
        var z = Input.GetAxis("Horizontal") * Time.deltaTime * 15.0f;

        transform.Translate(0, 0, z);
    }
}