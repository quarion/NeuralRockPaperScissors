using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsCore
{
    /// <summary>
    /// Klasa z Extensions dla naszego Enuma Decision. Elegancko i wygodnie mieszamy sobie pojęcie klasy i enuma
    /// </summary>
    public static class RPSExtensions
    {
            /// <summary>
            /// Sprawdza, czy decyzja jest zwycieska
            /// </summary>
            /// <param name="mine"></param>
            /// <param name="opponet"></param>
            /// <returns></returns>
            public static int Score(this Decision mine, Decision opponet)
            {
                if (mine == opponet)
                    return 0;

                switch (mine)
                {
                    case Decision.Rock:
                        if (opponet == Decision.Paper)
                            return -1;
                        else return 1;
                    case Decision.Paper:
                        if (opponet == Decision.Scissor)
                            return -1;
                        else
                            return 1;
                    case Decision.Scissor:
                        if (opponet == Decision.Rock)
                            return -1;
                        else
                            return 1;
                    default:
                        return 0;
                }
            }

        /// <summary>
        /// Kowenrtuje decyzję na odpowiadajace jej pobudzenie sieci neuronowej
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
            public static double[] ToNeural(this Decision d)
            {
                return new double[3] { Convert.ToDouble(d == Decision.Rock), Convert.ToDouble(d == Decision.Paper), Convert.ToDouble(d == Decision.Scissor) };
            }

        /// <summary>
        /// Zwracamy kontre dla danego zagrania
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
            public static Decision GetCounter(this Decision d)
            {
                if (d == Decision.Scissor)
                    return Decision.Rock;
                else
                    return d + 1;
            }

            public static string toLocalString(this Decision d)
            {
                switch (d)
                {
                    case Decision.Rock: return "Kamień";
                    case Decision.Scissor: return "Nożyce";
                    default: return "Papier";
                }
            }
    }
}
