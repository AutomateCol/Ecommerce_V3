using ClosedXML.Excel;
using EliteFlower.Classes;
using EliteFlower.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using MongoDB.Bson;



namespace EliteFlower.Methods
{
    public static class Mongoose
    {
        private static readonly string mongoDBConnection = "mongodb://localhost:27017/waitQueueSize=5000";

        private static readonly List<string> filterVases = new List<string> { "", " ", "0" };

        public static string GetMongoDBConnection()
        {
            return mongoDBConnection;
        }
        static MongoClient cliente;
        static IMongoDatabase db;

        public static MongoClient GetDBConnection()
        {
            try
            {
                cliente = new MongoClient(mongoDBConnection);


                //MessageBox.Show("se ha conectado con la base de datos " + database.ToString());
            }
            catch (MongoClientException e)
            {
                MessageBox.Show("no se ha logrado conectar a la base de datos" + e.ToString());
            }
            return cliente;

        }

        public static IMongoDatabase GetDataBase(MongoClient client)
        {
            try
            {
                db = client.GetDatabase("EliteFlower");
                //MessageBox.Show("se ha conectado con la base de datos " + db.ToString());
            }
            catch (MongoClientException e)
            {
                MessageBox.Show("error al cargar base de datos" + e.ToString());
            }
            return db;
        }


        public static void SetLastPosZone2(List<int> pos)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                if (pos[0] != -1)
                {
                    tt[0].lastPosSorter = pos[0];
                }

                if (pos[1] != -1)
                {
                    tt[0].lastPosEmpujador = pos[1];
                }
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }

