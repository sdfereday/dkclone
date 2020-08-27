using UnityEngine;
using System;

public class Tile : MonoBehaviour
{
    public Guid Id = System.Guid.NewGuid();
    public bool selected = false;
    public virtual void OnSelected() { }
}
