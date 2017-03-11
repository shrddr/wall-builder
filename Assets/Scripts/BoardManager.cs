using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject[] FloorTiles;
    public int XSize = 5;
    public int YSize = 5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupScene()
    {
        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                GameObject tileChoice = FloorTiles[Random.Range(0, FloorTiles.Length)];
                Instantiate(tileChoice, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
