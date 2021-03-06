﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace MasterOfInsec.Combos
{
    static class Harrash
    {
        public static void Combo()
        {
            var target = TargetSelector.GetTarget(1300, TargetSelector.DamageType.Physical);
            if (target != null)
            {
                if (Program.QHarrash.IsReady() && Program.GetBool("QH") && ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Name == "BlindMonkQOne")
                {
                    Program.QHarrash.CastIfHitchanceEquals(target, Combos.Combo.HitchanceCheck(Program.menu.Item("seth").GetValue<Slider>().Value)); // Continue like that
                }
                if (Program.GetBool("QH") && ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Name == "blindmonkqtwo")
                {
                    Program.QHarrash.Cast(target);
                }
                if (Program.E.IsReady() && Program.GetBool("EH") && Program.E.IsInRange(target))
                {
                    Program.E.Cast();
                    if (Items.CanUseItem(3077) && Program.Player.Distance(target.Position) < 350)
                        Items.UseItem(3077);
                    if (Items.CanUseItem(3074) && Program.Player.Distance(target.Position) < 350)
                        Items.UseItem(3074);
                    if (Items.CanUseItem(3142) && Program.Player.Distance(target.Position) < 350)
                        Items.UseItem(3142);
                }


            }
        }
    }
}
