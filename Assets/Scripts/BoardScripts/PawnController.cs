using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour {
    public int id;
    public int player;
    public bool queen;
    // Start is called before the first frame update
    void Start() {
		queen = false;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnMouseDown() {
        GetComponentInParent<PawnsController>().chosen(gameObject);
    }
}
