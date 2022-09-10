using System;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using YannikG.TSBE.Webcrawler.Core.Models;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
	public class ObjectToCSVProcessor
	{
		public ObjectToCSVProcessor()
		{

		}

		public async Task<string> Process<T>(List<T> obj)
		{
            StringWriter csvString = new StringWriter();

            using (var csvWriter = new CsvWriter(csvString, CultureInfo.CurrentCulture))
			{
                csvWriter.WriteHeader<T>();

                csvWriter.NextRecord();

                await csvWriter.WriteRecordsAsync<T>(obj);
            }

            return csvString.ToString();
        }
    }
}

