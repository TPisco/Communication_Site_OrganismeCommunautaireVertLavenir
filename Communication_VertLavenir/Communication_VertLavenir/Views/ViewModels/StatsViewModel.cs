using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication_VertLavenir.Models;

namespace Communication_VertLavenir.Views.ViewModels
{
    public class StatsViewModel
    {
        public int TotalUtilisateurs { get; set; }
        public int TotalPaiements { get; set; }
        public int TotalEvenements { get; set; }
        public int TotalMontantRecolte { get; set; }

        public Dictionary<role, int> UtilisateursParRole { get; set; } = new();
        public Dictionary<typeEvent, int> EvenementsParType { get; set; } = new();
    }
}
