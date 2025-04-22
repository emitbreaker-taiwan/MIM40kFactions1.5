﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace MIM40kFactions
{
    public class PlaceWorker_ShowBeamTurretRadius : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            VerbProperties verbProperties = ((ThingDef)checkingDef).building.turretGunDef.Verbs.Find((VerbProperties v) => v.verbClass == typeof(Verb_Shoot) || typeof(Verb_Spray).IsAssignableFrom(v.verbClass) || typeof(Verb_ShootBeam).IsAssignableFrom(v.verbClass) || typeof(Verb_SpewFireCustom).IsAssignableFrom(v.verbClass) || typeof(Verb_ShootLasBeam).IsAssignableFrom(v.verbClass) || typeof(Verb_ArcSprayPenetrate).IsAssignableFrom(v.verbClass));
            if (verbProperties.range > 0f)
            {
                GenDraw.DrawRadiusRing(loc, verbProperties.range);
            }

            if (verbProperties.minRange > 0f)
            {
                GenDraw.DrawRadiusRing(loc, verbProperties.minRange);
            }

            return true;
        }
    }
}
