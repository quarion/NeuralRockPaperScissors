using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsCore
{
    /// <summary>
    /// Mozliwe decyzje
    /// </summary>
    public enum Decision{ Rock = 1, Paper = 2, Scissor = 3 };

    /// <summary>
    /// Interfejs klasy podejmującej decyzje w naszej grze
    /// </summary>
    public interface IDecisionMaker
    {
        /// <summary>
        /// Pobiera nową decyzję AI
        /// </summary>
        /// <returns></returns>
        Decision getNextDecision();

        /// <summary>
        /// Zapamietuje decyzję przeciwnika
        /// </summary>
        /// <param name="opponentDecision"></param>
        void rememberDecision(Decision opponentDecision);
    }
}
