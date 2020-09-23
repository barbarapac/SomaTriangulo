using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TesteCandidatoTriangulo
{
    public class Triangulo
    {
        /// <summary>
        ///    6
        ///   3 5
        ///  9 7 1
        /// 4 6 8 4
        /// Um elemento somente pode ser somado com um dos dois elementos da próxima linha. Como o elemento 5 na Linha 2 pode ser somado com 7 e 1, mas não com o 9.
        /// Neste triangulo o total máximo é 6 + 5 + 7 + 8 = 26
        /// 
        /// Seu código deverá receber uma matriz (multidimensional) como entrada. O triângulo acima seria: [[6],[3,5],[9,7,1],[4,6,8,4]]
        /// </summary>
        /// <param name="dadosTriangulo"></param>
        /// <returns>Retorna o resultado do calculo conforme regra acima</returns>
        public int ResultadoTriangulo(string dadosTriangulo)
        {
            int aux = 0, total = 0;

            // Criação do padrão de string para aplicar o regex.
            var padrao = ("\\[(\\d+)\\]|\\[(\\d+),(\\d+)\\]|\\[(\\d+),(\\d+)\\,(\\d+)\\]|\\[(\\d+),(\\d+)\\,(\\d+)\\,(\\d+)\\]");
            MatchCollection matches = Regex.Matches(dadosTriangulo, padrao);

            // Percorro minha lista de string, que foi geradas a partir do match.
            int[][] matrix = matches.OfType<Match>()
                           .Select(m =>
                           {
                               // Trato os dados para conversão.
                               var g = m.Value.Replace("[", "").Replace("]", "").Split(',').ToList();

                               // Crio um e alimento uma lista de inteiros com os dados tratados.
                               List<int> i = new List<int>();
                               foreach (var item in g)
                               {
                                   i.Add(int.Parse(item.ToString()));
                               }

                               // Retorno lista criada para a matrix.
                               return i.ToArray();
                           })
                           .ToArray();

            // Percorro a matrix
            for (int i = 0; i < matrix.Length; i++)
            {
                // Verifico se o tamanho da linha da matriz é maior que 1, que indica que essa é a primeira linha.
                if (matrix[i].Length > 1)
                {
                    int maior = 0;

                    // Se sim, percorro as colunas buscando o maior valor.
                    for (int j = aux; j < matrix[i].Length; j++)
                    {
                        // Maior for igual a zero, significa que estamos na primeira coluna a ser percorrida.
                        if (maior == 0)
                            maior = matrix[i][j];
                        else
                        {
                            // Se não for a primeira coluna, verifico se o valor da coluna atual é menor que o maior valor anterior.
                            if (maior < matrix[i][j])
                            {
                                // Alimento variavel de controle maior e seto indice de sua coluna.
                                maior = matrix[i][j];
                                aux = j;
                            }
                        }
                    }

                    // Soma o maior valor encontrado com o total armazenado
                    total += maior;
                }
                else
                    // Somente se for a primeira linha.
                    total += matrix[i][0];
            }

            return total;
        }
    }
}