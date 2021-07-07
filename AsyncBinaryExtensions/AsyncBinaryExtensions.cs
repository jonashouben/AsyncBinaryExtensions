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

				if (readBytes == 0) throw new EndOfStreamException();

				remainingBytes -= readBytes;
			}

			return buffer;
		}
	}
}
