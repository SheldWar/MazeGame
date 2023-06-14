using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    private int mazeSize;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    void Start()
    {
        mazeSize = GetComponent<ChangeLVL>().startMazeSize;
        var currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (currentLevel > 1)
        {
            mazeSize += currentLevel;
            transform.position += new Vector3(currentLevel / 2, 0f, -(currentLevel / 2));
            if (currentLevel % 2 != 0)
            {
                transform.position -= new Vector3(0f, 0f, 1f);
            }
            GetComponent<ChangeLVL>().endRock.transform.position -= new Vector3(-(currentLevel), 0f, currentLevel);
        }
        var maze = MazeGenerator.Generate(mazeSize, mazeSize);
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {
        for (int i = 0; i < mazeSize; ++i)
        {
            for (int j = 0; j < mazeSize; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3(-mazeSize / 2 + i, 0, -mazeSize / 2 + j);

                if (cell.HasFlag(WallState.UP) && i != 0 && j != 0)
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(transform.position.x, transform.position.y, transform.position.z + size / 2);
                    topWall.localScale = new Vector3(size * 1.85f, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(transform.position.x - size / 2, transform.position.y, transform.position.z);
                    leftWall.localScale = new Vector3(size * 1.85f, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (cell.HasFlag(WallState.RIGHT))
                {
                    var rightWall = Instantiate(wallPrefab, transform) as Transform;
                    rightWall.position = position + new Vector3(transform.position.x + size / 2, transform.position.y, transform.position.z);
                    rightWall.localScale = new Vector3(size * 1.85f, rightWall.localScale.y, rightWall.localScale.z);
                    rightWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (cell.HasFlag(WallState.DOWN) && i != mazeSize - 1 && j != mazeSize - 1)
                {
                    var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                    bottomWall.position = position + new Vector3(transform.position.x, transform.position.y, transform.position.z - size / 2);
                    bottomWall.localScale = new Vector3(size * 1.85f, bottomWall.localScale.y, bottomWall.localScale.z);
                }

            }

        }

    }
}
