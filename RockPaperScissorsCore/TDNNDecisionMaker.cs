using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsCore
{
    /// <summary>
    /// Klasa podejmująca decyzje w grze korzystając z sieci neuronowej
    /// </summary>
   public class TDNNDecisionMaker:IDecisionMaker
    {
        public Decision getNextDecision()
        {
            myLastDecision = network.getDecision();
            return myLastDecision;
        }

        RPSNetwork network = new RPSNetwork( Settigns.samplesHistoryLength );
        Decision myLastDecision;

        public void rememberDecision(Decision opponentDecision)
        {
            network.updateWeights(myLastDecision, opponentDecision);
            network.updateInputs(myLastDecision, opponentDecision);
        }
    }
}
