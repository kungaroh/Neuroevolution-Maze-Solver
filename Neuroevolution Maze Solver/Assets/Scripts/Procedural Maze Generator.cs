using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using ColorUtility = UnityEngine.ColorUtility;

public class ProceduralMazeGenerator : MonoBehaviour
{
    Texture2D texture;

    private System.Random rnd;
    
    public int width = 21;  // Must be odd
    public int height = 21; // Must be odd
    public Color wallColor = Color.black;
    public Color pathColor = Color.white;

    private Texture2D mazeTexture;
    private bool[,] visited;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (width % 2 == 0 || height % 2 == 0)
        {
            Debug.LogError("Width and Height must be odd.");
            return;
        }
        StartGeneration();
    }

    public void StartGeneration()
    {
        Texture2D maze = GenerateMaze();
        
        maze.SetPixel(1,height-1, Color.white);
        maze.SetPixel(width-2, 0, Color.white);
        maze.Apply();
        
        Sprite sprite = Sprite.Create(maze, new Rect(0, 0, maze.width, maze.height), new Vector2(0.5f, 0.5f));
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        byte[] bytes = ImageConversion.EncodeToPNG(maze);
        File.WriteAllBytes(Application.dataPath + "/Textures/Maze.png", bytes);
    }
    
    Texture2D GenerateMaze()
    {
        mazeTexture = new Texture2D(width, height);
        mazeTexture.filterMode = FilterMode.Point;
        visited = new bool[width, height];

        // Fill with walls
        for (int a = 0; a < width; a++)
        {
            for (int b = 0; b < height; b++)
            {
                mazeTexture.SetPixel(a, b, wallColor);
            }
        }

        System.Random rng = new System.Random();

        // Start at a random odd cell
        int x = rng.Next(width / 2) * 2 + 1;
        int y = rng.Next(height / 2) * 2 + 1;
        visited[x, y] = true;
        mazeTexture.SetPixel(x, y, pathColor);

        int totalCells = ((width - 1) / 2) * ((height - 1) / 2);
        int visitedCells = 1;

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, -2), // up
            new Vector2Int(0, 2),  // down
            new Vector2Int(-2, 0), // left
            new Vector2Int(2, 0),  // right
        };

        while (visitedCells < totalCells)
        {
            // Random direction
            Vector2Int dir = directions[rng.Next(directions.Length)];
            int nx = x + dir.x;
            int ny = y + dir.y;

            // Bounds check
            if (nx > 0 && ny > 0 && nx < width && ny < height)
            {
                if (!visited[nx, ny])
                {
                    visited[nx, ny] = true;
                    visitedCells++;

                    // Carve the wall in between
                    int wallX = x + dir.x / 2;
                    int wallY = y + dir.y / 2;

                    mazeTexture.SetPixel(wallX, wallY, pathColor);
                    mazeTexture.SetPixel(nx, ny, pathColor);
                }

                // Always move (even to visited)
                x = nx;
                y = ny;
            }
        }

        mazeTexture.Apply();
        return mazeTexture;
    }
}
