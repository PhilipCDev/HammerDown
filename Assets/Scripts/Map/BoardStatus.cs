using HammerDown.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammerDown.Map
{
    //Do TODOs, Discuss Hannah:
    public class BoardStatus : MonoBehaviour
    {
        private HashSet<Plank> UnusedPlanks = new HashSet<Plank>(); //Planks that can potential be moved. Potential moved != !Fixed. Fixed only if 3-4 Nails completly in. Potential move as long no nail in
        private HashSet<Nail> UnusedNails = new HashSet<Nail>(); //Nails that can potential be moved. That means you can still move them or hit them by a hammer
        private HashSet<Plank> FixedPlanks = new HashSet<Plank>(); //Planks that are fixed. Board.IsPlankFixed return true for nails that are in the plank

        private bool isChecking = false;

        #region privateFunctions
        private void CheckStatus()
        {
            if(UnusedNails.Count == 0 && UnusedPlanks.Count == 0)
            {
                //TODO: END
                ResetStatus();
            }
            else
            {
                if (false)//TODO: Check if all holes are covered
                {
                    //TODO: END
                    ResetStatus();
                }
            }
        }
        private void ResetStatus()
        {
            UnusedPlanks = new HashSet<Plank>();
            UnusedNails = new HashSet<Nail>();
            FixedPlanks = new HashSet<Plank>();
            isChecking = false;
        }
        #endregion

        #region public Functions
        //call to add plank to unused planks. Call for each plank in current game. Not in Scene, in Game!
        public void AddPlank(Plank plank)
        {
            UnusedPlanks.Add(plank);
        }

        //call to add nail to unused nail. Call for each plank in current game. Not in Scene, in Game!
        public void AddNail(Nail nail)
        {
            UnusedNails.Add(nail);
        }

        //Call after all Nails and Boards are added
        public void StartChecking()
        {
            isChecking = true;
            //Hannah: where should I call this? can you call it after removing it from a list? - Hannah
        }

        //call to remove plank from unused planks. Call when plank is attached by one nail (just single stroke in the wall), or when gameobject is whyever destroyed
        public void RemovePlank(Plank plank)
        {
            if (UnusedPlanks.Contains(plank))
            {
                UnusedPlanks.Remove(plank);
                if (isChecking)
                {
                    CheckStatus();
                }
            }
        }

        //call to remove nail from unused nails. Call when nail is attached completely in the wall or has been hit wrong (is broken), or when gameobject is whyever destroyed
        public void RemoveNail(Nail nail)
        {
            //Hannah: it could happen, that i remove it multiple times from the list 
            if (UnusedNails.Contains(nail))
            {
                UnusedNails.Remove(nail);
                if (isChecking)
                {
                    CheckStatus();
                }
            }
        }

        //call to add plank to fixed planks. Call when Board.IsPlankFixed return true
        public void AddFixedPlanks(Plank plank) 
        {
            //Hannah:  Nail nail changed type from nail to plank? Correct
            FixedPlanks.Add(plank);
            CheckStatus();
        }
        #endregion
    }
}
