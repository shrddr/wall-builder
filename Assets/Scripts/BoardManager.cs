using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject horizontalWall;
    public GameObject verticalWall;
    public GameObject[] FloorTiles;
    public GameObject[] BorderTiles;
    public int XSize = 9;
    public int YSize = 9;

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

        SetupWalls();
    }

    private void SetupWalls()
    {
        for (var x = 0; x <= XSize - 1; x++)
        {
            for (var y = 0.5; y <= YSize - 1; y++)
            {
                Instantiate(horizontalWall, new Vector3(x, (float)y, 0), Quaternion.identity);
            }
        }

        for (var x = 0.5; x <= XSize - 1; x++)
        {
            for (var y = 0; y <= YSize - 1; y++)
            {
                Instantiate(verticalWall, new Vector3((float)x, y, 0), Quaternion.identity);
            }
        }
    }
}
