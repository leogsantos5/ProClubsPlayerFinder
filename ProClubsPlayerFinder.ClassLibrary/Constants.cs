using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProClubsPlayerFinder.ClassLibrary
{
    public class Constants
    {
        public static List<string> EuropeanCountries { get; } = new List<string>
        {
            "Albania", "Andorra", "Austria", "Belarus", "Belgium", "Bosnia and Herzegovina", "Bulgaria", "Croatia",
            "Cyprus", "Czech Republic", "Denmark", "Estonia", "Finland", "France", "Germany", "Greece", "Hungary",
            "Iceland", "Ireland", "Italy", "Kosovo", "Latvia", "Liechtenstein", "Lithuania", "Luxembourg", "Malta",
            "Moldova", "Monaco", "Montenegro", "Netherlands", "North Macedonia", "Norway", "Poland", "Portugal",
            "Romania", "Russia", "San Marino", "Serbia", "Slovakia", "Slovenia", "Spain", "Sweden", "Switzerland",
            "Ukraine", "United Kingdom", "Vatican City"
        };

        public static List<string> Consoles { get; } = new List<string> { "PS5", "XBox", "PC"};
    }

    public class Roles
    {
        public static string FreeAgent { get; } = "Free Agent";
        public static string ClubOwner { get; } = "Club Owner";
        public static string Player { get; } = "Player";

        public static string RoleDictKey { get; } = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    }
}
