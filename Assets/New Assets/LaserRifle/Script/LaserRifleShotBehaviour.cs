﻿using UnityEngine;
using System.Collections;

public class LaserRifleShotBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * 30f;
	}
}
