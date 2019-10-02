using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using XFOPI_Library.Models;
using System.Threading;
using System.Diagnostics;

namespace XFOPI_Library.PLCConnection 
{
    //This is the event publisher. UI update service is the Event Subscriber
    public class OmronFINsPlcConnector : IPlcDataConnection
    {
        public SerialPort mainPort { get; set; }


        public OmronFINsPlcConnector(SerialPort mainPort)
        {
            this.mainPort = mainPort;
            if (!mainPort.IsOpen)
            {
                try
                {
                    mainPort.Open();
                }
                catch (Exception)
                {   
                }
                
            }   
        }

        public OmronFINsPlcConnector()
        {

        }

        //1- Define the Event Handler (delegate)
        //public delegate void PlcReadingCompletedEventHandler(object source, EventArgs args);

        //2- Define an event based on the Event Handler
        //public event PlcReadingCompletedEventHandler PlcReadingCompleted;

        //Do steps 1,2 in one line
        public event EventHandler<PlcFinishReadEventArgs> PlcReadingCompleted;
        //public event EventHandler<PlcFinishReadEventArgs> PlcReadingProgressChanged;
        public event EventHandler<PlcFinishWriteEventArgs> PlcWritingCompleted;
        //public event EventHandler<PlcFinishReadEventArgs> PlcWritingProgressChanged;

        //3- Raise the Event
        //protected virtual void OnPlcReadingCompleted(List<PlcWordModel> receivedData)
        //{
        //    //if (PlcReadingCompleted!=null)
        //    //{
        //    //    PlcReadingCompleted(this, new PlcDataEventArgs() { PlcData= receivedData});
        //    //}
        //    PlcReadingCompleted?.Invoke(this, new PlcDataEventArgs() { BitsSent = WrtBitList,
        //                WordsSent = WrtWordList, ExpMsgBit = expMsgBit, ExpMsgWord = expMsgWord });
        //}
        //protected virtual void OnPlcReadingProgressChanged(int currentValue)
        //{
        //    PlcReadingProgressChanged?.Invoke(this, new PlcDataEventArgs() { CurrentValue = currentValue });
        //}


        //The actual method to read PLC
        //public void ReadPlc_AllData(SerialPort port)
        //{
        //    List<PlcWordModel> receivedData = new List<PlcWordModel>();
        //    //read all words from PLC as string
        //    //put string into words
        //    //return list words
        //    OnPlcReadingCompleted(receivedData);
        //}

