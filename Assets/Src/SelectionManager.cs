﻿using UnityEngine;
using System.Linq;

public class SelectionManager : MonoBehaviour
{
    public float checkDistance = 1000f;
    private Transform current;

    private void DoOnPrimaryClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, checkDistance);
        Clickable firstClickable = hits
            .Where(x => x.transform.GetComponent<Clickable>() != null)
            .Select(x => x.transform.GetComponent<Clickable>())
            .FirstOrDefault();

        if (firstClickable != null)
        {
            firstClickable.TriggerIt();
        }
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DoOnPrimaryClick();
    }
}
