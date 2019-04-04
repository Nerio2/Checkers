using System.Collections.Generic;
using UnityEngine;

public class PawnsController : MonoBehaviour {
    //public:
    public GameObject Pawn1;
    public GameObject Pawn2;
    public GameObject MoveMark;
    public int round = 1;             //1-player1     0-player0

    //private:

    private List<List<GameObject>> PlayerPawns = new List<List<GameObject>>();
    private Dictionary<GameObject , GameObject> marks = new Dictionary<GameObject , GameObject>();
    private GameObject marked;
    private bool block = false;

    void Start() {
        int colx = 7;
        int rowy = 0;
        int i = 0;
        List<GameObject> Player0Pawns = new List<GameObject>();
        List<GameObject> Player1Pawns = new List<GameObject>();
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
        PlayerPawns.Add(Player0Pawns);
        PlayerPawns.Add(Player1Pawns);

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
        if ( round > 1 )
            round = 0;

    }

    // checking if is atack avaiable for the player
    public bool isAtackAvailable(int player) {
        for ( int i = 0 ; i < 2 ; i++ ) {
            for ( int j = 0 ; j < PlayerPawns [i].Count ; j++ )
                if ( isAtackAvailable(PlayerPawns [i] [j]) )
                    return true;
        }
        return false;
    }

    // return true if is atack is avaiable for the pawn
    public bool isAtackAvailable(GameObject pawn) {
        bool avaiable = false;
        int player = pawn.GetComponent<PawnController>().player == 1 ? 0 : 1;
        Vector3 pawnPosition = pawn.transform.position;
        for ( int i = 0 ; i < 2 ; i++ ) {
            for ( int j = 0 ; j < 2 ; j++ ) {
                Vector3 markposition = new Vector3(2 - ( 4 * ( i % 2 ) ) + pawnPosition.x , 2 - ( j % 2 * 4 ) + pawnPosition.y , 0);
                PlayerPawns [player].ForEach(obj => {
                    bool skip = false;
                    if ( obj.transform.position == markposition ) {
                        Vector3 objPosition = obj.transform.position;
                        markposition = new Vector3(markposition.x + ( objPosition.x - pawnPosition.x ) , markposition.y + ( objPosition.y - pawnPosition.y ) , 0);
                        PlayerPawns [player].ForEach(objj => {
                            if ( objj.transform.position == markposition )
                                skip = true;
                        });
                        if ( !skip ) {
                            avaiable = true;
                            return;
                        }
                    }
                });
                if ( avaiable )
                    return avaiable;
            }
        }

        return false;
    }

    private void markMoves(GameObject pawn , bool attackOnly) {
        int player = pawn.GetComponent<PawnController>().player;
        Vector3 pawnPosition = pawn.transform.position;
    }

    public void chosen(GameObject pawn) {
        marked = pawn;
        Debug.Log(isAtackAvailable(pawn));
        int player = pawn.GetComponent<PawnController>().player;
        click();
        if ( block && !pawn.Equals(marked) ) {
            chosen(marked);
            return;
        }
        if ( player != round )
            return;
        Vector3 pawnPosition = marked.transform.position;
        if ( marked.GetComponent<PawnController>().super ) {
            ;
        } else {
            for ( int i = 0 ; i < 2 ; i++ ) {
                for ( int j = 0 ; j < 2 ; j++ ) {
                    bool skip = false;
                    bool changed = false;
                    Vector3 obstruction = new Vector3(0 , 0 , 0);
                    Vector3 markposition = new Vector3(2 - ( 4 * ( i % 2 ) ) + pawnPosition.x , 2 - ( j % 2 * 4 ) + pawnPosition.y , 0);
                    GameObject beaten = null;
                    PlayerPawns [0].ForEach(obj => {
                        if ( obj.transform.position == markposition ) {
                            if ( player == 1 && !changed ) {
                                changed = true;
                                Vector3 objPosition = obj.transform.position;
                                markposition = new Vector3(markposition.x + ( objPosition.x - pawnPosition.x ) , markposition.y + ( objPosition.y - pawnPosition.y ) , 0);
                                PlayerPawns [0].ForEach(objj => {
                                    if ( objj.transform.position == markposition )
                                        skip = true;
                                });
                                if ( !skip ) {
                                    beaten = obj;
                                }
                            } else skip = true;
                        }
                    });
                    PlayerPawns [1].ForEach(obj => {
                        if ( obj.transform.position == markposition ) {
                            if ( player == 0 && !changed ) {
                                changed = true;
                                Vector3 objPosition = obj.transform.position;
                                markposition = new Vector3(markposition.x + ( objPosition.x - pawnPosition.x ) , markposition.y + ( objPosition.y - pawnPosition.y ) , 0);
                                PlayerPawns [1].ForEach(objj => {
                                    if ( objj.transform.position == markposition )
                                        skip = true;
                                });
                                if ( !skip ) {
                                    beaten = obj;
                                }
                            } else skip = true;
                        }
                    });
                    PlayerPawns [0].ForEach(obj => {
                        if ( obj.transform.position == markposition && player == 0 )
                            skip = true;
                    });
                    if ( beaten == null && ( block ||
                        ( player == 0 ? markposition.y < pawn.transform.position.y : markposition.y > pawn.transform.position.y ) ) )
                        skip = true;
                    else if ( markposition.x > -8 && markposition.x < 8 && markposition.y < 8 && markposition.y > -8 && !skip ) {
                        GameObject mark = Instantiate(MoveMark , marked.transform);
                        mark.transform.position = markposition;
                        marks.Add(mark , beaten);
                    }
                }
            }
            if ( marks.Count == 0 ) {
                round++;
                block = false;
            }
        }
    }
    public void move(Transform mark) {
        block = false;
        round++;
        marked.transform.position = mark.position;
        foreach ( var obj in marks ) {
            if ( obj.Key.transform == mark ) {
                if ( obj.Value != null ) {

                    if ( obj.Value.GetComponent<PawnController>().player == 0 )
                        PlayerPawns [0].Remove(obj.Value);
                    else
                        PlayerPawns [1].Remove(obj.Value);
                    Destroy(obj.Value);
                    round--;
                    block = true;
                    chosen(marked);


                }
            }
        }
        if ( !block )
            click();

    }

    public void click() {
        foreach ( var obj in marks ) {
            Destroy(obj.Key);
        }
        marks = new Dictionary<GameObject , GameObject>();

    }
}
