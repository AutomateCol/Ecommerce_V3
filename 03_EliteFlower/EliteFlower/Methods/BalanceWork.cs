using System;
using System.Linq;

namespace EliteFlower.Methods
{
    public static class BalanceWork
    {
        /// <summary>
        /// Realiza el cambio de unas posiciones en la matriz.
        /// </summary>
        /// <param name="matrix">La matriz flotante que se le van hacer los cambios</param>
        /// <param name="pos1">Las posiciones de una matriz de enteros que van a ser cambiadas</param>
        /// <returns>Retorna una matrix de flotantes con las posiciones cambiadas</returns>
        public static float[][] changef(float[][] matrix, int[][] pos1)
        {
            float var1 = matrix[pos1[0][0]][pos1[0][1]];
            matrix[pos1[0][0]][pos1[0][1]] = matrix[pos1[1][0]][pos1[1][1]];
            matrix[pos1[1][0]][pos1[1][1]] = var1;
            return matrix;
        }
        /// <summary>
        /// Realiza el cambio de unas posiciones en la matriz.
        /// </summary>
        /// <param name="matrix">La matriz de enteros que se le van hacer los cambios</param>
        /// <param name="pos1">Las posiciones de una matriz de enteros que van a ser cambiadas</param>
        /// <returns>Retorna una matrix de enteros con las posiciones cambiadas</returns>
        public static int[][] changei(int[][] matrix, int[][] pos1)
        {
            int var1 = matrix[pos1[0][0]][pos1[0][1]];
            matrix[pos1[0][0]][pos1[0][1]] = matrix[pos1[1][0]][pos1[1][1]];
            matrix[pos1[1][0]][pos1[1][1]] = var1;
            return matrix;
        }
        /// <summary>
        /// Realiza el cambio de unas posiciones en la matriz.
        /// </summary>
        /// <param name="matrix">La matriz de enteros que se le van hacer los cambios</param>
        /// <param name="places">La cantidad de estaciones activas</param>
        /// <returns>Retorna una matrix de enteros con las posiciones cambiadas</returns>
        public static int[][] changeb(float[][] matrix, int places)
        {
            int[][] pos2 = new int[2][];
            int[][] pos1 = new int[2][];
            float[][] matrix2 = new float[places][];
            int[] argsort = new int[places], argsort2 = new int[places];
            float diff, diff2;
            for (int i = 0; i < places; i++)
            {
                matrix2[i] = new float[3];
            }
            for (int i = 0; i < 2; i++)
            {
                pos1[i] = new int[2];
                pos2[i] = new int[2];
            }
            pos1[0][0] = 0;
            pos1[0][1] = 0;
            pos1[1][0] = 0;
            pos1[1][1] = 0;

            argsort = sortmatrix(matrix, places);
            diff = Math.Abs(matrix[argsort[places - 1]].Sum() - matrix[argsort[0]].Sum());
            for (int k = 0; k < places - 1; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        pos2[0][0] = argsort[places - 1];
                        pos2[0][1] = i;
                        pos2[1][0] = argsort[k];
                        pos2[1][1] = j;
                        matrix2 = matrix;
                        matrix2 = changef(matrix2, pos2);
                        argsort2 = sortmatrix(matrix2, places);
                        diff2 = Math.Abs(matrix2[argsort2[places - 1]].Sum() - matrix2[argsort2[0]].Sum());
                        if (diff2 < diff)
                        {
                            diff = diff2;
                            pos2[0].CopyTo(pos1[0], 0);
                            pos2[1].CopyTo(pos1[1], 0);
                        }
                        matrix2 = changef(matrix2, pos2);
                    }
                }
            }
            return pos1;
        }
        /// <summary>
        /// Entrega una lista ordenada de enteros.
        /// </summary>
        /// <param name="matrix">La matriz de enteros que se le van hacer los cambios</param>
        /// <param name="places">La cantidad de estaciones activas</param>
        /// <returns>Una lista de enteros ordena descendentemente</returns>
        public static int[] sortmatrix(float[][] matrix, int places)
        {
            int[] ksort = new int[places];
            float[] ksortf = new float[places];
            if (places == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    ksortf[i] = matrix[i].Sum();
                }
                if (ksortf[0] > ksortf[1] & ksortf[0] > ksortf[2])
                {
                    ksort[2] = 0;
                    if (ksortf[1] > ksortf[2])
                    {
                        ksort[1] = 1;
                        ksort[0] = 2;
                    }
                    else
                    {
                        ksort[1] = 2;
                        ksort[0] = 1;
                    }
                }
                else
                {
                    if (ksortf[1] > ksortf[2] & ksortf[1] > ksortf[0])
                    {
                        ksort[2] = 1;
                        if (ksortf[2] > ksortf[0])
                        {
                            ksort[1] = 2;
                            ksort[0] = 0;
                        }
                        else
                        {
                            ksort[0] = 2;
                            ksort[1] = 0;
                        }
                    }
                    else
                    {
                        ksort[2] = 2;
                        if (ksortf[1] > ksortf[0])
                        {
                            ksort[1] = 1;
                            ksort[0] = 0;
                        }
                        else
                        {
                            ksort[0] = 1;
                            ksort[1] = 0;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < places; i++)
                {
                    ksortf[i] = matrix[i].Sum();
                }
                if (ksortf[0] > ksortf[1])
                {
                    ksort[0] = 1;
                    ksort[1] = 0;
                }
                else
                {
                    ksort[1] = 0;
                    ksort[1] = 1;
                }
            }
            return ksort;
        }

    }
}
