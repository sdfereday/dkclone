using UnityEngine;

public class Clickable : MonoBehaviour
{
    private Tile ParentTile;

    private void Start()
    {
        ParentTile = GetComponentInParent<Tile>();
    }

    public virtual void TriggerIt()
        => ParentTile.OnSelected();
}
