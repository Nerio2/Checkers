﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour {
	public int id;
	public int player;
	public bool queen;
	public Sprite Defult;
	public Sprite Queen;
	 // Start is called before the first frame update
	 void Start() {
		queen = false;
	}

	// Update is called once per frame
	void Update() {
		if ( queen )
			GetComponent<SpriteRenderer>().sprite = Queen;
	}

	private void OnMouseDown() {
		GetComponentInParent<PawnsController>().chosen(gameObject);
	}
}
