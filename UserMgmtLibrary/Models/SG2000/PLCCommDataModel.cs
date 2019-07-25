using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    public class PLCCommDataModel
    {
        public string[] TopW { get; set; }
        public string[] BottomW { get; set; }
        public string[] TopH { get; set; }
        public string[] BottomH { get; set; }
        public string[] AllW { get; set; }
        public string[] AllH { get; set; }
        public string[] AllD { get; set; }

        public List<PlcWordModel> RetData_W { get; set; } = new List<PlcWordModel>();
        public List<PlcWordModel> RetData_H { get; set; } = new List<PlcWordModel>();
        public List<PlcWordModel> RetData_D { get; set; } = new List<PlcWordModel>();

        public PLCCommDataModel()
        {

        }

        public void GetRetValue()
        {
            for (int i = 0; i < AllW.Length; i++)
            {
                PlcWordModel temp_word = new PlcWordModel();
                temp_word.MemoryArea = "W";
                temp_word.WordAddress = i;
                temp_word.SetValue(AllW[i]);
                RetData_W.Add(temp_word);
            }
            for (int i = 0; i < AllH.Length; i++)
            {
                PlcWordModel temp_word = new PlcWordModel();
                temp_word.MemoryArea = "H";
                temp_word.WordAddress = i;
                temp_word.SetValue(AllH[i]);
                RetData_H.Add(temp_word);
            }
            for (int i = 0; i < AllD.Length; i++)
            {
                PlcWordModel temp_word = new PlcWordModel();
                temp_word.MemoryArea = "D";
                temp_word.WordAddress = i;
                temp_word.SetValue(AllD[i]);
                RetData_D.Add(temp_word);
            }
        }


    }
}
