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
        public async void RunDataExchange(int timeout, int timeDelay, CancellationToken ct, IProgress<ProgressReportModel> progress)
        {
            bool canceled = false;
            int totalSteps = 2;
            ProgressReportModel report = new ProgressReportModel();

            //SortedList<int,int> addrIntervalWR=
            
            
            while (true)
            {
                DateTimeOffset startTime = DateTimeOffset.Now;
                
                

                report.PercetageComplete = 0 / totalSteps * 100;
                report.ReadCompleted = false;
                report.WrtCompleted = false;
                report.Msg = "Begining to read..";
                progress.Report(report);

                if (ct.IsCancellationRequested)
                {
                    canceled = true;
                    break;
                }

                try
                {
                    bool rdFinished = await GlobalConfig.DataConnection.GetPlcDataAsync(timeout,
                        PlcDataMapper.addrIntervalDM, PlcDataMapper.addrIntervalHR, PlcDataMapper.addrIntervalWR, 
                        ct, progress);
                    //report.PercetageComplete = 1 / totalSteps * 100;
                    //report.Msg = "Reading finished.. Begining to write..";
                    //progress.Report(report);

                    SimulateWlist();
                    
                    
                    var elapsedTime = DateTimeOffset.Now - startTime;
                    int timeLeft = timeout - (int)elapsedTime.TotalMilliseconds;

                    bool wrtFinished = await GlobalConfig.DataConnection.WriteDataToPlcAsync(PlcDataMapper.DataNameValue_Wlist,progress);
                    //report.PercetageComplete = 2 / totalSteps * 100;
                    //report.Msg = "Writing finished.. Begining next cycle..";
                    //progress.Report(report);

                    await Task.Delay(timeDelay);

                }
                catch (Exception ex)
                {

                }
            }
            if (canceled)
            {
                report.Msg = "The R/W operation has canceled";
                progress.Report(report);
                //throw new OperationCanceledException();
            }
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
