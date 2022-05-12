using System;
using System.Collections.Generic;
using System.Text;

namespace Nozarasius.Roles
{
    class Madmate
    {
        public static List<byte> CheckedImpostor;
        public static bool CheckImpostor(PlayerControl p)
        {
            if (!NozModRoleBehavior.MadMate.IsImpostorCheck) return false;
            if (!p.isRole(RoleId.MadMate)) return false;
            if (CheckedImpostor.Contains(p.PlayerId)) return true;
            return false;
        }
    }
}