        /// <summary>
        /// Read all data in the PLC W,H,D area, then Write the UI data to PLC every t_interval ms 
        /// </summary>
        /// <param name="t_interval">time between each read</param>
        /// <param name="cancellationToken">Cancel operation token</param>
        /// <returns>Async Task</returns>
        public async Task PlcDataExchangeAllAsync(int t_interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    //1.Read all from PLC to Words
                    //List<PlcWordModel> retDataW = new List<PlcWordModel>();
                    Stopwatch stopwatch1 = new Stopwatch();
                    Stopwatch stopwatch2 = new Stopwatch();
                    stopwatch1.Start();
                    OmronFINsProcessor.ReadAllWords(mainPort, out long t_ReadAll);//>1s


                    //stopwatch1.Start();
                    //OmronFINsProcessor.WordsToBits(PlcMemArea.WR);//>2s
                    //stopwatch1.Stop();
                    //OmronFINsProcessor.WordsToBits(PlcMemArea.HR);

                    //OmronFINsProcessor.ReadAllDMWords(mainPort);

                    //stopwatch1.Start();
                    //OmronFINsProcessor.ReadAllBits(mainPort);
                    //stopwatch1.Stop();
                    //2.Transfer value from Words/bits to DataNames
                    
                    PlcDataMapper.DMWordsToData();//4ms
                    
                    PlcDataMapper.BitsToData();//14ms
                    
                    //3. Transfer from DataNames to RWlist then Raise EventArgs and let UI to retrieve the data
                    //UI will subscribe to this event and retrieve data from RWlist
                    
                    lock (PlcDataMapper._RWlistLock)
                    {
                        PlcDataMapper.DataToRList();
                    }
                    stopwatch1.Stop();

                    PlcReadingCompleted.Raise(this, new PlcFinishReadEventArgs(){ ticks=stopwatch1.ElapsedTicks, _ms=stopwatch1.ElapsedMilliseconds } );

                    //SimulateWlist();//Simulate the test data

                    
                    //new 4. generate the write words and bits lists based on Wlist only 
                    List<PlcWordModel> WrtWordList = new List<PlcWordModel>();
                    List<PlcBitModel> WrtBitList = new List<PlcBitModel>();

                    if (PlcDataMapper.DataNameValue_Wlist.Count>0)
                    {
                        lock (PlcDataMapper._WlistLock)
                        {
                            WrtWordList = PlcDataMapper.WListGenerateWordList(WrtWordList);
                            WrtBitList = PlcDataMapper.WListGenerateBitList(WrtBitList);
                            //PlcDataMapper.WListToData();
                            PlcDataMapper.DataNameValue_Wlist.Clear();
                        }
                        stopwatch2.Start();
                        //new 5. write the data individually to PLC then raise complete event
                        bool BitsWrtSuccessed = OmronFINsProcessor.WriteBits(mainPort, WrtBitList, out string expMsgBit);
                        bool WordsWrtSuccessed = OmronFINsProcessor.WriteWords(mainPort, WrtWordList, out string expMsgWord);
                        //6.Write the data to PLC then Raise the written event
                        //OmronFINsProcessor.WriteAllWords(mainPort);
                        stopwatch2.Stop();
                        PlcWritingCompleted.Raise(this, new PlcFinishWriteEventArgs()
                        {
                            _ms = stopwatch2.ElapsedMilliseconds,
                            BitsSent = WrtBitList,
                            WordsSent = WrtWordList,
                            ExpMsgBit = expMsgBit,
                            ExpMsgWord = expMsgWord
                        });
                    }
                    
                    
                });
                Thread.Sleep(t_interval);
                if (cancellationToken.IsCancellationRequested)
                    break;
            }

        }

        //private void SimulateWlist()
        //{
        //    int i = 0;
        //    foreach (var typeBool in PlcDataMapper.TypeBools)
        //    {
        //        PlcDataMapper.DataNameValue_Wlist.Add(typeBool.Name, "1");
        //    }
        //    foreach (var typeUshort in PlcDataMapper.TypeUshorts)
        //    {
        //        PlcDataMapper.DataNameValue_Wlist.Add(typeUshort.Name, i.ToString());
        //        i++;
        //    }
        //    foreach (var typeUint in PlcDataMapper.TypeUints)
        //    {
        //        PlcDataMapper.DataNameValue_Wlist.Add(typeUint.Name, i.ToString());
        //        i++;
        //    }
        //}

        public void SetPort(SerialPort port)
        {
            mainPort = port;
        }

        /// <summary>
        /// Get plc data to Rlist async
        /// </summary>
        /// <param name="timeout_ms"></param>
        /// <param name="ct"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<bool> GetPlcDataAsync(int timeout_ms, 
            SortedList<int, int> addrDM, SortedList<int, int> addrHR, SortedList<int, int> addrWR,
            CancellationToken ct, IProgress<PlcProgressReportModel> progress)
        {
            Dictionary<string, RlistDataModel> retRlist = new Dictionary<string, RlistDataModel>();
            PlcProgressReportModel report = new PlcProgressReportModel();
            DateTimeOffset startTime = DateTimeOffset.Now;
            bool timedout = false;
            bool canceled = false;
            SortedList<int, string> DMstr = new SortedList<int, string>();
            SortedList<int, string> HRstr = new SortedList<int, string>();
            SortedList<int, string> WRstr = new SortedList<int, string>();

            Stopwatch stopwatch1 = new Stopwatch();//debug watch
            Stopwatch stopwatch2 = new Stopwatch();//debug watch
            Stopwatch stopwatch3 = new Stopwatch();//debug watch
            Stopwatch stopwatch4 = new Stopwatch();//debug watch
            Stopwatch stopwatch5 = new Stopwatch();//debug watch

            if (!mainPort.IsOpen)
            {
                mainPort.Open();
            }

            //try
            //{
                await Task.Run(() =>
                {
                        stopwatch1.Start();
                        report.ReadCompleted = OmronFINsProcessor.ReadAllPlcWords(mainPort, addrDM, addrHR, addrWR,
                            out DMstr, out HRstr, out WRstr);
                        stopwatch1.Stop();

                        
                 });
            try
            {
                if (DateTimeOffset.Now.Subtract(startTime).TotalMilliseconds > timeout_ms || timedout)
                {
                    timedout = true;
                    throw new TimeoutException("reading PLC timed out..");
                }
                else if (ct.IsCancellationRequested)
                {
                    canceled = true;
                    throw new OperationCanceledException(ct);
                    //throw new Exception();
                    //ct.ThrowIfCancellationRequested();
                    //return false;
                }
            }
            catch (TimeoutException ex)
            {
                report.ClrReport();
                report.Msg = "The operation has timed out";
                progress.Report(report);
                return false;
            }
            catch (OperationCanceledException ex)
            {
                report.ClrReport();
                report.Msg = "The operation has been canceled";
                progress.Report(report);
                return false;
            }
            catch (Exception)
            {
                report.ClrReport();
                report.Msg = "Unkmown exception";
                progress.Report(report);
                return false;
            }

            stopwatch2.Start();
            lock (PlcDataMapper._WordsListLock)
            {
                OmronFINsProcessor.UpdateStringToWords(PlcMemArea.WR_bit, WRstr);
                OmronFINsProcessor.UpdateStringToWords(PlcMemArea.HR_bit, HRstr);
                OmronFINsProcessor.UpdateStringToWords(PlcMemArea.DM, DMstr);
            }
            stopwatch2.Stop();

            stopwatch3.Start();
            PlcDataMapper.DMWordsToData();//4ms
            stopwatch3.Stop();

            stopwatch4.Start();
            PlcDataMapper.BitsToData();//14ms
            stopwatch4.Stop();

            stopwatch5.Start();
            lock (PlcDataMapper._RWlistLock)
            {
                PlcDataMapper.DataToRList();
            }
            stopwatch5.Stop();
            //}
            //catch (TimeoutException ex)
            //{
            //    report.ClrReport();
            //    report.Msg = "The operation has timed out";
            //    progress.Report(report);
            //    return false;
            //}
            //catch(OperationCanceledException ex)
            //{
            //    report.ClrReport();
            //    report.Msg = "The operation has been canceled";
            //    progress.Report(report);
            //    return false;
            //}
            //catch (Exception)
            //{
            //    report.ClrReport();
            //    report.Msg = "Unkmown exception";
            //    progress.Report(report);
            //    return false;
            //}

            //report.PercetageComplete = 1 / 2 * 100;
            report.Msg = "Reading finished.. Begining to write.."+"readAllWords="+stopwatch1.ElapsedMilliseconds.ToString()
                +"..updateStrToWords"+stopwatch2.ElapsedMilliseconds.ToString()
                +"..DMwordsToData"+stopwatch3.ElapsedMilliseconds.ToString()
                +"..bitsToData"+stopwatch4.ElapsedMilliseconds.ToString()
                +"..DataToRlist"+stopwatch5.ElapsedMilliseconds.ToString();
            progress.Report(report);
            return true;
        }

        public async Task<bool> GetPlcDataAsync_old(int timeout_ms,
            SortedList<int, int> addrDM, SortedList<int, int> addrHR, SortedList<int, int> addrWR,
            CancellationToken ct, IProgress<PlcProgressReportModel> progress)
        {
            Dictionary<string, RlistDataModel> retRlist = new Dictionary<string, RlistDataModel>();
            PlcProgressReportModel report = new PlcProgressReportModel();
            DateTimeOffset startTime = DateTimeOffset.Now;
            bool timedout = false;
            bool canceled = false;
            if (!mainPort.IsOpen)
            {
                mainPort.Open();
            }

            await Task.Run(() =>
            {
                report.ReadCompleted = OmronFINsProcessor.ReadAllPlcWords(mainPort, addrDM, addrHR, addrWR,
                    out SortedList<int, string> DMstr, out SortedList<int, string> HRstr, out SortedList<int, string> WRstr);
                lock (PlcDataMapper._WordsListLock)
                {
                    OmronFINsProcessor.UpdateStringToWords(PlcMemArea.WR_bit, WRstr);
                    OmronFINsProcessor.UpdateStringToWords(PlcMemArea.HR_bit, HRstr);
                    OmronFINsProcessor.UpdateStringToWords(PlcMemArea.DM, DMstr);
                }

                PlcDataMapper.DMWordsToData();//4ms
                PlcDataMapper.BitsToData();//14ms

                lock (PlcDataMapper._RWlistLock)
                {
                    PlcDataMapper.DataToRList();
                }
            });
            report.PercetageComplete = 1 / 2 * 100;
            report.Msg = "Reading finished.. Begining to write..";
            progress.Report(report);
            return true;
        }

        /// <summary>
        /// Write Wlist to PLC
        /// </summary>
        /// <param name="Wlist">Wlist to write</param>
        /// <returns>Completed</returns>
        public async Task<bool> WriteDataToPlcAsync(Dictionary<string, string> Wlist, IProgress<PlcProgressReportModel>progress)
        {
            List<PlcWordModel> WrtWordList = new List<PlcWordModel>();
            List<PlcBitModel> WrtBitList = new List<PlcBitModel>();
            PlcProgressReportModel report = new PlcProgressReportModel();

            await Task.Run(() =>
            {
                if (PlcDataMapper.DataNameValue_Wlist.Count > 0)
                {
                    lock (PlcDataMapper._WlistLock)
                    {
                        WrtWordList = PlcDataMapper.WListGenerateWordList(WrtWordList);
                        WrtBitList = PlcDataMapper.WListGenerateBitList(WrtBitList);
                        foreach (var item in PlcDataMapper.DataNameValue_Wlist)
                        {
                            report.Wlist.Add(item.Key,item.Value);
                        }
                        //PlcDataMapper.WListToData();
                        PlcDataMapper.DataNameValue_Wlist.Clear();
                    }
                    
                    //new 5. write the data individually to PLC then raise complete event
                    bool BitsWrtSuccessed = OmronFINsProcessor.WriteBits(mainPort, WrtBitList, out string expMsgBit);
                    bool WordsWrtSuccessed = OmronFINsProcessor.WriteWords(mainPort, WrtWordList, out string expMsgWord);
                    report.WrtCompleted = BitsWrtSuccessed & WordsWrtSuccessed;
                }
            });
            report.PercetageComplete = 2 / 2 * 100;
            report.Msg = "Writing finished.. Begin next cycle..";
            
            progress.Report(report);
            return true;
        }
            
    }
}
