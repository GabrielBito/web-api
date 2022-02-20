using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Log
    {
        public static void gravar(Exception ex)
        {
            string nomeArquivoLog = DateTime.Now.ToString("yyyy-MM-dd");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter($@"{Configuration.Parameters.getCaminhoArquivoLog()}\{nomeArquivoLog}.txt", true))
            {
                sw.WriteLine(mensagem(ex));
            }
        }

        private static string mensagem(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("----------------------------");
            sb.Append("Data:");
            sb.Append(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            sb.Append("- Mensagem: ");
            sb.Append(ex.Message);
            sb.Append("StackTrace: ");
            sb.Append(ex.StackTrace);
            sb.Append("InnerException: ");
            sb.Append(ex.InnerException);
            sb.Append("------------------------------");

            return sb.ToString();

        }

        public static void gravar(string texto)
        {

        }
    }
}
