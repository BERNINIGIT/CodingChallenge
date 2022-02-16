using FirstReact.Core.Models.Dtos;
using System.Collections.Generic;
using System.IO;

namespace FirstReact.Core.Services.Contracts
{
    public interface IFileProcessor
    {
        List<CarSale> ProcessCsvFile(Stream stream);
    }
}
