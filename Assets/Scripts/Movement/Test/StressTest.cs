using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StressTest: MonoBehaviour
{
    [SerializeField] private Movement[] _movement;
    [SerializeField] private GameGrid _gameGrid;

    private void Start()
    {
        StartCoroutine(RandomizingMovement());
    }

    private IEnumerator RandomizingMovement()
    {

        while (true)
        {
            foreach (var movement in _movement)
            {
                if (movement.MovementFinished)
                {
                    int index = Random.Range(0, _gameGrid.GridPositions.Count);
                    Vector3Int targetPosition = _gameGrid.GridPositions[index];
                    
                    movement.MoveTo(targetPosition);

                }

                yield return null;
            }
        }

    }
}
