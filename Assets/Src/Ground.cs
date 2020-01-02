using UnityEngine;

public class Ground : Tile
{
    public bool claimed = false;
    public int health = 3;
    public AFFINITY affinity = AFFINITY.NEUTRAL;

    public override void OnSelected()
    {
        Debug.Log("Clicked the ground.");
    }
}
