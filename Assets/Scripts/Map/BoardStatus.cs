﻿using HammerDown.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HammerDown.Map
{
    //Do TODOs, Discuss Hannah:
    public class BoardStatus : MonoBehaviour
    {
        public List<Nail> addNails;
        public List<Plank> addPlanks;

        private HashSet<Plank> UnusedPlanks = new HashSet<Plank>(); //Planks that can potential be moved. Potential moved != !Fixed. Fixed only if 3-4 Nails completly in. Potential move as long no nail in
        private HashSet<Nail> UnusedNails = new HashSet<Nail>(); //Nails that can potential be moved. That means you can still move them or hit them by a hammer
        private HashSet<Plank> LoosePlanks = new HashSet<Plank>(); //Planks that can not move but are also not fixedM=MOM=
        private HashSet<Plank> FixedPlanks = new HashSet<Plank>(); //Planks that are fixed. Board.IsPlankFixed return true for nails that are in the plank

        private bool isChecking = false;

        #region privateFunctions
        private void CheckStatus()
        {
            if(UnusedNails.Count == 0 && UnusedPlanks.Count == 0)
            {
                Game.instance.GameOver();
            }
            else
            {
                if (Game.instance.board.CalcHoleCoverage(FixedPlanks.ToList()) >= 0.95f)
                {
                    Game.instance.GameOver();
                }
            }
        }
        private void ResetStatus()
        {
            UnusedPlanks = new HashSet<Plank>();
            UnusedNails = new HashSet<Nail>();
            FixedPlanks = new HashSet<Plank>();
            LoosePlanks = new HashSet<Plank>();
            isChecking = false;
        }
        private void Awake()
        {
            foreach(Plank p in addPlanks)
            {
                UnusedPlanks.Add(p);
            }
            foreach (Nail n in addNails)
            {
                UnusedNails.Add(n);
            }
        }
        private void Unregister()
        {
            isChecking = false;
            Game.instance.GameOver -= Unregister;
            ResetStatus();
        }
        #endregion

        #region public Functions
        //call to remove plank from unused planks. Call when plank is attached by one nail (just single stroke in the wall), or when gameobject is whyever destroyed
        public void RemovePlank(Plank plank)
        {
            if (UnusedPlanks.Contains(plank))
            {
                UnusedPlanks.Remove(plank);
                LoosePlanks.Add(plank);
                if (isChecking)
                {
                    CheckStatus();
                }
            }
        }

        public void StartChecking()
        {
            isChecking = true;
            Game.instance.GameOver += Unregister;
        }

        //call to remove nail from unused nails. Call when nail is attached completely in the wall or has been hit wrong (is broken), or when gameobject is whyever destroyed
        public void RemoveNail(Nail nail)
        {
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
            FixedPlanks.Add(plank);
            LoosePlanks.Remove(plank);
            CheckStatus();
        }
        #endregion
    }
}
