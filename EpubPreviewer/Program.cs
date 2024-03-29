﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SanderSade.EpubPreviewer.App;
using SanderSade.EpubPreviewer.Epub;

namespace SanderSade.EpubPreviewer
{
	internal static class Program
	{
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(params string[] files)
		{
			Initialize();

			if (files?.Length > 0)
			{
				foreach (var file in files)
				{
					Previewer.Preview(file);
				}

				//run cleanup for old temp ebooks
				Cleanup.Run(3);

				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ConfigForm());
		}


		private static void Initialize()
		{
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

			Application.ThreadException += (_, args) =>
				LogUnhandledException(args.Exception, "Uncaught thread exception");

			AppDomain.CurrentDomain.UnhandledException += (sender, args) => LogUnhandledException(
				args.ExceptionObject as Exception,
				$"Uncaught exception: {sender}, terminating: {args.IsTerminating}");

			TaskScheduler.UnobservedTaskException += (sender, args) =>
			{
				LogUnhandledException(args.Exception,
					$"Uncaught task exception: {sender}");

				args.SetObserved();
			};
		}


		private static void LogUnhandledException(Exception ex, string message)
		{
			MessageBox.Show($"Fatal error. {message}\r\n\r\n{ex}",
				"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

			Environment.Exit(1);
		}
	}
}
