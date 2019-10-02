using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XFOPI_Library.Models;

namespace XFOPI_Library.PLCConnection
{
    public class DataWorker
    {
        public DataWorker()
        {

        }
        public async void RunDataExchange(int timeout, int timeDelay, CancellationToken ct, IProgress<PlcProgressReportModel> progress)
        {
            bool canceled = false;
            double totalSteps = 2;
            PlcProgressReportModel report = new PlcProgressReportModel();
            //PlcProgressReportModel readResult = new PlcProgressReportModel();

            while (true)
            {
                DateTimeOffset startTime = DateTimeOffset.Now;
                report.PercetageComplete =(int) (0 / totalSteps * 100);
                report.ReadCompleted = false;
                report.WrtCompleted = false;
                report.Msg = "Begining to read..";
                progress.Report(report);

                if (ct.IsCancellationRequested)
                {
                    canceled = true;
                    break;
                }

                //1.Get data from PLC all areas then send to UI for updating display
                try
                {
                    //1.Get data from PLC to R list
                    bool rdFinished = await GlobalConfig.DataConnection.GetPlcDataAsync(timeout,
                        PlcDataMapper.addrIntervalDM, PlcDataMapper.addrIntervalHR, PlcDataMapper.addrIntervalWR,
                        ct, progress);
                    if (!rdFinished)
                    {
                        report.PercetageComplete = (int)(0 / totalSteps * 100);
                        report.Msg = "Reading failed.. pls retry";
                        progress.Report(report);
                        break;
                    }
                    report.PercetageComplete = (int) (1 / totalSteps * 100);
                    report.Msg = "Reading finished.. Begining to write..";
                    progress.Report(report);

                    //2.Get updated data from UI
                    //SimulateWlist();


                    var elapsedTime = DateTimeOffset.Now - startTime;
                    int timeLeft = timeout - (int)elapsedTime.TotalMilliseconds;

                    //3.Write updated data to PLC
                    bool wrtFinished = await GlobalConfig.DataConnection.WriteDataToPlcAsync(PlcDataMapper.DataNameValue_Wlist, progress);
                    report.PercetageComplete = (int) (2 / totalSteps * 100);
                    
                    report.Msg = "Writing finished.. Begining next cycle..";
                    progress.Report(report);

                    await Task.Delay(timeDelay);

                }
                catch (TimeoutException ex)
                {
                    break;
                }
                catch (OperationCanceledException ex)
                {
                    //report.ClrReport();
                    //report.Msg = "The operation has been canceled";
                    //progress.Report(report);
                    break;
                }
                catch (NotImplementedException ex)
                {   
                    //report.ClrReport();
                    //report.Msg = ex.Message;
                    //progress.Report(report);
                    break;
                }
                catch (Exception)
                {
                    break;
                }


            }
            if (canceled)
            {
                report.Msg = "The R/W operation has canceled";
                progress.Report(report);
                //throw new OperationCanceledException();
            }

            //2.Collect UI data changed from last cycle and send to PLC for update mem








        }

        private void SimulateWlist()
        {
            int i = 0;
            for (int j = 0; j < 10; j++)
            {
                PlcDataMapper.DataNameValue_Wlist.Add(PlcDataMapper.TypeBools[j].Name, "1");
                PlcDataMapper.DataNameValue_Wlist.Add(PlcDataMapper.TypeUshorts[j].Name, j.ToString());
                PlcDataMapper.DataNameValue_Wlist.Add(PlcDataMapper.TypeUints[j].Name, j.ToString());

            }
            //foreach (var typeBool in PlcDataMapper.TypeBools)
            //{
            //    PlcDataMapper.DataNameValue_Wlist.Add(typeBool.Name, "1");
            //}
            //foreach (var typeUshort in PlcDataMapper.TypeUshorts)
            //{
            //    PlcDataMapper.DataNameValue_Wlist.Add(typeUshort.Name, i.ToString());
            //    i++;
            //}
            //foreach (var typeUint in PlcDataMapper.TypeUints)
            //{
            //    PlcDataMapper.DataNameValue_Wlist.Add(typeUint.Name, i.ToString());
            //    i++;
            //}
        }
    }
}
