using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AsyncBinaryExtensions.Test
{
	[TestClass]
	public class AsyncBinaryExtensionsTest
	{
		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadBytesAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadBytesAsync(null, 0).ConfigureAwait(false);
			Assert.Fail();
		}

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
		public async Task ReadBytesAsync_Partial()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				byte[] data = new byte[12];
				await ms.WriteAsync(data, 0, 6).ConfigureAwait(false);
				ms.Seek(0, SeekOrigin.Begin);
				ValueTask<byte[]> resultTask = AsyncBinaryExtensions.ReadBytesAsync(ms, 12);
				await Task.Delay(100).ConfigureAwait(false);
				await ms.WriteAsync(data, 0, 6).ConfigureAwait(false);
				ms.Seek(6, SeekOrigin.Begin);
				byte[] result = await resultTask.ConfigureAwait(false);
				Assert.AreEqual(data.Length, result.Length);
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadByteAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadByteAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(byte.MinValue)]
		[DataRow(byte.MaxValue)]
		public async Task ReadByteAsync(byte value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadByteAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadSByteAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadSByteAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(sbyte.MinValue)]
		[DataRow(sbyte.MaxValue)]
		public async Task ReadSByteAsync(sbyte value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadSByteAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadShortAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadShortAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(short.MinValue)]
		[DataRow(short.MaxValue)]
		public async Task ReadShortAsync(short value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadShortAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadUShortAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadUShortAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(ushort.MinValue)]
		[DataRow(ushort.MaxValue)]
		public async Task ReadUShortAsync(ushort value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadUShortAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadIntAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadIntAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(int.MinValue)]
		[DataRow(int.MaxValue)]
		public async Task ReadIntAsync(int value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadIntAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadUIntAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadUIntAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(uint.MinValue)]
		[DataRow(uint.MaxValue)]
		public async Task ReadUIntAsync(uint value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadUIntAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadLongAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadLongAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(long.MinValue)]
		[DataRow(long.MaxValue)]
		public async Task ReadLongAsync(long value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadLongAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadULongAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadULongAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(ulong.MinValue)]
		[DataRow(ulong.MaxValue)]
		public async Task ReadULongAsync(ulong value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadULongAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadFloatAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadFloatAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(float.MinValue)]
		[DataRow(float.MaxValue)]
		public async Task ReadFloatAsync(float value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadFloatAsync(ms).ConfigureAwait(false));
			}
		}

		[TestMethod]
		[Timeout(1000)]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ReadDoubleAsync_StreamNull()
		{
			await AsyncBinaryExtensions.ReadDoubleAsync(null).ConfigureAwait(false);
			Assert.Fail();
		}

		[DataTestMethod]
		[Timeout(1000)]
		[DataRow(double.MinValue)]
		[DataRow(double.MaxValue)]
		public async Task ReadDoubleAsync(double value)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(BitConverter.GetBytes(value));
				ms.Seek(0, SeekOrigin.Begin);
				Assert.AreEqual(value, await AsyncBinaryExtensions.ReadDoubleAsync(ms).ConfigureAwait(false));
			}
		}
	}
}
