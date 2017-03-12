using Mono.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public float CellWidth = 48;
    public float CellHeight = 48;
    public int FieldWidth = 15;
    public int FieldHeight = 15;

    private Collection<Vector3> _cells;
     

	// Use this for initialization
    private void Start ()
    {
        var parent = gameObject;

    }

    // Update is called once per frame
    private void Update ()
    {
		
	}
}
