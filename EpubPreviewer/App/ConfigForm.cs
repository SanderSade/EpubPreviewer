using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SanderSade.EpubPreviewer.Epub;

namespace SanderSade.EpubPreviewer.App
{
	public partial class ConfigForm : Form
	{
		public ConfigForm()
		{
			InitializeComponent();
		}

		private void btDeleteTemporary_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			Cleanup.Run(0);
			Cursor = Cursors.Arrow;
		}

		private void lbLink_Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/SanderSade/EpubPreviewer");
		}

		private void ConfigForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void ConfigForm_DragDrop(object sender, DragEventArgs e)
		{
			var files = (string[])e.Data.GetData(DataFormats.FileDrop);
			Cursor = Cursors.WaitCursor;
			var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
			RunPreview(files, parallelOptions);

			Cursor = Cursors.Arrow;
		}

		private void RunPreview(string[] files, ParallelOptions parallelOptions)
		{
			Parallel.ForEach(files, parallelOptions, file =>
			{
				if (Directory.Exists(file))
				{
					var epubs = Directory.GetFiles(file, "*.epub", SearchOption.AllDirectories);
					RunPreview(epubs, parallelOptions);
				}
				else if (File.Exists(file))
				{
					try
					{
						Previewer.Preview(file);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Error opening file {file}, \r\n\r\n{ex}", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			});
		}
	}
}
