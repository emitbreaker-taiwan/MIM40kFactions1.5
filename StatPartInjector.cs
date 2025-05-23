﻿using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace MIM40kFactions
{
    // Runs automatically after all defs are loaded.
    [StaticConstructorOnStartup]
    public static class StatPartInjector
    {
        static StatPartInjector()
        {
            try
            {
                // 1) Ensure the StatDefOf static constructor has run and all fields are loaded:
                DefOfHelper.EnsureInitializedInCtor(typeof(StatDefOf));

                // 2) List every StatDef you want to patch:
                StatDef[] statsToPatch = new[]
                {
                    StatDefOf.MeleeHitChance,
                    StatDefOf.ShootingAccuracyPawn,
                    StatDefOf.MeleeDamageFactor,
                    StatDefOf.InjuryHealingFactor,
                    StatDefOf.AimingDelayFactor,
                    StatDefOf.RangedCooldownFactor,
                    StatDefOf.IncomingDamageFactor
                };

                // Filter out any null StatDefs
                foreach (var sd in statsToPatch.Where(s => s != null))
                {
                    // 3) Make sure the parts list exists
                    if (sd.parts == null)
                    {
                        sd.parts = new List<StatPart>();
                    }

                    // 4) Create a new StatPart for each StatDef (don't reuse the same instance)
                    var part = new StatPart_VariableStatBonus();
                    part.stat = sd;

                    // Check if we already have this type of StatPart on this stat
                    bool alreadyExists = sd.parts.Any(p => p is StatPart_VariableStatBonus);
                    if (!alreadyExists)
                    {
                        sd.parts.Add(part);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error in StatPartInjector: {ex}");
            }
        }
    }
}