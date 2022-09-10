using System;
using Newtonsoft.Json;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
	public class ObjectToJsonProcessor
	{
		public ObjectToJsonProcessor()
		{
		}

		public string Process<T>(List<T> obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}

