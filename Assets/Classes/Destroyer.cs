﻿using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
