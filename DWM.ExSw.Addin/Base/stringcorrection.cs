using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DWM.ExSw.Addin.Base
{
    public class stringcorrection
    {
        public string RemoveBarrainvertida(string stringvalue)
        {
            string newstring="";
            for (int i = 0; i < stringvalue.Length; i++)
            {
                if (stringvalue[i] == '\'')
                {
                    if(i+1 < stringvalue.Length)
                    {
                        if (stringvalue[i + 1] != '\'')
                        {
                            newstring = newstring + "\"";
                        }
                    }
                    else
                    {
                        newstring = newstring + "\"";
                    }
                    
                    
                }
                else
                {
                    newstring = newstring + stringvalue[i].ToString();
                }
                
            }

            return newstring;
        }
    }
}
