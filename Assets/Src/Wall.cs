using UnityEngine;
using System.Linq;

public class Wall : Tile
{
    public int health = 3;
    public bool fortified = false;
    public AFFINITY affinity = AFFINITY.NEUTRAL;
    public Transform[] digNodes;

    private MeshRenderer rend;
    private Color defColour;
    private Color modColour;

    private void Start()
    {
        digNodes = GetComponentsInChildren<Transform>()
            .Where(x => x.CompareTag("DigNode"))
            .Select(x => x)
            .ToArray();
        
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
