using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncBinaryExtensions.Test
{
	[TestClass]
	public class AsyncBinaryExtensionsTest
	{
		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task ReadBytesAsync_StreamNotReadable()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Close();
				await AsyncBinaryExtensions.ReadBytesAsync(ms, 0).ConfigureAwait(false);
				Assert.Fail();
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public async Task ReadBytesAsync_CountBelowZero()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				await AsyncBinaryExtensions.ReadBytesAsync(ms, -1).ConfigureAwait(false);
				Assert.Fail();
			}
		}

		[TestMethod]
		[Timeout(1000)]
		public async Task ReadBytesAsync_CountZero()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				byte[] result = await AsyncBinaryExtensions.ReadBytesAsync(ms, 0).ConfigureAwait(false);
				Assert.AreEqual(0, result.Length);
			}
		}

		[TestMethod]
		[Timeout(1000)]
		public async Task ReadBytesAsync()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				byte[] data = Guid.NewGuid().ToByteArray();
				await ms.WriteAsync(data).ConfigureAwait(false);
				ms.Seek(0, SeekOrigin.Begin);
				byte[] result = await AsyncBinaryExtensions.ReadBytesAsync(ms, data.Length).ConfigureAwait(false);
				Assert.IsTrue(data.SequenceEqual(result));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(EndOfStreamException))]
		public async Task ReadBytesAsync_Partial()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				byte[] data = Guid.NewGuid().ToByteArray();
				await ms.WriteAsync(data, 0, 8).ConfigureAwait(false);
				ms.Seek(0, SeekOrigin.Begin);
				await AsyncBinaryExtensions.ReadBytesAsync(ms, data.Length);
				Assert.Fail();
			}
		}
	}
}
