using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncBinaryExtensions
{
	public static class AsyncBinaryExtensions
	{
		public static async ValueTask<byte[]> ReadBytesAsync(this Stream stream, int count, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new InvalidOperationException();
			if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
			if (count == 0) return new byte[0];
			cancellationToken.ThrowIfCancellationRequested();

			byte[] buffer = new byte[count];
			int remainingBytes = count;

			while (remainingBytes > 0)
			{
				cancellationToken.ThrowIfCancellationRequested();

				int readBytes = await stream.ReadAsync(buffer, count - remainingBytes, remainingBytes, cancellationToken).ConfigureAwait(false);

				if (readBytes == 0)
				{
					await Task.Delay(10, cancellationToken).ConfigureAwait(false);
				}
				else
				{
					remainingBytes -= readBytes;
				}
			}

			return buffer;
		}

		public static async Task<byte> ReadByteAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return (await stream.ReadBytesAsync(1, cancellationToken).ConfigureAwait(false))[0];
		}

		public static async Task<sbyte> ReadSByteAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return (sbyte) (await stream.ReadBytesAsync(1, cancellationToken).ConfigureAwait(false))[0];
		}

		public static async Task<short> ReadShortAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return BitConverter.ToInt16(await stream.ReadBytesAsync(2, cancellationToken).ConfigureAwait(false), 0);
		}

		public static async Task<ushort> ReadUShortAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return BitConverter.ToUInt16(await stream.ReadBytesAsync(2, cancellationToken).ConfigureAwait(false), 0);
		}

		public static async Task<int> ReadIntAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return BitConverter.ToInt32(await stream.ReadBytesAsync(4, cancellationToken).ConfigureAwait(false), 0);
		}

		public static async Task<uint> ReadUIntAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return BitConverter.ToUInt32(await stream.ReadBytesAsync(4, cancellationToken).ConfigureAwait(false), 0);
		}

		public static async Task<long> ReadLongAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return BitConverter.ToInt64(await stream.ReadBytesAsync(8, cancellationToken).ConfigureAwait(false), 0);
		}

		public static async Task<ulong> ReadULongAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return BitConverter.ToUInt64(await stream.ReadBytesAsync(8, cancellationToken).ConfigureAwait(false), 0);
		}

		public static async Task<float> ReadFloatAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return BitConverter.ToSingle(await stream.ReadBytesAsync(4, cancellationToken).ConfigureAwait(false), 0);
		}

		public static async Task<double> ReadDoubleAsync(this Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			return BitConverter.ToDouble(await stream.ReadBytesAsync(8, cancellationToken).ConfigureAwait(false), 0);
		}
	}
}
