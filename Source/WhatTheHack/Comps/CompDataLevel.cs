﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace WhatTheHack.Comps
{
    public class CompDataLevel : ThingComp
    {
        private float accumulatedData = 0;
        private float levelledData = 0;
        public int curLevel = 1;
        private float extraDataNextLevel = 20;
        private const int MAXLEVEL = 5;
        //CompRefuelable.Refuel() is prefixed with Harmony to call AccumulateData so data is accumulated in the accumulatedData variable
        public void AccumulateData(float amount)
        {
            accumulatedData += amount;
            MaybeLevelUp();
        }
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref accumulatedData, "accumulatedData");
            Scribe_Values.Look(ref levelledData, "levelledData");
            Scribe_Values.Look(ref curLevel, "curLevel");
            Scribe_Values.Look(ref extraDataNextLevel, "extraDataNextLevel");
        }

        private float DataNextLevel
        {
            get
            {
                return levelledData + extraDataNextLevel;
            }
        }
        private float DataNeededNextLevel
        {
            get
            {
                return DataNextLevel - accumulatedData;
            }
        }     

        public override string CompInspectStringExtra()
        {
            string text = "WTH_AccumulatedData_CurLevel".Translate(curLevel);
            if(curLevel != MAXLEVEL)
            {
                text += "\n" + "WTH_AccumulatedData_DataNeededNextLevel".Translate(DataNeededNextLevel.ToStringDecimalIfSmall());
            }
            else
            {
                text += "\n" + "WTH_AccumulatedData_MaxLevel".Translate();
            }
            return text;

        }
        private void MaybeLevelUp()
        {
            if (curLevel < MAXLEVEL)
            {
                while (accumulatedData > DataNextLevel)
                {
                    Levelup();
                }
            }

        }
        private void Levelup()
        {
            levelledData += extraDataNextLevel;
            curLevel += 1;
            extraDataNextLevel *= 1.5f;
        }

    }
}