using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsCore
{
    /// <summary>
    /// Klasa podejmująca decyzje losowo
    /// </summary>
    public class RandomDecisionMaker:IDecisionMaker
    {
        Random rnd = new Random();

        public Decision getNextDecision()
        {
            return (Decision)(rnd.Next(3) + 1);
        }

        public void rememberDecision(Decision d){}
    }
}
