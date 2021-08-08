using System;

using Crawler.Implementation;

namespace Crawler
{
	class Program
	{
		static void Main()
		{
			// This example crawls this website and downloads
			// all images on the website and all pages that
			// begins with the url provided.
			const string website = "https://useum.org/";

			// These rules only allows urls that begins with the
			// website, which should be every page on the site.
			var crawlerRules = new ImageCrawlerRules(website);
			var crawlerProcessorRules = new ImageCrawlerProcesserRules();

			// Create a processer with rules on how images are handled.
			var crawlerProcesser = new ImageCrawlerProcesser(crawlerProcessorRules, @"F:\Pictures\Crawler3");

			// Only download images with the following extensions.
			crawlerProcessorRules.AllowedExtensions.Add(".png");
			crawlerProcessorRules.AllowedExtensions.Add(".jpg");
			crawlerProcessorRules.AllowedExtensions.Add(".jpeg");

			// We only want pictures larger than 300x300.
			// This is checked after downloading the image.
			crawlerProcessorRules.MinWidth = 100;
			crawlerProcessorRules.MinHeight = 100;

			// Create a crawler with the rules we have created above, and a thread
			// for each processor core.
			using (var crawler = new ImageCrawler(crawlerRules, crawlerProcesser, Environment.ProcessorCount))
			{
				// Start the crawler.
				crawler.Start(new Uri(website));

				// The crawler should now be crawling ;)
				Console.WriteLine("Crawler started, press a key to close...");
				Console.ReadLine();
			}
		}
	}
}