        public static bool GetESActive()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                return tt[0].ESActive;
            }
            return false;
        }

        public static void SetESActiveTrue()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                tt[0].ESActive = true;
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }

        public static void SetESActiveFalse()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                tt[0].ESActive = false;
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }

        public static bool GetWorkMesanin()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                return tt[0].workMesanin;
            }
            return false;
        }

        public static void SetWorkMesaninTrue()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                tt[0].workMesanin = true;
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }



        public static void SetWorkMesaninFalse()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                tt[0].workMesanin = false;
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }

        public static bool GetWorkUp()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                return tt[0].workUp;
            }
            return false;
        }

        public static void SetWorkUpTrue()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                tt[0].workUp = true;
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }

        public static void SetWorkUpFalse()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                tt[0].workUp = false;
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }

        public static string[] GetLastReferences()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<MLlenado> MReferenceDB = database.GetCollection<MLlenado>("BWorkMesanin");

            List<MLlenado> tt = MReferenceDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                return tt[0].References;
            }
            return new string[] { "NV", "NV", "NV", "NV", "NV", "NV", "NV", "NV", "NV" };
        }

        public static bool GetInitialFeed()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                return tt[0].initialFeed;
            }
            return false;
        }

        public static void SetInitialFeedTrue()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                tt[0].initialFeed = true;
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }

        public static void SetInitialFeedFalse()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            if (tt.Count > 0)
            {
                tt[0].initialFeed = false;
                OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
            }
        }

        public static string ShowImageMesanin(string name)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

            List<Product> lst = ProductDB.Find(d => d.ID == name).ToList();
            if (lst.Count > 0)
            {
                return lst[0].Url;
            }
            {
                return "ND";
            }
        }

        public static void UnbalanceData()
        {
            MongoClient client = GetDBConnection();
            IMongoDatabase database = GetDataBase(client);

            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>("Data");


            List<DataProduct> dataproduct = DataProductDB.Find(f => f.TrackingNumber != "").ToList();


            //MessageBox.Show("se ejecuta el bucle for each");
            foreach (DataProduct item in dataproduct)
            {
                item.WhStage = -1;
                item.Balance = -1;
                item.BalanceUUID = "";
                DataProductDB.ReplaceOneAsync(f => f.TrackingNumber == item.TrackingNumber, item);

            }
            database.GetCollection<BalanceName>("BWorkName").DeleteManyAsync(a => a._Id <= 4);
            database.GetCollection<BalanceName>("BWorkQuantity").DeleteManyAsync(a => a._Id <= 4);



        }

        //Se copia el registro de la coleccion data una vez se haya realizada la lectura del vase, la copia se relaiza en la coleccion DataSummary
        public static void UpdateReadedData()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataDB = database.GetCollection<DataProduct>("Data");
            IMongoCollection<DataProduct> DataSummaryDB = database.GetCollection<DataProduct>("DataSummary");
            List<DataProduct> datareaded = DataDB.Find(w => w.Readed || w.ReadedAddon).ToList();
            List<string> datareadedindexs = datareaded.Select(s => s.TrackingNumber).ToList();
            List<DataProduct> datasummary = DataSummaryDB.Find(f => f.OrderNumber != "").ToList();
            List<string> datasummaryindexs = datasummary.Select(s => s.TrackingNumber).ToList();
            List<string> datadifferenceindexs = datareadedindexs.Except(datasummaryindexs).ToList();
            List<DataProduct> datadifference = datareaded.Where(item => datadifferenceindexs.Contains(item.TrackingNumber)).ToList();
            datadifference.ForEach(fe => fe.ReadedDate = DateTime.Now.ToString("MM/dd/yyyy"));
            foreach (DataProduct itemreaded in datadifference)
            {
                DataSummaryDB.InsertOneAsync(itemreaded);
            }
            //DataDB.DeleteMany(s => s.Readed || s.ReadedAddon);
        }

        public static void UpdateEntryData()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataDB = database.GetCollection<DataProduct>("Data");
            IMongoCollection<DataProduct> DataSummaryDB = database.GetCollection<DataProduct>("DataSummary");
            IMongoCollection<DataProduct> DataTunnelDB = database.GetCollection<DataProduct>("DataTunnel");
            List<string> dataindexs = DataDB.Find(f => f.OrderNumber != "").ToList().Select(s => s.TrackingNumber).ToList();
            List<string> datasummaryindexs = DataSummaryDB.Find(f => f.OrderNumber != "").ToList().Select(s => s.TrackingNumber).ToList();
            List<DataProduct> datatunnel = DataTunnelDB.Find(f => f.OrderNumber != "").ToList();
            List<string> datatunnelindexs = datatunnel.Select(s => s.TrackingNumber).ToList();
            List<string> datadifference1indexs = datatunnelindexs.Except(dataindexs).ToList();
            List<string> datadifference2indexs = datadifference1indexs.Except(datasummaryindexs).ToList();
            List<DataProduct> datadifference = datatunnel.Where(item => datadifference2indexs.Contains(item.TrackingNumber)).ToList();
            foreach (DataProduct itemDifferent in datadifference)
            {
                DataDB.InsertOneAsync(itemDifferent);
            }
        }

        public static void BackupEntryData()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataDB = database.GetCollection<DataProduct>("Data");
            IMongoCollection<DataProduct> DataBackupDB = database.GetCollection<DataProduct>("DataBackup");
            List<DataProduct> backup = DataDB.Find(f => f.OrderNumber != "").ToList();
            foreach (DataProduct item in backup)
            {
                DataBackupDB.InsertOneAsync(item);
            }
        }

        public static void BackupEntryDataSummary()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataSummaryDB = database.GetCollection<DataProduct>("DataSummary");
            IMongoCollection<DataProduct> DataSummaryBackupDB = database.GetCollection<DataProduct>("DataSummaryBackup");
            List<DataProduct> backup = DataSummaryDB.Find(f => f.OrderNumber != "").ToList();
            foreach (DataProduct item in backup)
            {
                DataSummaryBackupDB.InsertOneAsync(item);
            }
        }

        /// <summary>
        /// Actualiza el estado actual cuando recorre un ID entre las 3 estaciones.
        /// </summary>
        /// <param name="nStage">Identificador de la estacion</param>
        /// <param name="request">ID del cual va hacer el descuento</param>
        /// <returns>Retorna un bandera si lo puede hacer o no</returns>
        public static bool RefreshStateActual(int nStage, string request)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDStages> IDStagesDB = database.GetCollection<IDStages>("BalanceIDStages");

            List<IDStages> actualState = IDStagesDB.Find(f => f.status == "state").ToList();
            List<string> keyVase = actualState.Select(s => s.Vase).ToList();
            int indVase = keyVase.IndexOf(request);

            if (nStage == 0)
            {
                if (actualState[indVase].Stage3 >= 0 && actualState[indVase].Stage2 >= 0 && actualState[indVase].Stage1 > 0)
                {
                    actualState[indVase].Stage1 -= 1;
                    IDStagesDB.ReplaceOneAsync(r => r._Id == actualState[indVase]._Id, actualState[indVase]);
                    return true;
                }
            }
            if (nStage == 1)
            {
                if (actualState[indVase].Stage3 >= 0 && actualState[indVase].Stage2 > 0 && actualState[indVase].Stage1 == 0)
                {
                    actualState[indVase].Stage2 -= 1;
                    IDStagesDB.ReplaceOneAsync(r => r._Id == actualState[indVase]._Id, actualState[indVase]);
                    return true;
                }
            }
            if (nStage == 2)
            {
                if (actualState[indVase].Stage3 > 0 && actualState[indVase].Stage2 == 0 && actualState[indVase].Stage1 == 0)
                {
                    actualState[indVase].Stage3 -= 1;
                    IDStagesDB.ReplaceOneAsync(r => r._Id == actualState[indVase]._Id, actualState[indVase]);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Actualiza los cambios en las 3 estaciones del ID del producto que se le indica si este a llegado a 0 en todos las estaciones.
        /// </summary>
        /// <param name="request">ID del cual se van actualizar los datos</param>
        public static void RefreshStateVase(string request)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDStages> IDStagesDB = database.GetCollection<IDStages>("BalanceIDStages");
            IMongoCollection<StateStage> StateDB = database.GetCollection<StateStage>("BWorkState");

            List<IDStages> initState = IDStagesDB.Find(f => f.status == "init" && f.Vase == request).ToList();
            List<IDStages> actualState = IDStagesDB.Find(f => f.status == "state").ToList();

            if (initState.Count > 0 && actualState.Count > 0)
            {
                List<string> keyVase = actualState.Select(s => s.Vase).ToList();
                int indVase = keyVase.IndexOf(request);

                List<StateStage> tt = StateDB.Find(f => f.Status == "state").ToList().Where(w => w.ID == request).ToList();
                float tt1 = tt.Where(w => w.StageN == 1).Select(s => s.Quantity).ToList().Sum();
                float tt2 = tt.Where(w => w.StageN == 2).Select(s => s.Quantity).ToList().Sum();
                float tt3 = tt.Where(w => w.StageN == 3).Select(s => s.Quantity).ToList().Sum();

                if (actualState[indVase].Stage3 == 0 && actualState[indVase].Stage2 == 0 && actualState[indVase].Stage1 == 0)
                {
                    actualState[indVase].Stage1 = (tt1 == 0) ? 0 : initState[0].Stage1;
                    actualState[indVase].Stage2 = (tt2 == 0) ? 0 : initState[0].Stage2;
                    actualState[indVase].Stage3 = (tt3 == 0) ? 0 : initState[0].Stage3;
                    IDStagesDB.ReplaceOneAsync(r => r._Id == actualState[indVase]._Id, actualState[indVase]);
                }
            }
        }

        //-----------------------------------
        //              CREATE              |
        //-----------------------------------

        /// <summary>
        /// Almacena el registro de los errores en un documento.
        /// </summary>
        /// <param name="ex">El error de entrada</param>
        public static void LoadError(Exception ex, string sourceMethod, string window)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<LogsEliteFlower> LogFileDB = database.GetCollection<LogsEliteFlower>("ErrorLogs");

            string[] error = ex.GetType().FullName.Split('.');

            LogFileDB.InsertOneAsync(new LogsEliteFlower
            {
                startTime = DateTime.Now,
                sourceMethod = sourceMethod,
                window = window,
                sourceError = error[0],
                errorType = error[error.Length - 1],
                errorCode = ex.HResult,
                verbose = ex.Message.ToString(),
            }
            );
        }
        /// <summary>
        /// Verifica la diferencia que hay entre 2 estaciones que tengan en comun un ID de referencia
        /// y si tienen alguno en comun y estos estan por fuera del promedio un 4% recalcula y compensa
        /// las estaciones.
        /// </summary>
        public static void AddChangeBalance()
        {
            List<BalanceName> references = GetDataBalanceNames("Data");
            List<BalanceCount> values = GetDataBalanceCount();
            const double difference = 0.04;

            if (references.Count == 2)
            {
                List<List<string>> refsSTS = new List<List<string>>()
                {
                    new List<string>() { references[0].ID1, references[0].ID2, references[0].ID3 },
                    new List<string>() { references[1].ID1, references[1].ID2, references[1].ID3 },
                };
                List<string> commonRefs = refsSTS[0].Intersect(refsSTS[1]).ToList();

                if (commonRefs.Count > 0)
                {
                    List<List<float>> valsSTS = new List<List<float>>()
                    {
                        new List<float>() { values[0].ID1, values[0].ID2, values[0].ID3 },
                        new List<float>() { values[1].ID1, values[1].ID2, values[1].ID3 },
                    };
                    List<float> countST = values.Select(s => s.Count).ToList();
                    int idxST1 = refsSTS[0].IndexOf(commonRefs[0]);
                    int idxST2 = refsSTS[1].IndexOf(commonRefs[0]);

                    if (Utils.IsInRange(countST[0], countST.Average(), difference) && Utils.IsInRange(countST[1], countST.Average(), difference))
                    {
                        float diff = Math.Abs(countST[0] - countST[1]);
                        int prod1 = countST[0] < countST.Average() ? 1 : -1;
                        int prod2 = prod1 == 1 ? -1 : 1;
                        valsSTS[0][idxST1] = valsSTS[0][idxST1] + (float)Math.Floor(diff / 2.0) * prod1;
                        valsSTS[1][idxST2] = valsSTS[1][idxST2] + (float)Math.Floor(diff / 2.0) * prod2;
                    }
                    RefreshBalanceQuantitys(valsSTS);
                }
            }
            else if (references.Count == 3)
            {
                List<List<float>> valsSTS = new List<List<float>>()
                {
                    new List<float>() { values[0].ID1, values[0].ID2, values[0].ID3 },
                    new List<float>() { values[1].ID1, values[1].ID2, values[1].ID3 },
                    new List<float>() { values[2].ID1, values[2].ID2, values[2].ID3 },
                };
                List<List<string>> refsSTS = new List<List<string>>()
                {
                    new List<string>() { references[0].ID1, references[0].ID2, references[0].ID3 },
                    new List<string>() { references[1].ID1, references[1].ID2, references[1].ID3 },
                    new List<string>() { references[2].ID1, references[2].ID2, references[2].ID3 },
                };
                List<float> countSTS = values.Select(s => s.Count).ToList();
                List<bool> countControl = new List<bool>()
                {
                    Utils.IsInRange(countSTS[0], countSTS.Average(), difference),
                    Utils.IsInRange(countSTS[1], countSTS.Average(), difference),
                    Utils.IsInRange(countSTS[2], countSTS.Average(), difference),
                };
                List<int> countControlIDX = countControl.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();

                if (countControlIDX.Count == 2)
                {
                    List<string> commonRefs = refsSTS[countControlIDX[0]].Intersect(refsSTS[countControlIDX[1]]).ToList();

                    if (commonRefs.Count > 0)
                    {
                        int idxST1 = refsSTS[countControlIDX[0]].IndexOf(commonRefs[0]);
                        int idxST2 = refsSTS[countControlIDX[1]].IndexOf(commonRefs[0]);
                        float diff = Math.Abs(countSTS[countControlIDX[0]] - countSTS[countControlIDX[1]]);
                        int prod1 = (countSTS[0] < countSTS.Average()) ? 1 : -1;
                        int prod2 = (prod1 == 1) ? -1 : 1;
                        valsSTS[countControlIDX[0]][idxST1] = valsSTS[countControlIDX[0]][idxST1] + (float)Math.Floor(diff / 2.0) * prod1;
                        valsSTS[countControlIDX[1]][idxST2] = valsSTS[countControlIDX[1]][idxST2] + (float)Math.Floor(diff / 2.0) * prod2;
                    }
                    RefreshBalanceQuantitys(valsSTS);
                }
            }
        }
        /// <summary>
        /// Crea la estacion 4 con los addons en las posiciones que van a ir, tanto con los nombres y sus cantidades.
        /// </summary>
        /// <param name="addons">Lista de addons en las posiciones</param>
        public static void LoadAddOns(List<List<string>> addons, string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceName> BalanceNameDB = database.GetCollection<BalanceName>("BWorkName");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");
            List<IDAddOn> tt = GetListAddOn(document);
            BalanceNameDB.InsertOneAsync(new BalanceName
            {
                _Id = 4,
                Stage = 4,
                File = document,
                ID1 = addons[0][0],
                ID2 = addons[0][1],
                ID3 = addons[0][2],
                Count = -1
            });

            List<int> qID1 = tt.Where(w => w.AddOn == addons[0][0]).Select(s => s.Count).ToList();
            List<int> qID2 = tt.Where(w => w.AddOn == addons[0][1]).Select(s => s.Count).ToList();
            List<int> qID3 = tt.Where(w => w.AddOn == addons[0][2]).Select(s => s.Count).ToList();

            int valID1 = qID1.Count > 0 ? qID1[0] : 0;
            int valID2 = qID2.Count > 0 ? qID2[0] : 0;
            int valID3 = qID3.Count > 0 ? qID3[0] : 0;
            int totalIDs = valID1 + valID2 + valID3;
            BalanceQuantityDB.InsertOneAsync(new BalanceCount
            {
                _Id = 4,
                Stage = 4,
                File = document,
                ID1 = valID1,
                ID2 = valID2,
                ID3 = valID3,
                Count = totalIDs
            });
        }

        public static int GetTotalAddons()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");
            var tt = BalanceQuantityDB.Find(s => s._Id == 4).ToList();
            return (int)tt[0].Count;
        }

        /// <summary>
        /// Almacena los registros de un archivo de excel en formato JSON.
        /// </summary>
        /// <param name="fileName">Nombre del archivo</param>
        /// <param name="document">Nombre del documento</param>
        public static List<int> LoadExcel(string fileName, string document)
        {
            var myValues = new List<int>() { };
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>(document);
            DataProduct dataProduct = new DataProduct();
            Random _random = new Random(13);
            using (XLWorkbook workbook = new XLWorkbook(fileName))
            {
                //-- enableRows = [ 'ORDER NUMBER', 'TRACKING NUMBER', 'VASE', 'ADD_ON_ID', 'SKU', 'SHIP DATE', 'SHIP METHOD', 'ORIGIN' ]
                List<int> enableRows = new List<int> { 2, 14, 7, 8, 4, 9, 26, 15 };
                Cursor.Current = Cursors.WaitCursor;
                bool isFirstRow = true;
                var rows = workbook.Worksheet(1).RowsUsed();
                var columns = workbook.Worksheet(1).ColumnsUsed();
                bool Vase_flag = false;
                bool Add_on_flag = false;
                bool Bar_flag = false;


                foreach (var column in columns)
                {

                    Console.WriteLine(column.Cell(1).Value.ToString());
                    var excolumns = column.Cell(1).Value.ToString();


                    if (excolumns == "VASE_ID")
                    {
                        Vase_flag = true;
                        Console.WriteLine("EXISTE VASE ID");
                    }

                    if (excolumns == "ADD_ON_ID")
                    {
                        Add_on_flag = true;
                        Console.WriteLine("EXISTE ADD ON");
                    }

                    if (excolumns == "BARCODE_NUMBER")
                    {
                        Bar_flag = true;
                        Console.WriteLine("EXISTE BARCODE");

                    }


                }

                foreach (var row in rows)
                {
                    //Console.WriteLine(row.Cell(enableRows[2]).Value.ToString());
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                    }

                    else
                    {
                        string vase;
                        string addOn;
                        string rawVase = row.Cell(enableRows[2]).Value.ToString();
                        string rawAddon = row.Cell(enableRows[3]).Value.ToString();

                        if (filterVases.Contains(rawVase))
                            vase = null;
                        else
                            vase = rawVase;

                        if (filterVases.Contains(rawAddon))
                            addOn = null;
                        else
                            addOn = rawAddon;

                        dataProduct = new DataProduct()
                        {
                            OrderNumber = row.Cell(enableRows[0]).Value.ToString(),
                            TrackingNumber = row.Cell(enableRows[1]).Value.ToString(),
                            Vase = vase,
                            AddOnId = addOn,
                            SKU = row.Cell(enableRows[4]).Value.ToString(),
                            ShipDate = row.Cell(enableRows[5]).Value.ToString(),
                            ShipMethod = row.Cell(enableRows[6]).Value.ToString(),
                            Origin = row.Cell(enableRows[7]).Value.ToString(),
                            Readed = false,
                            ReadedAddon = false,
                            ReadedStage = new int[] { 0, 0, 0 },
                            ReadedDate = "",
                            WhStage = -1,
                            Balance = -1,
                            BalanceStage = 0,
                            BalanceUUID = "",
                            Random = _random.Next(0, 5000),
                            TypeBand = "NV"
                        };
                        DataProductDB.InsertOneAsync(dataProduct);
                    }
                }
                Cursor.Current = Cursors.Default;



                if (Vase_flag == true && Add_on_flag == true && Bar_flag == true)
                {
                    myValues.Add(1);
                    myValues.Add(1);
                    myValues.Add(1);

                    Console.WriteLine(myValues);
                    Console.WriteLine("check flags");

                }

                else
                {

                    myValues.Add(0);
                    myValues.Add(0);
                    myValues.Add(0);

                    Console.WriteLine(myValues);
                    Console.WriteLine("check flags");

                }

            }
            return myValues;
        }
        /// <summary>
        /// Carga la lista de referencias que debe ir cargando elevador por elevador.
        /// </summary>
        /// <param name="document">Que tiene de archivo es: [Actual, Siguiente]</param>
        /// <param name="references">Lista de referencias</param>
        public static void LoadMReferences(string document, List<string> references)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<MLlenado> MReferenceDB = database.GetCollection<MLlenado>("BWorkMesanin");
            MReferenceDB.InsertOneAsync(
                new MLlenado
                {
                    _Id = document,
                    ID = 1,
                    References = references.ToArray(),
                    Quantitys = Utils.FillList(9, 1).ToArray(),
                    indRef = 0
                }
            );
        }
        /// <summary>
        /// Crea el estado inicial y el que se usa en el proceso de cada elevador.
        /// </summary>
        /// <param name="newidSelected">Lista de elevadores</param>
        /// <param name="init">Valor desde el cual se crea en la DB</param>
        public static void LoadStateStage(List<Stage> newidSelected, int init)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<StateStage> StateStageDB = database.GetCollection<StateStage>("BWorkState");

            foreach (Stage item in newidSelected)
            {
                StateStageDB.InsertOneAsync(
                    new StateStage
                    {
                        _Id = init,
                        ID = item.ID,
                        Check = item.Check,
                        Quantity = item.Quantity,
                        StageN = item.StageN,
                        Status = "init"
                    }
                );
                StateStageDB.InsertOneAsync(
                    new StateStage
                    {
                        _Id = 100 + init++,
                        ID = item.ID,
                        Check = item.Check,
                        Quantity = item.Quantity,
                        StageN = item.StageN,
                        Status = "state"
                    }
                );
            }
        }
        /// <summary>
        /// Saca los elevadores que van por estacion para cargar el total por estacion.
        /// </summary>
        public static void LoadStateIDs()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<StateStage> StateStageDB = database.GetCollection<StateStage>("BWorkState");
            List<StateStage> _stage = StateStageDB.Find(f => f._Id != 0).ToList();

            List<string> vasesUnique = _stage.Select(s => s.ID).Distinct().ToList();
            List<string> stage1 = _stage.Where(s => s.StageN == 1 && s.Status == "init").Select(s => s.ID).ToList();
            List<string> stage2 = _stage.Where(s => s.StageN == 2 && s.Status == "init").Select(s => s.ID).ToList();
            List<string> stage3 = _stage.Where(s => s.StageN == 3 && s.Status == "init").Select(s => s.ID).ToList();
            List<StageVases> stage1IDS = stage1.GroupBy(s => s).Select(g => new StageVases { Vase = g.Key, Count = g.Count() }).ToList();
            List<StageVases> stage2IDS = stage2.GroupBy(s => s).Select(g => new StageVases { Vase = g.Key, Count = g.Count() }).ToList();
            List<StageVases> stage3IDS = stage3.GroupBy(s => s).Select(g => new StageVases { Vase = g.Key, Count = g.Count() }).ToList();
            LoadTotalIDs(vasesUnique, stage1IDS, stage2IDS, stage3IDS);
        }
        /// <summary>
        /// Saca la cantidad que va por estacion de cada una de las referencias que tiene en el archivo actual.
        /// </summary>
        /// <param name="vasesUnique">Lista con los valores unicos de los ID</param>
        /// <param name="stage1IDS">Lista con los ID de la estacion</param>
        /// <param name="stage2IDS">Lista con los ID de la estacion</param>
        /// <param name="stage3IDS">Lista con los ID de la estacion</param>
        public static void LoadTotalIDs(List<string> vasesUnique, List<StageVases> stage1IDS, List<StageVases> stage2IDS, List<StageVases> stage3IDS)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDStages> IDStagesDB = database.GetCollection<IDStages>("BalanceIDStages");

            int ii = 1;
            foreach (var item in vasesUnique)
            {
                int vaseItem1 = 0;
                int vaseItem2 = 0;
                int vaseItem3 = 0;

                if (item == "NV")
                    continue;

                var tt1 = stage1IDS.Select(s => s.Vase).ToList().Contains(item);
                if (tt1)
                {
                    int index = stage1IDS.FindIndex(s => s.Vase.Equals(item));
                    vaseItem1 = stage1IDS[index].Count;
                }
                else
                    vaseItem1 = 0;

                var tt2 = stage2IDS.Select(s => s.Vase).ToList().Contains(item);
                if (tt2)
                {
                    int index = stage2IDS.FindIndex(s => s.Vase.Equals(item));
                    vaseItem2 = stage2IDS[index].Count;
                }
                else
                    vaseItem2 = 0;

                var tt3 = stage3IDS.Select(s => s.Vase).ToList().Contains(item);
                if (tt3)
                {
                    int index = stage3IDS.FindIndex(s => s.Vase.Equals(item));
                    vaseItem3 = stage3IDS[index].Count;
                }
                else
                    vaseItem3 = 0;

                IDStagesDB.InsertOneAsync(new IDStages { Vase = item, Stage1 = vaseItem1, Stage2 = vaseItem2, Stage3 = vaseItem3, status = "init", _Id = ii });
                IDStagesDB.InsertOneAsync(new IDStages { Vase = item, Stage1 = vaseItem1, Stage2 = vaseItem2, Stage3 = vaseItem3, status = "state", _Id = 10 + ii++ });
            }
        }
        /// <summary>
        /// Permite crear los registros de los IDs de los productos distintos segun el tipo de documento
        /// con el que se este trabajando.
        /// </summary>
        /// <param name="document">El nombre del documento puede ser Actual o Next.</param>
        /// <param name="init">El valor inicial a partir del cual se van a crear los registros.</param>
        public static void LoadCountIDs(string document, int init)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>(document);
            IMongoCollection<VaseCount> IDDistinctDB = database.GetCollection<VaseCount>("BalanceIDProducts");

            List<string> nameVases = DataProductDB.Find(s => s.OrderNumber != "").ToList().Where(s => s.Vase != null).Select(s => s.Vase).ToList();
            List<VaseCount> countVases = nameVases.GroupBy(s => s).Select(g => new VaseCount { Vase = g.Key, Count = g.Count() }).ToList();

            foreach (VaseCount item in countVases)
            {
                IDDistinctDB.InsertOneAsync(new VaseCount { _Id = init++, File = document, Vase = item.Vase, Count = item.Count });
            }
        }
        /// <summary>
        /// Carga los nombres de los IDs balanceados.
        /// </summary>
        /// <param name="combo">Lista de IDs diferentes</param>
        /// <param name="places">Cantidad de estaciones</param>
        /// <param name="BalanceIndex">Nombres de los IDs por estacion</param>
        /// <param name="StageIndex">Numero de la estacion</param>
        /// <param name="document">El documento que se va hacer la accion</param>
        /// <param name="init">Valor a partir del cual se inicial los registros</param>
        public static void LoadBalanceNames(ComboBox combo, int places, List<int> BalanceIndex, List<int> StageIndex, List<int> false_indexes, string document, int init)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceName> BalanceNameDB = database.GetCollection<BalanceName>("BWorkName");

            for (int i = 0; i < places; i++)
            {
                BalanceNameDB.InsertOneAsync(new BalanceName
                {
                    _Id = StageIndex[i] + 1,
                    Stage = StageIndex[i] + 1,
                    File = document,
                    ID1 = combo.Items[BalanceIndex[0 + i * 3]].ToString(),
                    ID2 = combo.Items[BalanceIndex[1 + i * 3]].ToString(),
                    ID3 = combo.Items[BalanceIndex[2 + i * 3]].ToString(),
                    Count = -1
                });
            }
            for (int ii = 0; ii < false_indexes.Count; ii++)
            {
                BalanceNameDB.InsertOneAsync(new BalanceName
                {
                    _Id = false_indexes[ii] + 1,
                    Stage = false_indexes[ii] + 1,
                    File = document,
                    ID1 = "NV",
                    ID2 = "NV",
                    ID3 = "NV",
                    Count = -1
                });
            }
        }
        /// <summary>
        /// Almacena la cantidades por ID que tiene por estacion.
        /// </summary>
        /// <param name="values">Array de valores</param>
        /// <param name="workers2">Lista de posiciones</param>
        /// <param name="true_indexes">Lista de la posicion de las estaciones habilitadas</param>
        /// <param name="places">Cantidad de estaciones habilitadas</param>
        /// <param name="document">Nombre del documento</param>
        /// <param name="init">Valor inicial a partir del cual crea los registros</param>
        public static void LoadBalanceCount(float[][] values, int[] workers2, List<int> true_indexes, List<int> false_indexes, int places, string document, int init)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");

            for (int i = 0; i < places; i++)
            {
                BalanceQuantityDB.InsertOneAsync(new BalanceCount
                {
                    _Id = true_indexes[i] + 1,
                    Stage = true_indexes[i] + 1,
                    File = document,
                    ID1 = values[workers2[i]][0],
                    ID2 = values[workers2[i]][1],
                    ID3 = values[workers2[i]][2],
                    Count = values[workers2[i]].Sum()
                });
            }
            for (int ii = 0; ii < false_indexes.Count; ii++)
            {
                BalanceQuantityDB.InsertOneAsync(new BalanceCount
                {
                    _Id = false_indexes[ii] + 1,
                    Stage = false_indexes[ii] + 1,
                    File = document,
                    ID1 = 0,
                    ID2 = 0,
                    ID3 = 0,
                    Count = 0
                });
            }
        }

        //-------------------------------
        //              READ            |
        //-------------------------------
        //public static void get_balance()
        //{
        //    MongoClient client = GetDBConnection();
        //    IMongoDatabase database = GetDataBase(client);
        //    //var statics = database.GetCollection<BsonDocument>("Statics");
        //    //var documents = statics.Find(new BsonDocument()).ToList();
        //    var statics = database.GetCollection<BsonDocument>("Statics");
        //    var documents = statics.Find(new BsonDocument()).ToList();
        //    Console.WriteLine(documents.ToString());

        //    foreach (var doc in documents)
        //    {               
        //            var vases = doc.GetValue(i);         
        //        Console.WriteLine(vases);

        //    }                      
        //}

        public static List<Statics> get_balance()
        {
            MongoClient client = GetDBConnection();
            IMongoDatabase database = GetDataBase(client);
            IMongoCollection<Statics> StaticsDB = database.GetCollection<Statics>("Statics");
            List<Statics> doc = StaticsDB.Find(f => f._id == 1).ToList();



            Console.WriteLine(doc);
            //List<Statics> Static = StaticsDB.Find(s => s.BalancedWork).ToList();

            //Console.WriteLine(documents.ToString());

            //foreach (var doc in documents)
            //{               
            //        var vases = doc.GetValue(i);         
            //    Console.WriteLine(vases);

            //
            //
            return doc;
        }

        public static void SetVase2Stage(int valueBalance)
        {
            //MongoClient client = new MongoClient(mongoDBConnection);       
            //IMongoDatabase database = client.GetDatabase("EliteFlower");
            MongoClient client = GetDBConnection();


            IMongoDatabase database = GetDataBase(client);

            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>("Data");
            IMongoCollection<BalanceName> BalanceNameDB = database.GetCollection<BalanceName>("BWorkName");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");


            List<BalanceName> balanceNames = BalanceNameDB.Find(s => s.Stage < 4).ToList();
            List<BalanceCount> balanceCounts = BalanceQuantityDB.Find(s => s.Stage < 4).ToList();
            string balanceUUID = Guid.NewGuid().ToString();
            int stages = balanceNames.Count;
            int contador = 0;



            System.Threading.Thread.Sleep(10);
            for (int ii = 0; ii < stages; ii++)
            {

                // int tickswait = 5;


                //Debug.WriteLine("ii = "+ ii.ToString());
                //-- ID1
                //List<DataProduct> dataProductID1 = DataProductDB.Find(s => s.Vase == balanceNames[ii].ID1 && s.WhStage == -1).ToList().OrderBy(ob => ob.Random).ToList();
                List<DataProduct> dataProductID1 = DataProductDB.Find(s => s.Vase == balanceNames[ii].ID1 && s.WhStage == -1).ToList();
                if (dataProductID1.Count > 0)
                {
                    for (int jj = 0; jj < balanceCounts[ii].ID1; jj++)
                    {

                        //Debug.WriteLine("jj = " + jj.ToString());
                        dataProductID1[jj].WhStage = balanceNames[ii].Stage;
                        dataProductID1[jj].BalanceUUID = balanceUUID;
                        dataProductID1[jj].Balance = valueBalance;
                        DataProductDB.ReplaceOneAsync(r => r.TrackingNumber == dataProductID1[jj].TrackingNumber, dataProductID1[jj]);
                        //System.Threading.Thread.SpinWait(tickswait);

                        //contadorID1++;
                        //if (contadorID1 == 1000)
                        //{
                        System.Threading.Thread.Sleep(1);
                        //    contadorID1 = 0;
                        //}
                    }
                }
                //-- ID2
                //List<DataProduct> dataProductID2 = DataProductDB.Find(s => s.Vase == balanceNames[ii].ID2 && s.WhStage == -1).ToList().OrderBy(ob => ob.Random).ToList();
                List<DataProduct> dataProductID2 = DataProductDB.Find(s => s.Vase == balanceNames[ii].ID2 && s.WhStage == -1).ToList();
                if (dataProductID2.Count > 0)
                {
                    for (int jj = 0; jj < balanceCounts[ii].ID2; jj++)
                    {

                        dataProductID2[jj].WhStage = balanceNames[ii].Stage;
                        dataProductID2[jj].BalanceUUID = balanceUUID;
                        dataProductID2[jj].Balance = valueBalance;
                        DataProductDB.ReplaceOneAsync(r => r.TrackingNumber == dataProductID2[jj].TrackingNumber, dataProductID2[jj]);
                        // System.Threading.Thread.SpinWait(tickswait);
                        //contadorID2++;
                        //if (contadorID2 == 1000)
                        //{
                        System.Threading.Thread.Sleep(1);
                        //    contadorID2 = 0;
                        //}
                    }
                }
                //-- ID3
                //List<DataProduct> dataProductID3 = DataProductDB.Find(s => s.Vase == balanceNames[ii].ID3 && s.WhStage == -1).ToList().OrderBy(ob => ob.Random).ToList();
                List<DataProduct> dataProductID3 = DataProductDB.Find(s => s.Vase == balanceNames[ii].ID3 && s.WhStage == -1).ToList();
                if (dataProductID3.Count > 0)
                {
                    for (int jj = 0; jj < balanceCounts[ii].ID3; jj++)
                    {

                        dataProductID3[jj].WhStage = balanceNames[ii].Stage;
                        dataProductID3[jj].BalanceUUID = balanceUUID;
                        dataProductID3[jj].Balance = valueBalance;
                        DataProductDB.ReplaceOneAsync(r => r.TrackingNumber == dataProductID3[jj].TrackingNumber, dataProductID3[jj]);
                        // System.Threading.Thread.SpinWait(tickswait);
                        //contadorID3++;
                        //if (contadorID3 == 1000)
                        //{
                        System.Threading.Thread.Sleep(1);
                        //    contadorID3 = 0;
                        //}
                    }
                }
                contador++;
                if (contador == 1000)
                {
                    System.Threading.Thread.Sleep(10);
                    contador = 0;
                }

            }
        }
        /// <summary>
        /// Recupera el estado de como se encontraba el balanceo despues de activarse la UPS.
        /// </summary>
        /// <param name="cbworkers">Lista de los combobox</param>
        public static void SetRecoveryNames(List<ComboBox> cbworkers)
        {
            List<BalanceName> references = GetDataBalanceNames("Data");
            List<BalanceName> tt1 = references.Where(w => w.Stage == 1).ToList();
            List<BalanceName> tt2 = references.Where(w => w.Stage == 2).ToList();
            List<BalanceName> tt3 = references.Where(w => w.Stage == 3).ToList();
            List<string> refStages = new List<string>() { tt1[0].ID1, tt1[0].ID2, tt1[0].ID3, tt2[0].ID1, tt2[0].ID2, tt2[0].ID3, tt3[0].ID1, tt3[0].ID2, tt3[0].ID3 };
            int ii = 0;
            foreach (ComboBox item in cbworkers)
            {
                item.SelectedItem = refStages[ii++];
            }
        }
        /// <summary>
        /// Carga el estado inicial del mesanin.
        /// </summary>
        public static List<string> InitMesanin(string[] lastReferences)
        {
            List<BalanceName> balanceo = GetDataBalanceNames("Data");
            if (balanceo.Count > 0)
            {
                List<BalanceName> station1 = balanceo.Where(w => w.Stage == 1).ToList();
                List<BalanceName> station2 = balanceo.Where(w => w.Stage == 2).ToList();
                List<BalanceName> station3 = balanceo.Where(w => w.Stage == 3).ToList();
                List<string> references = new List<string>() {
                    station1[0].ID1, station1[0].ID2, station1[0].ID3,
                    station2[0].ID1, station2[0].ID2, station2[0].ID3,
                    station3[0].ID1, station3[0].ID2, station3[0].ID3,
                };
                LoadMReferences("Data", references);
                return references;
            }
            return new List<string>() { "NV", "NV", "NV", "NV", "NV", "NV", "NV", "NV", "NV" };
        }
        /// <summary>
        /// Va actualizando la cantidad de cajas que se han metido en los elevadores en el estado
        /// de llenado inicial.
        /// </summary>
        public static void SetMReferences(int indRef)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<MLlenado> MReferenceDB = database.GetCollection<MLlenado>("BWorkMesanin");
            List<MLlenado> tt = MReferenceDB.Find(s => s.ID == indRef).ToList();
            int indXR = tt[0].indRef;

            tt[0].Quantitys[indXR] += 1;
            if (tt[0].Quantitys[indXR] == 2)
            {
                tt[0].indRef += 1;
            }
            MReferenceDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
        }
        /// <summary>
        /// Va indicando que referencia se debe ir agregando segun el elevador en el que se encuentra.
        /// </summary>
        /// <param name="lblMesanin"></param>
        /// <returns></returns>
        public static bool SetLabelMesanin(Label lblMesanin)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<MLlenado> MReferenceDB = database.GetCollection<MLlenado>("BWorkMesanin");
            List<MLlenado> tt = MReferenceDB.Find(s => s.ID == 1).ToList();
            int indRef = tt[0].indRef;

            if (indRef == 8)
                lblMesanin.Text = $"Ingrese la refencia {tt[0].References[indRef]} - {tt[0].Quantitys[indRef]}/2";
            else if (tt[0].indRef == 7)
                lblMesanin.Text = $"Ingrese la refencia {tt[0].References[indRef]} - {tt[0].Quantitys[indRef]}/2\nPuede ir preparando la ultima referencia {tt[0].References[indRef]}";
            else if (tt[0].indRef < 8)
                lblMesanin.Text = $"Ingrese la refencia {tt[0].References[indRef]} - {tt[0].Quantitys[indRef]}/2\nPuede ir preparando la referencia {tt[0].References[indRef]}";
            else if (tt[0].indRef > 8)
            {
                lblMesanin.Text = "El llenado inicial esta completo";
                string msg = $"El llenado inicial esta completo.\n\nYa puede iniciar con el proceso.";
                SetInitValuesPulmon();
                MessageBox.Show(msg, "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Cuando ya se agregaron todos las cajas en los elevadores entonces los inicia en 1 ya que
        /// todos cuentan con una caja en el pulmon.
        /// </summary>
        private static void SetInitValuesPulmon()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<MLlenado> MReferenceDB = database.GetCollection<MLlenado>("BWorkMesanin");
            List<MLlenado> tt = MReferenceDB.Find(s => s.ID == 1).ToList();

            tt[0].Quantitys = Utils.FillList(9, 1).ToArray();
            MReferenceDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
        }
        /// <summary>
        /// Refresca la cantidad que tiene en las estaciones y su total.
        /// </summary>
        /// <param name="valsSTS"></param>
        public static void RefreshBalanceQuantitys(List<List<float>> valsSTS)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");
            List<BalanceCount> tt = BalanceQuantityDB.Find(s => s._Id != 0).ToList();

            for (int i = 0; i < valsSTS.Count; i++)
            {
                tt[i].ID1 = valsSTS[i][0];
                tt[i].ID2 = valsSTS[i][1];
                tt[i].ID3 = valsSTS[i][2];
                tt[i].Count = valsSTS[i][0] + valsSTS[i][1] + valsSTS[i][2];
                BalanceQuantityDB.ReplaceOneAsync(r => r._Id == tt[i]._Id, tt[i]);
            }
        }
        /// <summary>
        /// Indica la cantidad que lleva en esa referencia en especifico.
        /// </summary>
        /// <param name="indRef"></param>
        /// <returns></returns>
        public static MLlenado GetMReferences(int indRef)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<MLlenado> MReferenceDB = database.GetCollection<MLlenado>("BWorkMesanin");
            List<MLlenado> tt = MReferenceDB.Find(s => s.ID == indRef).ToList();
            return tt[0];
        }
        /// <summary>
        /// Indica el nombre del archivo actual.
        /// </summary>
        /// <returns></returns>
        public static string GetFileNameML()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            return tt[0].NameActualFile;
        }
        public static string GetFilePath()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            return tt[0].FilePath;
        }
        /// <summary>
        /// Obtiene el estado del modo de UPS.
        /// </summary>
        /// <returns></returns>
        public static bool GetRecovery()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            return tt[0].Recovery;
        }
        /// <summary>
        /// Obtiene la lista de los IDs de los addons del archivo actual con toda su informacion.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static List<IDAddOn> GetListAddOn(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDAddOn> IDAddOnDB = database.GetCollection<IDAddOn>("BalanceIDAddOn");
            return IDAddOnDB.Find(s => s._Id != 0).ToList().Where(s => s.File == document).ToList();
        }
        /// <summary>
        /// Obtiene la lista de nombres de los IDs de los addons del archivo actual.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static List<string> GetNamesAddOn(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDAddOn> IDAddOnDB = database.GetCollection<IDAddOn>("BalanceIDAddOn");
            return IDAddOnDB.Find(s => s._Id != 0).ToList().Where(s => s.File == document).Select(s => s.AddOn).ToList();
        }
        /// <summary>
        /// Obtiene los nombres de las 3 referencias que estan en la estacion del addon.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAddons()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceName> DataBalanceDB = database.GetCollection<BalanceName>("BWorkName");
            List<BalanceName> balanceNames = DataBalanceDB.Find(f => f._Id == 4).ToList();
            return new List<string>() { balanceNames[0].ID1, balanceNames[0].ID2, balanceNames[0].ID3 };
        }
        /// <summary>
        /// Obtiene las cantidad que tiene por las 3 referencias que estan en la estacion del addon.
        /// </summary>
        public static BalanceCount GetAddonQuantity()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");
            List<BalanceCount> addonQ = BalanceQuantityDB.Find(f => f.Stage == 4).ToList();
            return addonQ[0];
        }
        /// <summary>
        /// Obtiene las estadisticas del documento que se le esta pidiendo.
        /// </summary>
        /// <param name="document">Nombre del documento</param>
        /// <param name="english">Si es en español/ingles</param>
        public static void GetStatistics(string document, bool english)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<VaseCount> IDDistinctDB = database.GetCollection<VaseCount>("BalanceIDProducts");
            IMongoCollection<IDAddOn> IDAddOnDB = database.GetCollection<IDAddOn>("BalanceIDAddOn");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>(document);

            List<VaseCount> countVases = IDDistinctDB.Find(s => s._Id != 0).ToList().Where(s => s.File == document).ToList();
            List<IDAddOn> addOn = IDAddOnDB.Find(f => f._Id != 0).ToList();
            string msgShow = Utils.MessageStatistics(DataProductDB.Find(f => f.OrderNumber != "").ToList().Count, english, countVases, addOn);
            MessageBox.Show(msgShow, UIMessages.EliteFlower(56, english), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        /// <summary>
        /// Muestra un resumen de cuantas ordenes se han procesado hasta el momento.
        /// Ademas de mostrar segun la referencia de vases cuantos han sido
        /// </summary>
        /// <returns></returns>
        public static (int, List<VaseCount>) GetSummaryProduction()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>("DataSummary");

            List<DataProduct> data = DataProductDB.Find(s => s.Readed == true).ToList();
            if (data.Count == 0)
            {
                return (0, new List<VaseCount>());
            }
            else
            {
                List<string> nameVases = data.Where(s => s.Vase != null).Select(s => s.Vase).ToList();
                List<VaseCount> countVases = nameVases.GroupBy(s => s).Select(g => new VaseCount { Vase = g.Key, Count = g.Count() }).ToList();
                return (data.Count, countVases);
            }
        }

        /// <summary>
        /// Hace una busqueda por Numero de orden o Numero de tracking para obtener el ID del vase.
        /// </summary>
        /// <param name="query">Numero de orden o Numero de tracking</param>
        /// <returns>Retorna el ID del vase</returns>
        public static string GetSearchVase(string query, int nStage, string typeband, bool Tracking)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>("Data");

            List<DataProduct> data = (Tracking) ?
                DataProductDB.Find(w => w.TrackingNumber == query).Limit(1).ToList() :
                DataProductDB.Find(w => w.OrderNumber == query).Limit(1).ToList();
            if (data.Count > 0)
            {
              //Se verifica si el Vase  es Nulo y si este no se ha leido en la estación de la cual llega la lectura
                if (data[0].Vase == null && data[0].ReadedStage[nStage] == 0)
                {
                    if (data[0].Readed == false)
                    {
                        data[0].Readed = true;
                        data[0].TypeBand = typeband;
                        DataProductDB.ReplaceOne(r => r.TrackingNumber == data[0].TrackingNumber, data[0]);
                    }
                    return "OFF";
                }
                //se verifica que el dato no se halla leido en la estación y la estación generada en el balanceo corresponde a la estación
                //en la que se realiza la lectura
                else if (/*data[0].ReadedStage[nStage] == 0 &&*/ data[0].WhStage == nStage + 1)
                {
                    Console.WriteLine("Vase: " + data[0].Vase + "en estacion " + nStage +1);
                    return data[0].Vase; //retorna el vase correspondiente a la lectura
                }
                //se verifica que el dato no se halla leido en la estación y la estación generada en el balanceo no corresponde a la estación
                //en la que se realiza la lectura
                else if (data[0].ReadedStage[nStage] == 0 && data[0].WhStage != nStage + 1)
                {
                    return "OFF";
                }
                else
                {
                    Console.WriteLine("Vase ya se leyó en esta estación");
                    return "CK";  //condicion correspondiente a que ya se realizó la lectura
                }
            }
            else
            {
                return "ND";
            }
        }

        public static void SetReadedVase(string query, int nStage, string typeband, bool Tracking)
        {
            MongoClient client = new MongoClient(mongoDBConnection);


            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>("Data");
            List<DataProduct> data = (Tracking) ?
                DataProductDB.Find(w => w.TrackingNumber == query).Limit(1).ToList() :
                DataProductDB.Find(w => w.OrderNumber == query).Limit(1).ToList();
            if (data.Count > 0)
            {
                data[0].Readed = true;
                data[0].ReadedStage[nStage] = 1;
                data[0].BalanceStage += (int)Math.Pow(2, nStage);
                data[0].TypeBand = typeband;
                DataProductDB.ReplaceOne(r => r.TrackingNumber == data[0].TrackingNumber, data[0]);
            }
        }

        public static void SetReadedStage(string query, int nStage, bool Tracking)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>("Data");
            List<DataProduct> data = (Tracking) ?
                DataProductDB.Find(w => w.TrackingNumber == query).Limit(1).ToList() :
                DataProductDB.Find(w => w.OrderNumber == query).Limit(1).ToList();
            if (data.Count > 0)
            {
                data[0].ReadedStage[nStage] = 1;
                data[0].BalanceStage += (int)Math.Pow(2, nStage);
                DataProductDB.ReplaceOne(r => r.TrackingNumber == data[0].TrackingNumber, data[0]);
            }
        }

        public static void SetReadedAddon(string query, bool Tracking)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>("Data");
            List<DataProduct> data = (Tracking) ?
                DataProductDB.Find(w => w.TrackingNumber == query).Limit(1).ToList() :
                DataProductDB.Find(w => w.OrderNumber == query).Limit(1).ToList();
            if (data.Count > 0)
            {
                data[0].ReadedAddon = true;
                DataProductDB.ReplaceOne(r => r.TrackingNumber == data[0].TrackingNumber, data[0]);
            }
        }
        /// <summary>
        /// Hace una busqueda por Numero de orden o Numero de tracking para obtener el ID del addon.
        /// </summary>
        /// <param name="query">Numero de orden o Numero de tracking</param>
        /// <returns>Retorna el ID del addon</returns>
        public static string GetSearchAddOn(string query, bool Tracking)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>("Data");

            List<DataProduct> data = (Tracking) ?
                DataProductDB.Find(w => w.TrackingNumber == query).Limit(1).ToList() :
                DataProductDB.Find(w => w.OrderNumber == query).Limit(1).ToList();
            if (data.Count > 0)
            {
                if (data[0].AddOnId == null && data[0].ReadedAddon == false)
                {
                    //Apaga los pilotos si no hay addon y se leyó la primera vez, actualiza el registro en 
                    // la base de datos
                    data[0].ReadedAddon = true;
                    DataProductDB.ReplaceOne(r => r.TrackingNumber == data[0].TrackingNumber, data[0]);
                    return "OFF";
                }
                if (data[0].ReadedAddon == false)
                {
                    //si no se ha leido, retorna el addonid leido
                    return data[0].AddOnId;
                }
                else
                {
                    return "CK";
                }
            }
            else
            {
                // ND : No data?
                return "ND";
            }
        }
        /// <summary>
        /// Obtiene los nombres de los Vase del documento pedido.
        /// </summary>
        /// <param name="document">Nombre del documento</param>
        /// <returns>Retorna una lista</returns>
        public static List<string> GetNameVases(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<VaseCount> IDDistinctDB = database.GetCollection<VaseCount>("BalanceIDProducts");
            return IDDistinctDB.Find(s => s.File == document).ToList().Select(s => s.Vase).ToList();
        }
        /// <summary>
        /// Obtiene una lista de los datos del documento
        /// </summary>
        /// <param name="document">Nombre del documento</param>
        /// <returns>Retorna una lista</returns>
        /// 

        public static List<string> GetMasterProducts(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");
            return ProductDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
        }

        public static List<string> GetTemplate(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<Btemplate> TemplateDB = database.GetCollection<Btemplate>("BalanceTemplate");
            return TemplateDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
        }


        public static List<DataProduct> GetDataProduct(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>(document);
            return DataProductDB.Find(s => s.OrderNumber != "").ToList();
        }
        /// <summary>
        /// Obtiene una lista de los datos de IDDistinct
        /// </summary>
        /// <param name="document">Nombre del documento</param>
        /// <returns>Retorna una lista</returns>
        public static List<VaseCount> GetDataVase(string document)
        {

            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<VaseCount> IDDistinctDB = database.GetCollection<VaseCount>("BalanceIDProducts");
            return IDDistinctDB.Find(s => s.File == document).ToList();
        }
        /// <summary>
        /// Obtiene una lista de los datos de BalanceQuantity
        /// </summary>
        /// <returns>Retorna una lista</returns>
        /// 
        public static List<BalanceCount> GetDataBalanceCount()
        {
            //   MongoClient client = new MongoClient(mongoDBConnection);
            MongoClient client = GetDBConnection();
            // IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoDatabase database = GetDataBase(client);

            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");
            return BalanceQuantityDB.Find(f => f.Stage != 0).ToList();
        }








        /// <summary>
        /// Obtiene una lista de los datos de BalanceName
        /// </summary>
        /// <param name="document">Nombre del documento</param>
        /// <returns>Retorna una lista</returns>
        public static List<BalanceName> GetDataBalanceNames(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceName> BalanceNameDB = database.GetCollection<BalanceName>("BWorkName");
            return BalanceNameDB.Find(f => f.File == document).ToList();
        }
        /// <summary>
        /// 
        /// }
        /// </summary>
        /// <returns></returns>
        public static bool GetOverview()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            Console.WriteLine(tt);
            return tt[0].Overview;
            // return true;
        }
        /// <summary>
        /// Saca la lista de los diferentes addons con sus cantidades del archivo actual que se esta utilizando.
        /// </summary>
        /// <param name="document">Determina cual es el archivo actual que se esta usando.</param>
        /// <param name="init">Desde donde se va contar para guardar informacion.</param>
        public static void GetDistinctAddOn(string document, int init)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>(document);
            IMongoCollection<IDAddOn> IDAddOnDB = database.GetCollection<IDAddOn>("BalanceIDAddOn");

            List<DataProduct> tt = DataProductDB.Find(f => f.OrderNumber != "").ToList();
            List<string> tt1 = tt.Where(w => w.AddOnId != null && w.AddOnId != "" && w.AddOnId != "0").Select(s => s.AddOnId).Distinct().ToList().Select(s => s.Split(',')).SelectMany(m => m).ToList();

            List<string> tt2 = new List<string>();
            foreach (string item in tt1)
                tt2.Add(item.Replace(" ", ""));
            tt2 = tt2.Distinct().ToList();

            for (int i = 0; i < tt2.Count; i++)
            {
                IDAddOnDB.InsertOneAsync(new IDAddOn
                {
                    _Id = init++,
                    AddOn = tt2[i],
                    Count = tt.Where(w => w.AddOnId != null && w.AddOnId != "" && w.AddOnId != "0").Where(s => s.AddOnId.Contains(tt2[i])).ToList().Count,
                    File = document
                });
            }
        }

        //-----------------------------------
        //              UPDATE              |
        //-----------------------------------

        /// <summary>
        /// Verifica que los logs no sobrepasen de 30.000 registros, y si ocurre esto entonces
        /// elimina la cantidad de registros que se pasaron pero de los registros mas
        /// antiguos.
        /// </summary>
        public static void CleanLogs()
        {
            int limitLogs = 30000;
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<LogsEliteFlower> LogFileDB = database.GetCollection<LogsEliteFlower>("ErrorLogs");
            List<LogsEliteFlower> logs = LogFileDB.Find(f => f.sourceError != "").ToList();

            if (logs.Count > limitLogs)
            {
                List<LogsEliteFlower> deletedLogs = logs.GetRange(0, logs.Count - limitLogs);
                foreach (LogsEliteFlower item in deletedLogs)
                {
                    LogFileDB.DeleteOneAsync(d => d._id == item._id);
                }
            }
        }
        /// <summary>
        /// Convierte los datos de una lista a formato de documentos en MongoDB
        /// </summary>
        /// <param name="stageElevators">Lista de datos a transformar</param>
        public static void ConvertStages(List<StageElevator> stageElevators, string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceName> BalanceNameDB = database.GetCollection<BalanceName>("BWorkName");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");

            List<List<StageElevator>> stages = new List<List<StageElevator>>
            {
                stageElevators.Where(s => s.StageN == 1).OrderBy(s => s.ElevatorN).ToList(),
                stageElevators.Where(s => s.StageN == 2).OrderBy(s => s.ElevatorN).ToList(),
                stageElevators.Where(s => s.StageN == 3).OrderBy(s => s.ElevatorN).ToList()
            };

            List<Stage> newStages = new List<Stage>();
            int init = 1;
            foreach (List<StageElevator> stage in stages)
            {
                BalanceNameDB.InsertOneAsync(new BalanceName
                {
                    _Id = init,
                    Stage = stage[0].StageN,
                    File = document,
                    ID1 = stage[0].ID,
                    ID2 = stage[1].ID,
                    ID3 = stage[2].ID,
                    Count = -1
                });
                BalanceQuantityDB.InsertOneAsync(new BalanceCount
                {
                    _Id = init,
                    Stage = stage[0].StageN,
                    File = document,
                    ID1 = stage[0].Quantity,
                    ID2 = stage[1].Quantity,
                    ID3 = stage[2].Quantity,
                    Count = (stage[0].Quantity + stage[1].Quantity + stage[2].Quantity)
                });
                init++;
            }
        }
        /// <summary>
        /// Cambia el estado de la ventana de ayuda.
        /// </summary>
        public static void SetOverview()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            tt[0].Overview = true;
            OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
        }
        /// <summary>
        /// Cambia el estado de UPS, cuando detecta un cambio en la caida de energia o se activa desde el programa.
        /// </summary>
        public static void SetRecovery()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");

            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            tt[0].Recovery = !tt[0].Recovery;
            OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
        }
        /// <summary>
        /// Guarda el nombre del archivo actual que se esta usando.
        /// </summary>
        /// <param name="filename"></param>
        public static void SetFileNameML(string filename, bool update, string filepath)
        {
            Console.WriteLine("file path ");
            Console.WriteLine(filepath);
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDOverview> OverviewDB = database.GetCollection<IDOverview>("Metadata");
            List<IDOverview> tt = OverviewDB.Find(f => f.ID == 1).ToList();
            string[] lines = filename.Split(new string[] { "\\" }, StringSplitOptions.None);
            string path = filepath;
            tt[0].NameActualFile = lines[lines.Length - 1];
            tt[0].FilePath = path;

            if (update == true)
            {
                tt[0].lastUpdate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            {
                tt[0].createDate = DateTime.Now.ToString("MM/dd/yyyy");
                tt[0].lastUpdate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            OverviewDB.ReplaceOneAsync(r => r.ID == tt[0].ID, tt[0]);
        }
        /// <summary>
        /// Saca una unidad del sistema de los addons y actualiza  el dato.
        /// </summary>
        /// <param name="response">Lista de addons que lleva una etiqueta</param>
        public static void SetAddonQuantity(List<string> response)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");
            BalanceCount addon = GetAddonQuantity();
            if (response.Count > 0)
            {
                List<int> idx = response.Select(s => GetAddons().IndexOf(s)).ToList();
                foreach (int item in idx)
                {
                    if (item != -1)
                    {
                        switch (item)
                        {
                            case 0:
                                if (addon.ID1 > 0)
                                    addon.ID1--;
                                break;
                            case 1:
                                if (addon.ID2 > 0)
                                    addon.ID2--;
                                break;
                            case 2:
                                if (addon.ID3 > 0)
                                    addon.ID3--;
                                break;
                            default: break;
                        }

                    }
                }
                addon.Count = (addon.ID1 + addon.ID2 + addon.ID3);
                BalanceQuantityDB.ReplaceOneAsync(r => r._Id == addon._Id, addon);
            }
        }


        //-----------------------------------
        //              DELETE              |
        //-----------------------------------

        /// <summary>
        /// Borra los logs generados.
        /// Nota: solamente usarlo en las pruebas para borrar mas rapido los registros.
        /// </summary>
        public static void DeleteLogs()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<LogsEliteFlower> LogFileDB = database.GetCollection<LogsEliteFlower>("ErrorLogs");
            LogFileDB.DeleteManyAsync(d => d.sourceError != "");
        }
        /// <summary>
        /// Permite borrar las referencias que tiene guardadas en el mesanin.
        /// </summary>
        /// <param name="indRef"></param>
        public static void DeleteMReferences(int indRef)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<MLlenado> MReferenceDB = database.GetCollection<MLlenado>("BWorkMesanin");
            MReferenceDB.DeleteOneAsync(d => d.ID == indRef);
        }
        /// <summary>
        /// Permite borrar el estado asociado a los addons en la estacion
        /// </summary>
        public static void DeleteAddons()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> AddonQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");
            IMongoCollection<BalanceCount> AddonNameDB = database.GetCollection<BalanceCount>("BWorkName");
            AddonQuantityDB.DeleteOneAsync(d => d._Id == 4);
            AddonNameDB.DeleteOneAsync(d => d._Id == 4);
        }
        /// <summary>
        /// Permite borrar los datos asociados al documento de BalanceQuantity
        /// </summary>
        /// <param name="document">Nombre del documento</param>
        public static void DeleteBalanceCount(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> BalanceQuantityDB = database.GetCollection<BalanceCount>("BWorkQuantity");
            BalanceQuantityDB.DeleteManyAsync(d => d.File == document);
        }
        /// <summary>
        /// Permite borrar los datos asociados al documento de Balance
        /// </summary>
        /// <param name="document">Nombre del documento</param>
        public static void DeleteBalanceNames(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> BalanceNameDB = database.GetCollection<BalanceCount>("BWorkName");
            BalanceNameDB.DeleteManyAsync(d => d.File == document);
        }
        /// <summary>
        /// Permite borrar los datos asociados al documento que se le asigne
        /// </summary>
        /// <param name="document">Nombre del documento</param>
        public static void DeleteEntryData(string document)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<DataProduct> DataProductDB = database.GetCollection<DataProduct>(document);
            DataProductDB.DeleteManyAsync(d => d.OrderNumber != "");
        }
        /// <summary>
        /// Permite borrar los datos asociados al documento de IDDistinct
        /// </summary>
        /// <param name="file">Nombre del documento</param>
        public static void DeleteCountIDs(string file)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<VaseCount> IDDistinctDB = database.GetCollection<VaseCount>("BalanceIDProducts");
            IDDistinctDB.DeleteManyAsync(s => s.File == file);
        }
        /// <summary>
        /// Permite borrar los datos asociados al documento de State
        /// </summary>
        public static void DeleteStateStages()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<StateStage> StateStageDB = database.GetCollection<StateStage>("BWorkState");
            StateStageDB.DeleteManyAsync(d => d._Id != 0);
        }
        /// <summary>
        /// Permite borrar los datos asociados al documento de IDStages
        /// </summary>
        public static void DeleteIDStages()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDStages> StateStageDB = database.GetCollection<IDStages>("BalanceIDStages");
            StateStageDB.DeleteManyAsync(d => d.Vase != "");
        }
        /// <summary>
        /// Permite borrar los datos asociados al documento de IDAddOn
        /// </summary>
        public static void DeleteIDAddOn()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<IDAddOn> StateStageDB = database.GetCollection<IDAddOn>("BalanceIDAddOn");
            StateStageDB.DeleteManyAsync(d => d._Id != 0);
        }


    }
}
