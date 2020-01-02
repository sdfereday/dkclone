using UnityEngine;

public class Wall : Tile
{
    public bool selected = false;
    public int health = 3;
    public bool fortified = false;
    public AFFINITY affinity = AFFINITY.NEUTRAL;

    private MeshRenderer rend;
    private Color defColour;
    private Color modColour;

    private void Start()
    {
        rend = GetComponentInChildren<MeshRenderer>();
        defColour = rend.material.color;
        modColour = Color.green;
    }

    public override void OnSelected()
    {
        Debug.Log("Clicked a wall.");

        selected = !selected;
        rend.material.color = selected ? modColour : defColour;
    }
}
