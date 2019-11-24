using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void COUOUCOUJESUISLA() {
        Destroy(transform.parent.gameObject);
    }
}
