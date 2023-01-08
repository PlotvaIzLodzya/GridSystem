using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public bool Clicked { get; private set; } 
    public Cell TargetedCell { get; private set; }
    
    private void Update()
    {
        TargetedCell = null;
        Clicked = false;

        if (Input.GetMouseButtonDown(0))
        {
            if(TryGetCell(Input.mousePosition, out Cell cell))
            {
                Clicked = true;
                TargetedCell = cell;
            }
        }
    }

    public bool TryGetCell(Vector3 screenPosition, out Cell cell)
    {
        cell = null;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out cell))
            {
            }
        }

        return cell != null;
    }
}
