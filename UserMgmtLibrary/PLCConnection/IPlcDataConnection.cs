using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XFOPI_Library.Models;

namespace XFOPI_Library.PLCConnection
{
    public interface IPlcDataConnection
    {
        //Task<Dictionary<string, RlistDataModel>> GetPlcDataAsync(int timeout_ms, CancellationToken ct, IProgress<ProgressReportModel> progress);
        Task<bool> GetPlcDataAsync(int timeout_ms,
            SortedList<int, int> addrDM, SortedList<int, int> addrHR, SortedList<int, int> addrWR, 
            CancellationToken ct, IProgress<PlcProgressReportModel> progress);
        Task<bool> WriteDataToPlcAsync(Dictionary<string, string> Wlist, IProgress<PlcProgressReportModel> progress);
        void SetPort(SerialPort port);
    }
}
