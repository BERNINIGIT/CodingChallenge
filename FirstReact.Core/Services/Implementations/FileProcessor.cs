using FirstReact.Core.Models.Dtos;
using FirstReact.Core.Services.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FirstReact.Core.Services.Implementations
{
    public class FileProcessor : IFileProcessor
    {
        private readonly ILogger<FileProcessor> _logger;

        public FileProcessor(ILogger<FileProcessor> logger)
        {
            _logger = logger;
        }

        public List<CarSale> ProcessCsvFile(Stream stream)
        {
            List<CarSale> result = new List<CarSale>();
            try
            {                
                using (StreamReader reader = new StreamReader(stream, Encoding.Latin1))
                {
                    var header = reader.ReadLine();//header
                    //reader.
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var columns = JoinFieldWithCommas(line.Split(','));
                        result.Add(new CarSale
                        {
                            DealNumber = columns[0],
                            CustomerName = columns[1],
                            DealershipName = columns[2],
                            Vehicle = columns[3],
                            Price = $"{columns[4].Replace(",",".")}",
                            Date = columns[5]
                        });
                    }
                }
                return result;
            }
            catch(Exception e)
            {
                _logger.LogError($"An error has occurred when processing csv file: {e.Message}");
                throw;
            }
        }
        public string[] JoinFieldWithCommas(string[] items)
        {
            List<string> fields = new List<string>();
            bool complete = true;
            StringBuilder currentItem = new StringBuilder();
            foreach (var item in items)
            {
                if ((!complete && item[0] == '\"') || (complete && item[item.Length - 1] == '\"'))
                    throw new ArgumentException();
                if (item[0] != '\"' && complete)
                {
                    fields.Add(item);
                    continue;
                }
                else
                {
                    complete = false;
                }
                if (item[item.Length - 1] == '\"' && !complete)
                {
                    currentItem.Append(item.Replace("\"", ""));
                    fields.Add(currentItem.ToString());
                    currentItem = new StringBuilder();
                    complete = true;
                    continue;
                }
                if (!complete)
                {
                    currentItem.Append(item.Replace("\"", "") + ",");
                }
            }
            return fields.ToArray();
        }
    }
}
