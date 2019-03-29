using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnsController : MonoBehaviour{
    //public:
    public GameObject Pawn1;
    public GameObject Pawn2;

    //private:
    private List<GameObject> Player1Pawns = new List<GameObject>();
    private List<GameObject> Player2Pawns = new List<GameObject>();
    private Dictionary<int, Vector2> Player1Positions = new Dictionary<int, Vector2>();
    private Dictionary<int, Vector2> Player2Positions = new Dictionary<int, Vector2>();

    void Start(){
        int colx = 7;
        int rowy = 0;
        int i = 0;
        for (int y = 0; y < 3; y++)
        {
            
            for (int x = 0; x < 4; x++)
            {
                GameObject newPawn1 = createPawn(colx, rowy, Pawn1);
                GameObject newPawn2 = createPawn(7-colx, 7-rowy, Pawn2);
                Player1Pawns.Add(newPawn1);
                Player2Pawns.Add(newPawn2);
                Player1Positions.Add(i, new Vector2(colx, rowy));
                Player2Positions.Add(i, new Vector2(7 - colx, 7 - rowy));
                colx -= 2;
                i++;
            }
            colx = 6 + y % 2;
            rowy += 1;
        }
        
    }

    GameObject createPawn(float x, float y, GameObject pawn)
    {
        GameObject newPawn = Instantiate(pawn, this.transform);
        newPawn.transform.position = transform.position + new Vector3(2 * x, 2 * y, 0);
        return newPawn;
    }


    void Update(){
        
        
    }
}
