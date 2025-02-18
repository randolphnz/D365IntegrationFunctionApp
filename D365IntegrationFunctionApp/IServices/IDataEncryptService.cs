using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365IntegrationFunctionApp.IServices
{
    public interface IDataEncryptService
    {
        void EncryptCsvFile(string inputFilePath, string outputFilePath, string password);
    }
}
