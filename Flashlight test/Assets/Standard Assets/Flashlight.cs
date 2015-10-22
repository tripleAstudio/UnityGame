using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {
public Light flashlight;

void Update () {
     if (Input.GetKeyDown(KeyCode.Q))
             flashlight.enabled = !flashlight.enabled;
}
}