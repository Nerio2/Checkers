using System.Collections.Generic;
using UnityEngine;

public class PawnsController : MonoBehaviour {
    //public:
    public GameObject Pawn1;
    public GameObject Pawn2;
    public GameObject MoveMark;

    //private:
    private List<GameObject> Player0Pawns = new List<GameObject>();
    private List<GameObject> Player1Pawns = new List<GameObject>();
    private List<GameObject> marks = new List<GameObject>();
    private GameObject marked;

    void Start() {
        int colx = 7;
        int rowy = 0;
        int i = 0;
        for ( int y = 0 ; y < 3 ; y++ ) {

            for ( int x = 0 ; x < 4 ; x++ ) {
                GameObject newPawn0 = createPawn(colx , rowy , Pawn1 , i , 0 , false);
                GameObject newPawn1 = createPawn(7 - colx , 7 - rowy , Pawn2 , i , 1 , false);
                Player0Pawns.Add(newPawn0);
                Player1Pawns.Add(newPawn1);
                colx -= 2;
                i++;
            }
            colx = 6 + y % 2;
            rowy += 1;
        }

    }

    GameObject createPawn(float x , float y , GameObject pawn , int id , int player , bool super) {
        GameObject newPawn = Instantiate(pawn , transform);
        newPawn.transform.position = transform.position + new Vector3(2 * x , 2 * y , 0);
        newPawn.GetComponent<PawnController>().id = id;
        newPawn.GetComponent<PawnController>().player = player;
        newPawn.GetComponent<PawnController>().super = super;
        return newPawn;
    }


    void Update() {


    }

    public void chosen(int x , int y , int player , int id) {
        click();
        if ( player == 0 ) {
            marked = Player0Pawns [id];
        } else {
            marked = Player1Pawns [id];
        }
        if ( marked.GetComponent<PawnController>().super ) {
            ;
        } else {
            for ( int i = 0 ; i < 2 ; i++ ) {
                bool notSkip = true;
                bool attack = false;
                Vector3 obstruction=new Vector3(0,0,0);
                Vector3 pawnPosition = marked.transform.position;
                Vector3 markposition = new Vector3(2 - ( 4 * ( i % 2 ) ) + pawnPosition.x , 2 - ( player % 2 * 4 ) + pawnPosition.y , 0);
                Player0Pawns.ForEach(obj => {
                    if ( obj.transform.position == markposition && player == 0 ) {
                        notSkip = false;
                    } else if( obj.transform.position == markposition && player == 1 ) {
                        attack = true;
                        obstruction = obj.transform.position;
                    }
                });
                Player1Pawns.ForEach(obj => {
                    if ( obj.transform.position == markposition && player == 1 ) {
                        notSkip = false;
                    } else if ( obj.transform.position == markposition && player == 0 ) {
                        attack = true;
                        obstruction = obj.transform.position;
                    }
                });
                if ( markposition.x > -8 && markposition.x < 8 && markposition.y < 8 && markposition.y > -8 && notSkip && !attack) {
                    
                    GameObject mark = Instantiate(MoveMark , marked.transform);
                    mark.transform.position = markposition;
                    marks.Add(mark);
                }
            }
        }
    }
    public void move(Vector3 position) {
        click();
        marked.transform.position = position;
    }

    public void click() {
        marks.ForEach(mark => {
            Destroy(mark);
        });
    }
}
