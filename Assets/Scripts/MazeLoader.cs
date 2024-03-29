﻿using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
    public GameObject floor;
    public GameObject player;
    public GameObject EndPoint;
	public float size = 2f; // Size of row or column

	private MazeCell[,] mazeCells;

	// Use this for initialization
	void Start () {
        // Called to creat maze grid
		InitializeMaze ();

		MazeAlgorithm ma = new GridDestroyMazeAlgorithm (mazeCells);
		ma.CreateMaze ();

        SpawnPlayer();
        SpawnEndPoint();
	}


	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

        // Iterate through the rows and columns creating a grid
		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				// Placing floor
				mazeCells [r, c] .floor = Instantiate (floor, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

                // Only add west wall if it's the first column, as this is the outer wall
				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
				}

                // Add east wall for each side
				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;

                // If the row is the first row, add north wall as this is the outer wall
				if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

                // Add south wall for each side
				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
			}
		}
	}

    private void SpawnPlayer()
    {
        GameObject myplayer = Instantiate(player, new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.transform.parent = myplayer.transform;
        cam.transform.localPosition = new Vector3(0,3f,-3f);
        cam.transform.Rotate(15, 0, 0);

    }

    private void SpawnEndPoint()
    {
        //int x = ProceduralNumberGenerator.GetXPosition(mazeRows);
        //int z = ProceduralNumberGenerator.GetZPosition(mazeColumns);

        GameObject End = Instantiate(EndPoint, new Vector3((mazeRows-1)*size, 2, (mazeColumns-1)*size), Quaternion.identity);

    }
}
