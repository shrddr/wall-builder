using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject[] FloorTiles;
    public GameObject[] BorderTiles;
    public int XSize = 5;
    public int YSize = 5;

    // Use this for initialization
    private void Start ()
    {
		
	}
	
	// Update is called once per frame
    private void Update ()
    {
		
	}

    public void SetupScene()
    {
        for (int x = -1; x <= XSize; x++)
        {
            for (int y = -1; y <= YSize; y++)
            {
                GameObject tileChoice;

                if (x == -1 || x == XSize || y == -1 || y == YSize)
                    tileChoice = BorderTiles[Random.Range(0, BorderTiles.Length)];
                else
                    tileChoice = FloorTiles[Random.Range(0, FloorTiles.Length)];
                    
                Instantiate(tileChoice, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
