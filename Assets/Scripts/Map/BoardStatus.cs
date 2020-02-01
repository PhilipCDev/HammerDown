using HammerDown.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammerDown.Map
{
    public class BoardStatus : MonoBehaviour
    {
        private HashSet<Plank> UnusedPlanks; //Planks that can potential be moved. Potential moved != !Fixed. Fixed only if 3-4 Nails completly in. Potential move as long no nail in
        private HashSet<Nail> UnusedNails; //Nails that can potential be moved. That means you can still move them or hit them by a hammer
        private HashSet<Plank> FixedPlanks; //Planks that are fixed. Board.IsPlankFixed return true for nails that are in the plank

        private void CheckStatus()
        {

        }

        //call to add plank to unused planks. Call for each plank in current game. Not in Scene, in Game!
        public void AddPlank(Plank plank)
        {

        }

        //call to add nail to unused nail. Call for each plank in current game. Not in Scene, in Game!
        public void AddNail(Nail nail)
        {

        }

        //Call after all Nails and Boards are added
        public void StartChecking()
        {

        }

        //call to remove plank from unused planks. Call when plank is attached by one nail (just single stroke in the wall), or when gameobject is whyever destroyed
        public void RemovePlank(Plank plank)
        {

        }

        //call to remove nail from unused nails. Call when nail is attached completely in the wall or has been hit wrong (is broken), or when gameobject is whyever destroyed
        public void RemoveNail(Nail nail)
        {

        }

        //call to add plank to fixed planks. Call when Board.IsPlankFixed return true
        public void AddFixedPlanks(Nail nail)
        {

        }
    }
}
