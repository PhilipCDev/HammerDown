using HammerDown.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test : MonoBehaviour
{
    public Collider c;
    public List<Transform> nails;
    public Board board;

    // Update is called once per frame
    void Update()
    {
        board.IsPlankFixed(new RectanglePos(c.bounds.min, c.bounds.max), nails.Select(t => t.position).ToList());
    }
}
