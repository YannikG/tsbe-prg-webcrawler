using System;
using Newtonsoft.Json;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
	public class ObjectToFileProcessor
	{
		public ObjectToFileProcessor()
		{
		}

		public void Process<T>(T obj, string fileName)
		{
			var jsonString = JsonConvert.SerializeObject(obj);

			File.WriteAllText($"{fileName}.json", jsonString);
		}
	}
}

