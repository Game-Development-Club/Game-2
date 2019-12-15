using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    public List<Puzzle> puzzles;

    private void Start()
    {
        if (instance == null) instance = this;

        puzzles = new List<Puzzle>();
    }

    public void StartPuzzle(string puzzleName)
    {

    }
}
