using System;
namespace YannikG.TSBE.Webcrawler.Core.Collectors.Handlers.FileSystem
{
	public class FileSystemHandler
	{
		public bool CheckIfExists(string fileNameAndPath) => File.Exists(fileNameAndPath);

	}
}

