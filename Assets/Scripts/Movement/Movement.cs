using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameGrid _grid;
    [SerializeField] private Control _control;

    private Coroutine _moving;

    public Speed Speed { get; private set; } = new Speed(0.5f);
    public bool MovementFinished { get; private set; } = true;
    public Vector3Int CurrentGridPosition => PositionConverter.Wrap(transform.position, _grid.GridType);
    public Cell CurrentCell { get; private set; }

    private void Start()
    {
        if (_grid.TryGetCell(CurrentGridPosition, out Cell cell))
            CurrentCell = cell;
    }

    private void Update()
    {
        if (_control.Clicked)
        {
            MoveTo(_control.TargetedCell.GridPosition);
        }
    }

    public void MoveTo(Vector3Int targetPosition, int earlyStop = 0)
    {
        if (_moving != null)
            StopCoroutine(_moving);

        var path = _grid.PathCreator.GetPath(CurrentGridPosition, targetPosition);

        _moving = StartCoroutine(MovingAlongPath(path, earlyStop));
    }

    public void Stop()
    {
        if (_moving != null)
            StopCoroutine(_moving);

        MoveTo(CurrentGridPosition);
    }

    private IEnumerator MovingAlongPath(List<Vector3Int> path, int earlyStop)
    {
        MovementFinished = false;

        int endPathPointIndex = path.Count - earlyStop;
        endPathPointIndex = Mathf.Clamp(endPathPointIndex, 0, path.Count);

        for (int i = 0; i < endPathPointIndex; i++)
        {
            yield return MovingToNextPoint(path[i]);

            CurrentCell = _grid.GetCell(path[i]);
        }

        MovementFinished = true;
    }

    private IEnumerator MovingToNextPoint(Vector3Int targetGridPosition)
    {
        Vector3 targetPosition = _grid.GetCell(targetGridPosition).transform.position;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while(elapsedTime< Speed.CellsPerSecond)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime/ Speed.CellsPerSecond);

            Rotate(targetPosition);

            yield return null;
        }
    }

    private void Rotate(Vector3 targetPosition)
    {
        var direction =targetPosition- transform.position;
        transform.rotation = Quaternion.LookRotation(direction,Vector3.up);
    }
}
