using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject FloorTile;
    public GameObject WallTile;

    private int[,] MapTest = new int[,] {
        { 0, 0, 0, 0, 0 },
        { 0, 1, 1, 0, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 1, 1, 0 },
        { 0, 0, 0, 0, 0 }
    };

    private void Start()
    {
        var mapWidth = MapTest.GetLength(0);
        var mapXCenter = mapWidth / 2;

        var mapHeight = MapTest.GetLength(1);
        var mapYCenter = mapHeight / 2;

        for (var row = 0; row < mapWidth; row++)
        {
            for (var col = 0; col < mapHeight; col++)
            {
                var spawnPos = new Vector3(row - mapXCenter, 0.5f, col - mapYCenter);

                switch(MapTest[row, col])
                {
                    case 0:
                        Instantiate(FloorTile, spawnPos, Quaternion.identity, transform);
                        break;
                    case 1:
                        Instantiate(WallTile, spawnPos, Quaternion.identity, transform);
                        break;
                }
            }
        }
    }
}
