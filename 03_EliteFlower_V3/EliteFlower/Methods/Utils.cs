using EliteFlower.Classes;
using EliteFlower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace EliteFlower.Methods
{
    public static class Utils
    {
        readonly static List<string> serialST1 = new List<string> { "COM21", "COM31" };
        readonly static List<string> serialST2 = new List<string> { "COM22", "COM32" };
        readonly static List<string> serialST3 = new List<string> { "COM23", "COM33" };
        readonly static List<string> serialADN = new List<string> { "COM24", "COM34" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromSource"></param>
        /// <param name="toSource"></param>
        /// <param name="fromTarget"></param>
        /// <param name="toTarget"></param>
        /// <returns></returns>
        public static double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSerials()
        {
            List<string> serials = new List<string>();
            serials.AddRange(serialST1);
            serials.AddRange(serialST2);
            serials.AddRange(serialST3);
            serials.AddRange(serialADN);
            return serials;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static List<string> GetStageSerials(int idx)
        {
            List<string> serials;
            switch (idx)
            {
                case 1: serials = serialST1; break;
                case 2: serials = serialST2; break;
                case 3: serials = serialST3; break;
                case 4: serials = serialADN; break;
                default: serials = new List<string>() { "NN", "NN" }; break;
            }
            return serials;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIPs()
        {
            return new List<string>() { "169.254.1.236", "169.254.1.238", "169.254.1.200", "169.254.1.210" }; // { PLC1, PLC2, Escaner_banda1, Escaner_banda2 }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<bool> GetIPConnected()
        {
            List<string> ipList = GetIPs();
            List<bool> connected = new List<bool>();
            Ping x = new Ping();
            foreach (string item in ipList)
            {
                PingReply reply = x.Send(IPAddress.Parse(item));
                if (reply.Status == IPStatus.Success)
                    connected.Add(true);
                else
                    connected.Add(false);
            }
            return connected;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registers"></param>
        /// <param name="english"></param>
        /// <param name="countVases"></param>
        public static string MessageStatistics(int registers, bool english, List<VaseCount> countVases, List<IDAddOn> addOn)
        {
            int total = countVases.Select(s => s.Count).Sum();

            string msg1 = $"{string.Format(UIMessages.EliteFlower(12, english), registers)}\n";
            msg1 += $"{string.Format(UIMessages.EliteFlower(55, english), total)}\n";
            msg1 += $"Total of registers with addon: {addOn.Select(s => s.Count).ToList().Sum()}\n\n";

            foreach (VaseCount vase in countVases)
                msg1 += $"{string.Format(UIMessages.EliteFlower(19, english), vase.Vase, vase.Count)}\n";
            msg1 += "\n";
            foreach (IDAddOn item in addOn)
                msg1 += $"addon: {item.AddOn} - Quantity: {item.Count}\n";

            int numVases = countVases.Count;
            string msgShow = $"{msg1}\n{string.Format(UIMessages.EliteFlower(20, english), numVases)}\n";
            msgShow += $"The number of addon are {addOn.Select(s => s.Count).ToList().Count}";
            if (numVases <= 3)
                msgShow += $"\n{UIMessages.EliteFlower(21, english)}";
            else if (numVases > 3 && numVases <= 6)
                msgShow += $"\n{UIMessages.EliteFlower(22, english)}";
            else if (numVases > 6 && numVases <= 9)
                msgShow += $"\n{UIMessages.EliteFlower(23, english)}";
            else
                msgShow += $"\n{UIMessages.EliteFlower(24, english)}";

            return msgShow;
        }
        /// <summary>
        /// Convierte los datos de una lista a otra
        /// </summary>
        /// <param name="stages">Lista a convertir</param>
        /// <returns>Devuelve datos convertidos</returns>
        public static List<BalanceName> ConvertBalance(List<Stage> stages)
        {
            List<BalanceName> balances = new List<BalanceName>();
            for (int i = 0; i < 3; i++)
            {
                float valueTotal = stages[i * 3].Quantity + stages[i * 3 + 1].Quantity + stages[i * 3 + 2].Quantity;
                balances.Add(new BalanceName { Stage = i + 1, Count = valueTotal, ID1 = $"{stages[i * 3].Quantity}", ID2 = $"{stages[i * 3 + 1].Quantity}", ID3 = $"{stages[i * 3 + 2].Quantity}" });
            }
            return balances;
        }

        public static List<int> FillList(int sizeL, int value)
        {
            List<int> L = new List<int>();
            for (int i = 0; i < sizeL; i++) L.Add(value);
            return L;
        }

        public static bool IsInRange(float value, float valueAVG, double fraction)
        {
            return (value < valueAVG * (1 - fraction)) || (value > valueAVG * (1 + fraction));
        }

        /// <summary>
        /// Crea una lista de las 3 estaciones con los valores que se asigna a cada uno.
        /// </summary>
        /// <param name="valueStages">Valores a asignar</param>
        /// <param name="true_indexes">Estaciones habilitadas</param>
        /// <returns>Devuelve la lista de los valores para todas las estaciones</returns>
        public static List<float> GetQuantities(List<float> valueStages, List<int> true_indexes)
        {
            List<float> balanceQuantities = new List<float>();
            int jj = 0;
            for (int i = 0; i < 3; i++)
            {
                if (true_indexes.Contains(i))
                {
                    balanceQuantities.Add(valueStages[jj * 3]);
                    balanceQuantities.Add(valueStages[jj * 3 + 1]);
                    balanceQuantities.Add(valueStages[jj++ * 3 + 2]);
                }
                else
                {
                    balanceQuantities.Add(0);
                    balanceQuantities.Add(0);
                    balanceQuantities.Add(0);
                    jj++;
                }
            }
            return balanceQuantities;
        }
        /// <summary>
        /// Revisa si efectua el cambio o no si el valor que tiene actual es direfente al que se encuentra en el balanceo.
        /// </summary>
        /// <param name="value">El ID del producto</param>
        /// <param name="nStage">El numero de la estacion</param>
        /// <param name="nElevator">El numero del elevador</param>
        /// <returns>Retorna un booleano segun si fue exitoso el chequeo</returns>
        public static bool manualCheck(string value, int nStage, int nElevator)
        {
            try
            {
                List<BalanceName> balance = Mongoose.GetDataBalanceNames("Actual");
                if (nElevator == 1)
                {
                    if (balance.Count == 0 || balance[nStage].ID1 != value)
                    {
                        return true;
                    }
                }
                if (nElevator == 2)
                {
                    if (balance.Count == 0 || balance[nStage].ID2 != value)
                    {
                        return true;
                    }
                }
                if (nElevator == 3)
                {
                    if (balance.Count == 0 || balance[nStage].ID3 != value)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="balanceQuantities"></param>
        /// <param name="true_indexes"></param>
        /// <returns></returns>
        public static List<float> GetValuesBalanced(List<BalanceCount> balanceQuantities, List<int> true_indexes)
        {
            List<float> tt1 = balanceQuantities.Select(s => s.ID1).ToList();
            List<float> tt2 = balanceQuantities.Select(s => s.ID2).ToList();
            List<float> tt3 = balanceQuantities.Select(s => s.ID3).ToList();

            List<float> values = new List<float>();
            int jj = 0;
            //MessageBox.Show("index lenght" + true_indexes.Count.ToString());
            int stages = true_indexes.Count;
            if (stages > 3)
            {
                stages = 3;
            }
            for (int i = 0; i < 3; i++)
            {
                if (true_indexes.Contains(i))
                {
                    values.Add(tt1[jj]);
                    values.Add(tt2[jj]);
                    values.Add(tt3[jj++]);
                }
                else
                {
                    values.Add(0);
                    values.Add(0);
                    values.Add(0);
                }
            }
            return values;
        }
        /// <summary>
        /// Setea valores a una lista de textboxs.
        /// </summary>
        /// <param name="values">Lista con valores</param>
        /// <param name="textboxs">Lista con textboxs</param>
        public static void SetValuesTxt(List<float> values, List<TextBox> textboxs)
        {
            int ii = 0;
            foreach (TextBox item in textboxs)
                item.Text = $"{values[ii++]}";
        }

        /// <summary>
        /// Obtiene una lista de flotantes con la cantidad por producto que tiene cada estacion.
        /// </summary>
        /// <param name="balanceQuantities">Lista de objetos con cantidades de cada estacion con el trabajo balanceado</param>
        /// <param name="true_indexes">Lista de enteros con los indices de las estaciones activas</param>
        /// <returns>Retorna una lista de flotantes de las cantidades que tiene cada uno de los 9 elevadores</returns>
        public static List<float> FillQuantity(List<BalanceCount> balanceQuantities, List<int> true_indexes)
        {
            List<float> quantityIDS = new List<float>();
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                if (true_indexes.Contains(i) == true)
                {
                    quantityIDS.Add(balanceQuantities[j].ID1);
                    quantityIDS.Add(balanceQuantities[j].ID2);
                    quantityIDS.Add(balanceQuantities[j++].ID3);
                }
                else
                {
                    quantityIDS.Add(0);
                    quantityIDS.Add(0);
                    quantityIDS.Add(0);
                }
            }
            return quantityIDS;
        }
        /// <summary>
        /// Obtiene una lista de objetos con la informacion resumida de cada una de las estaciones.
        /// </summary>
        /// <param name="comboStages">Lista de listas de ComboBox</param>
        /// <param name="quantityIDS">Lista de flotantes con las cantidades que tiene cada elevador</param>
        /// <returns>Retorna una lista de objetos</returns>
        public static List<Stage> FillSelected(List<List<ComboBox>> comboStages, List<float> quantityIDS)
        {
            List<Stage> idSelected = new List<Stage>();
            int jj = 0;
            int ii = 1;
            foreach (var stage in comboStages)
            {
                foreach (var item in stage)
                {
                    idSelected.Add(
                        new Stage()
                        {
                            StageN = ii,
                            ID = (string)item.SelectedItem,
                            Check = (string)item.SelectedItem != "NV",
                            Quantity = quantityIDS[jj++]
                        }
                    );
                }
                ii += 1;
            }
            return idSelected;
        }
        /// <summary>
        /// Setea un componente de ComboBox con una lista de cadenas y la inicializa en un indice.
        /// </summary>
        /// <param name="nameVS">Lista de cadena con los valores</param>
        /// <param name="workers">Lista de comboBox al cual se le va aplicar</param>
        public static void SetComboBox(List<string> nameVS, List<ComboBox> workers)
        {
            foreach (ComboBox worker in workers)
            {
                worker.Items.Clear();
                foreach (string item in nameVS)
                    worker.Items.Add(item);
                worker.SelectedIndex = worker.Items.Count - 1;
            }
        }
    }
}
