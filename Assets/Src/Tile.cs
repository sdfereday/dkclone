using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool selected = false;
    public virtual void OnSelected() { }
}
