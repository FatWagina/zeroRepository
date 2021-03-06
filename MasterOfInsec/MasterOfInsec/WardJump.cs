﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace MasterOfInsec
{
   static class WardJump
    {
       public static Vector3 posforward;
       public static float lastwardjump = 0;
       private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
       public static InventorySlot getBestWardItem()
       {
           InventorySlot ward = Items.GetWardSlot();
           if (ward == default(InventorySlot)) return null;
           return ward;
       }

       public static bool Harrasjump(Vector3 position)
       {
           #region ward ya existe
           if (Program.W.IsReady())
           {
               foreach (Obj_AI_Minion ward in ObjectManager.Get<Obj_AI_Minion>().Where(ward =>
                    ward.Name.ToLower().Contains("ward") && ward.Distance(Game.CursorPos) < 250))
               {
                   if (ward != null)
                   {
                       Program.W.CastOnUnit(ward);
                       Program.W.Cast();
                       return true;

                   }

               }

               foreach (
                   Obj_AI_Hero hero in ObjectManager.Get<Obj_AI_Hero>().Where(hero => hero.Distance(Game.CursorPos) < 250 && !hero.IsDead))
               {
                   if (hero != null)
                   {
                       Program.W.CastOnUnit(hero);
                       Program.W.Cast();
                       return true;

                   }

               }

               foreach (Obj_AI_Minion minion in ObjectManager.Get<Obj_AI_Minion>().Where(minion =>
                   minion.Distance(Game.CursorPos) < 250))
               {
                   if (minion != null)
                   {
                       Program.W.CastOnUnit(minion);
                       Program.W.Cast();
                       return true;
                   }

               }
           }
           #endregion
           if (Program.W.IsReady() && ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Name == "BlindMonkWOne")
           {
               InventorySlot invSlot = getBestWardItem();
               Items.UseItem((int)invSlot.Id, position );
               foreach (Obj_AI_Minion ward in ObjectManager.Get<Obj_AI_Minion>().Where(ward =>
           ward.Name.ToLower().Contains("ward") && ward.Distance(Game.CursorPos) < 250))
               {
                   if (ward != null)
                   {
                       Program.W.CastOnUnit(ward);
                       Program.W.Cast();
                       return true;

                   }

               }
           }
           return false;
       }
//------------------------------------------------JUMP--------------------------------------------------------------------------------
       public static int LastPlaced = new int();
       public static Vector3 wardPosition = new Vector3();
       public static int SecondWTime = new int();
       static bool jumped;
       public static bool Newjump()
       {
                          Player.IssueOrder(GameObjectOrder.MoveTo, Player.Position.Extend(Game.CursorPos,150));

                          if (Program.W.IsReady()&&ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Name=="BlindMonkWOne")
                          {
                              wardPosition = Game.CursorPos;
                              Obj_AI_Minion Wards;
                              if (Game.CursorPos.Distance(Program.Player.Position) <= 700)
                              {
                                  Wards = ObjectManager.Get<Obj_AI_Minion>().Where(ward => ward.Distance(Game.CursorPos) < 150 && !ward.IsDead).FirstOrDefault();
                              }
                              else
                              {
                                  Vector3 cursorPos = Game.CursorPos;
                                  Vector3 myPos = Player.ServerPosition;
                                  Vector3 delta = cursorPos - myPos;
                                  delta.Normalize();
                                  wardPosition = myPos + delta * (600 - 5);
                                  Wards = ObjectManager.Get<Obj_AI_Minion>().Where(ward => ward.Distance(wardPosition) < 150 && !ward.IsDead).FirstOrDefault();
                              }
                      if(Wards== null)
                      {
                          if (jumped==false)
                          if (!wardPosition.IsWall()) { 
                                  InventorySlot invSlot = Items.GetWardSlot();
                                  Items.UseItem((int)invSlot.Id, wardPosition);
                                  jumped = true;
                      }
                      }
                              
                              else
                                  if (Program.W.CastOnUnit(Wards))
                                  {
                                      jumped = false;
                                  }
                          }
           
           return false;
       }
       public static Vector3 wardpos;
       public static bool wardj;
       public static Vector3 InsecposN2(Obj_AI_Hero ts)
       {
           return Game.CursorPos.Extend(ts.Position, Game.CursorPos.Distance(ts.Position) - 125);
       }
       public static bool JumpToFlash(Vector3 position)
       {
           if (wardj == false)
           {
               wardpos = position;
               wardj = true;
           }
           if (Program.W.IsReady() && ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Name == "BlindMonkWOne")
           {
               var Wards = ObjectManager.Get<Obj_AI_Minion>().Where(ward => ward.Distance(wardpos) < 150 && !ward.IsDead).FirstOrDefault();
               if (Wards == null)
               {
                   InventorySlot invSlot = Items.GetWardSlot();
                   Items.UseItem((int)invSlot.Id, wardpos);
               }
               Wards = ObjectManager.Get<Obj_AI_Minion>().Where(ward => ward.Distance(wardpos) < 150 && !ward.IsDead).FirstOrDefault();
               if (Program.W.CastOnUnit(Wards))
               {
                   wardj = false;
               }
           }

           return false;
       }

       public static bool JumpTo(Vector3 position)
       {
           if(wardj==false)
           {
               wardpos = position;
               wardj = true;
           }
           if (Program.W.IsReady() && ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Name == "BlindMonkWOne")
           {
               var Wards = ObjectManager.Get<Obj_AI_Minion>().Where(ward => ward.Distance(wardpos) < 150 && !ward.IsDead).FirstOrDefault();
                     if (Wards == null)
                     {
                         InventorySlot invSlot = Items.GetWardSlot();
                         Items.UseItem((int)invSlot.Id, wardpos);
                     }
                     Wards = ObjectManager.Get<Obj_AI_Minion>().Where(ward => ward.Distance(wardpos) < 150 && !ward.IsDead).FirstOrDefault();
               if (Program.W.CastOnUnit(Wards))
               {
                   NormalInsec.Steps = NormalInsec.steps.R;
                   wardj = false;
               }
           }

           return false;
       }
       public static void Wcast(Obj_AI_Base ward)
       {
           if (Program.W.CastOnUnit(ward))
           {
               MasterOfInsec.NormalInsec.Steps = MasterOfInsec.NormalInsec.steps.R;
           }
       }

       public static bool inDistance(Vector2 pos1, Vector2 pos2, float distance)
       {
           float dist2 = Vector2.DistanceSquared(pos1, pos2);
           return (dist2 <= distance * distance) ? true : false;
       }
       public static Obj_AI_Turret getNearTower(Obj_AI_Hero ts)
       {
           return  ObjectManager.Get<Obj_AI_Turret>().Where(tur => tur.IsAlly && tur.Health > 0 && !tur.IsMe).OrderBy(tur => tur.Distance(Player.ServerPosition)).First();
       }
       public static Vector3 Insecpos(Obj_AI_Hero ts)
       {
           return Game.CursorPos.Extend(ts.Position, Game.CursorPos.Distance(ts.Position) + 250);
       }
       public static Vector3 InsecposTower(Obj_AI_Hero target)
       {
           Obj_AI_Turret turret = ObjectManager.Get<Obj_AI_Turret>().Where(tur => tur.IsAlly && tur.Health > 0 && !tur.IsMe).OrderBy(tur => tur.Distance(Player.ServerPosition)).Where(tur => tur.Distance(target.Position) <= 1500).First();
           return target.Position + Vector3.Normalize(turret.Position - target.Position) + 100;

       }
       public static Obj_AI_Hero InsecgetAlly(Obj_AI_Hero target)
       {

           return HeroManager.Allies
                    .FindAll(hero => hero.Distance(Game.CursorPos, true) < 40000) // 200 * 200
                    .OrderBy(h => h.Distance(Game.CursorPos, true)).FirstOrDefault();
       }
       public static Vector3 InsecposToAlly(Obj_AI_Hero target,Obj_AI_Hero ally)
       {
           return ally.Position.Extend(target.Position ,ally.Position.Distance(target.Position)+250);
       }
       public static Vector3 getward(Obj_AI_Hero target)
       {
           Obj_AI_Turret turret = ObjectManager.Get<Obj_AI_Turret>().Where(tur => tur.IsAlly && tur.Health > 0 && !tur.IsMe).OrderBy(tur => tur.Distance(Player.ServerPosition)).First();
           return target.ServerPosition + Vector3.Normalize(turret.ServerPosition - target.ServerPosition) * (-300);
       }
       public static bool putWard(Vector2 pos)
       {
           InventorySlot invSlot = getBestWardItem();
           Items.UseItem((int)invSlot.Id, pos);
           return true;
       }
       public static void moveTo(Vector2 Pos)
       {
           Player.IssueOrder(GameObjectOrder.MoveTo, Pos.To3D());
       }


    }
}
