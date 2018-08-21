﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace WhatTheHack
{
    public class DefModExtension_Recipe : DefModExtension
    {
        public float surgerySuccessChanceFactor = -1f;
        public float deathOnFailedSurgeryChance = -1f;
        public bool requireBed;
        public HediffDef requiredHediff;
        public HediffDef addsAdditionalHediff;
        public float minBodySize = -1f;
    }
}
