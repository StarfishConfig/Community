using System.Net;

namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Provides extension methods for network-related operations.
/// </summary>
public static class NetworkExtensions
{
	extension(IPAddress source)
	{
		/// <summary>
		/// Determines whether the specified IP address is within the given subnet.
		/// </summary>
		/// <param name="subnetAddress"></param>
		/// <param name="prefixLength"></param>
		/// <returns></returns>
		public bool IsInSubnet(IPAddress subnetAddress, int prefixLength)
		{
			if (subnetAddress.AddressFamily != source.AddressFamily)
			{
				return false;
			}

			var addressBytes = source.GetAddressBytes();
			var subnetBytes = subnetAddress.GetAddressBytes();

			var byteCount = prefixLength / 8;
			var bitCount = prefixLength % 8;

			for (var i = 0; i < byteCount; i++)
			{
				if (addressBytes[i] != subnetBytes[i])
				{
					return false;
				}
			}

			if (bitCount <= 0)
			{
				return true;
			}

			var mask = 0xFF << (8 - bitCount);
			return (addressBytes[byteCount] & mask) == (subnetBytes[byteCount] & mask);
		}

		/// <summary>
		/// Determines whether the specified IP address is within the given subnet in CIDR notation.
		/// </summary>
		/// <param name="subnetCidr"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public bool IsInSubnet(string subnetCidr)
		{
			var parts = subnetCidr.Split('/');
			if (parts.Length != 2)
			{
				throw new ArgumentException("Invalid CIDR notation.", nameof(subnetCidr));
			}

			var subnetAddress = IPAddress.Parse(parts[0]);
			var prefixLength = int.Parse(parts[1]);

			return source.IsInSubnet(subnetAddress, prefixLength);
		}
	}
}