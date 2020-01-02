using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour
{
    public Transform NavMeshTransform;
    public GameObject FloorTile;
    public GameObject WallTile;
    public GameObject RockTile;
    public GameObject NavAgentTestObject;

    private NavMeshSurface Surface;

    /* Try to keep it in odd numbers so it centers properly */
    private int[,] MapTest = new int[,] {
        { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        { 2, 0, 0, 0, 0, 0, 0, 0, 2 },
        { 2, 0, 1, 1, 1, 0, 0, 0, 2 },
        { 2, 0, 1, 0, 1, 0, 0, 0, 2 },
        { 2, 0, 1, 1, 1, 1, 0, 0, 2 },
        { 2, 0, 0, 0, 0, 1, 1, 1, 2 },
        { 2, 1, 1, 0, 0, 0, 0, 1, 2 },
        { 2, 1, 1, 0, 0, 1, 1, 1, 2 },
        { 2, 2, 2, 2, 2, 2, 2, 2, 2 }
    };

    private void Start()
    {
        var mapWidth = MapTest.GetLength(0);
        var mapXCenter = mapWidth / 2;

        var mapHeight = MapTest.GetLength(1);
        var mapYCenter = mapHeight / 2;

        Surface = NavMeshTransform.GetComponent<NavMeshSurface>();
        NavMeshTransform.localScale = new Vector3(mapWidth, 1, mapHeight);
        
        /* Instantiate starting map tiles */
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
                    case 2:
                        Instantiate(RockTile, spawnPos, Quaternion.identity, transform);
                        break;
                }
            }
        }

        /* Always generate 'after' map creation or map tile update (can we edit regions for performance?) */
        Surface.BuildNavMesh();

        /* Spawn a nav mesh agent test to run about */
        Instantiate(NavAgentTestObject);
    }
}
