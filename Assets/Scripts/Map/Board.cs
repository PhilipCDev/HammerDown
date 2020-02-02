using HammerDown.Map.Components;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HammerDown.Tools;

namespace HammerDown.Map
{
    public class Board : MonoBehaviour
    {
        private float friendlyness = 2;
     
        //List of all holes, just use and not change entries! Currently holes are not implemented
        public List<Hole> holes;
        public Transform HandPos;
        public Transform HammerPos;
        public Transform CameraPos;
        public float maxTime;

        #region public Functions
        //Bool function. Give a point and returns if it is on a board. Check for Nail (Nail can only be nailed in if OnBoard is true)
        public bool IsOnBoard(Transform obj)
        {
            RaycastHit[] hits = Physics.RaycastAll(obj.transform.position + obj.transform.up * 0.5f, - obj.transform.transform.up, 1, 1 << 12 | 1 << 14);
            if (hits.Length > 0 && !hits.Any(h => h.collider.gameObject.layer == 14))
            {
                return true;
            }
            return false;
        }

        //Call Whenever a nail is nailed completly in. Returns true only if the Plank is fully fixed. Needs position of the board and position of all full nailed in and not broken nails.
        public bool IsPlankFixed(RectanglePos position, List<Vector3> nailPositions)
        {
            float refDistance = Vector3.SqrMagnitude(position.leftBottomFront - position.rightTopBack);

            Dictionary<int, float> distanceMap = new Dictionary<int, float>();
            for(int i=0; i< nailPositions.Count; i++)
            {
                for (int j = 0; j < nailPositions.Count; j++)
                {
                    if (i < j)
                    {
                        if (!distanceMap.ContainsKey(i << 8 + j))
                        {
                            distanceMap.Add(i << 8 + j, Vector3.SqrMagnitude(nailPositions[i] - nailPositions[j]));
                        }
                    }
                    else
                    {
                        if (j < i)
                        {
                            if (!distanceMap.ContainsKey(j << 8 + i))
                            {
                                distanceMap.Add(j << 8 + i, Vector3.SqrMagnitude(nailPositions[i] - nailPositions[j]));
                            }
                        }
                    }
                }
            }

            float sumDistance = 0;
            foreach(int key in distanceMap.Keys)
            {
                sumDistance += distanceMap[key];
            }
            sumDistance *= friendlyness;

            return sumDistance > refDistance;
        }

        //used for evaluating points, you do not need this
        public float CalcHoleCoverage(List<Plank> planks)
        {
            float sum = 0;
            foreach(Hole hole in holes)
            {
                float bestValue = 0;
                foreach(Plank plank in planks)
                {
                    Bounds boundA = plank.GetComponent<Collider>().bounds;
                    boundA.extents += transform.forward;


                    Bounds boundB = hole.GetComponent<Collider>().bounds;
                    Vector3 invForward = new Vector3(1 - transform.forward.x, 1 - transform.forward.y, 1 - transform.forward.z);

                    Vector3 sizeA = boundA.size;
                    boundA.Encapsulate(boundB);
                    Vector3 sizeABDiff = boundA.size-sizeA;

                    float value = (1 - Mathf.Clamp((sizeABDiff.x / boundB.size.x), 0, 1)) * (1 - Mathf.Clamp((sizeABDiff.y / boundB.size.y), 0, 1)) * (1 - Mathf.Clamp((sizeABDiff.z / boundB.size.z), 0, 1)); 
                    if (value > bestValue)
                    {
                        bestValue = value;
                    }
                }
                sum += bestValue;
            }
            return sum / (float) holes.Count;
        }
        #endregion
    }

    public struct RectanglePos{
        public Vector3 leftBottomFront;
        public Vector3 rightTopBack;

        public RectanglePos(Vector3 _leftBottomFront, Vector3 _rightTopBack)
        {
            leftBottomFront = _leftBottomFront;
            rightTopBack = _rightTopBack;
        }
    }
}
