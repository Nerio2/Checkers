﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour {
    public int id;
    public int player;
    public bool super;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnMouseDown() {
        GetComponentInParent<PawnsController>().chosen(( int ) transform.position.x / 2 , ( int ) transform.position.y / 2 , player , gameObject);
    }
}
