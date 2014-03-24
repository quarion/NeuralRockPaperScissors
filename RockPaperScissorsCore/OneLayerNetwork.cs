using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsCore
{
    /// <summary>
    /// Podstawowy model prostej sieci neuronowej
    /// </summary>
    class OneLayerNetwork
    {
        public OneLayerNetwork(int inputsNumber, int outputsNumber)
        {
            inputs = new double[inputsNumber];
            outputs = new double[outputsNumber];
            weights = new double[outputsNumber, inputsNumber];
        }

        protected double[] inputs;
        protected double[] outputs;
        protected double[,] weights;

        /// <summary>
        /// Generujemly losowe wagi
        /// </summary>
        public void resetWeights(double scale)
        {
            Random rnd = new Random();

            for (int i = 0; i < outputs.Length; ++i)
            {
                for (int j = 0; j < inputs.Length; ++j)
                    weights[i, j] = rnd.NextDouble()*scale;
            }

        }

        /// <summary>
        /// Wyliczamy pobudzenie neuronów wyjściowych
        /// </summary>
        protected void updateOutput()
        {
            for (int i = 0; i < outputs.Length; ++i )
            {
                outputs[i] = 0.0;
                for (int j = 0; j < inputs.Length; ++j)
                    outputs[i] += weights[i, j] * inputs[j];
            }
        }


        /// <summary>
        /// Generujemy nowe pobudzenie neurownó wywjsciowych na podstawie nowego pobudzenia neronów wejsciowych
        /// </summary>
        /// <param name="newInputs">nowa wartosc pobudzenia neuronów wejsciowych</param>
        /// <returns></returns>
        public double[] getResult(double[] newInputs)
        {
            if (newInputs.Length != inputs.Length)
                return null;

            inputs = newInputs;
            updateOutput();
            return outputs;
        }


    }
}
