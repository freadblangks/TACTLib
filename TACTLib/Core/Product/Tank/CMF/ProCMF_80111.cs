using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
	[ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
	public class ProCMF_80111 : ICMFEncryptionProc
	{
		public byte[] Key(CMFHeader header, int length)
		{
			byte[] buffer = new byte[length];
			uint kidx, okidx;
			kidx = okidx = Keytable[header.m_dataCount & 511];
			for (uint i = 0; i != length; ++i)
			{
				buffer[i] = Keytable[SignedMod(kidx, 512)];
				kidx = header.m_buildVersion - kidx;
			}
			return buffer;
		}

		public byte[] IV(CMFHeader header, byte[] digest, int length)
		{
			byte[] buffer = new byte[length];
			uint kidx, okidx;
			kidx = okidx = Keytable[header.m_buildVersion & 511];
			for (int i = 0; i != length; ++i)
			{
				buffer[i] = Keytable[SignedMod(kidx, 512)];
				kidx += (uint)header.m_dataCount + digest[SignedMod(header.m_dataCount, SHA1_DIGESTSIZE)];
				buffer[i] ^= digest[SignedMod(header.m_buildVersion + i, SHA1_DIGESTSIZE)];
			}
			return buffer;
		}

		private static readonly byte[] Keytable =
		{
			0x50, 0x28, 0xEA, 0x47, 0xDA, 0xD7, 0x5B, 0x51, 0x2F, 0xA1, 0x3A, 0x92, 0x82, 0x92, 0x9E, 0x24, 
			0x5F, 0x79, 0x66, 0x01, 0xAF, 0xAE, 0x68, 0x4F, 0x2C, 0xF1, 0xCC, 0xE4, 0x43, 0xEC, 0x98, 0x35, 
			0xE4, 0x75, 0x3E, 0x77, 0x92, 0xE0, 0x77, 0x0F, 0x02, 0x35, 0x1E, 0x4E, 0xCF, 0xD2, 0x07, 0x1E, 
			0x30, 0xD3, 0xC3, 0x1C, 0x02, 0xAF, 0x8C, 0x54, 0xCF, 0x65, 0x5B, 0x91, 0xBB, 0x43, 0x99, 0xE8, 
			0x0C, 0x95, 0xF5, 0xC2, 0xBA, 0xB0, 0xD3, 0xD9, 0xAF, 0x65, 0x02, 0x03, 0xBE, 0xC2, 0xAD, 0x5C, 
			0x44, 0x14, 0x54, 0xD4, 0x42, 0x70, 0x44, 0xDC, 0xF1, 0x55, 0x65, 0xDD, 0x1B, 0x0B, 0xE6, 0x0F, 
			0x29, 0xCC, 0xE2, 0x79, 0x24, 0x19, 0xBC, 0x69, 0xAC, 0xC1, 0x50, 0xE5, 0x22, 0xF6, 0xA4, 0xF7, 
			0x39, 0xF5, 0xCE, 0x94, 0x96, 0xE4, 0xC2, 0x79, 0x0B, 0x71, 0x30, 0x4A, 0x51, 0x44, 0xBD, 0x4C, 
			0xFB, 0x97, 0xF6, 0x71, 0x27, 0x6F, 0xC5, 0x93, 0x20, 0x34, 0x66, 0xBC, 0xE1, 0x5A, 0xFC, 0x6D, 
			0x88, 0xB5, 0x56, 0x7B, 0x6E, 0xD9, 0xC1, 0xE3, 0xDA, 0x2D, 0x37, 0xC0, 0xDB, 0x85, 0x5F, 0x9F, 
			0xAE, 0x19, 0x13, 0x48, 0x36, 0xE2, 0x86, 0x9D, 0xB6, 0xDD, 0xAD, 0x0E, 0xAC, 0xB2, 0x80, 0x00, 
			0x05, 0xF8, 0x6D, 0x17, 0x3C, 0x4E, 0x41, 0xDC, 0x56, 0x9E, 0xF8, 0xFE, 0x8F, 0x1F, 0x69, 0xCF, 
			0x8B, 0x31, 0x1D, 0x66, 0x82, 0x48, 0x44, 0x66, 0x73, 0x7A, 0x64, 0x6C, 0x57, 0xAF, 0xD7, 0x4D, 
			0x97, 0x36, 0x4B, 0x71, 0xBC, 0x25, 0xB8, 0x61, 0x24, 0xCE, 0xE3, 0xCE, 0xBF, 0x82, 0xBA, 0xAC, 
			0xA5, 0xA9, 0x2A, 0x3B, 0x92, 0x66, 0x0D, 0xDB, 0xE3, 0x43, 0x1D, 0x4B, 0x70, 0x32, 0xD0, 0x8C, 
			0xA9, 0xD5, 0x1E, 0x60, 0xD0, 0xBF, 0xE2, 0x87, 0xA7, 0x80, 0x88, 0xE9, 0x6E, 0x15, 0x95, 0x90, 
			0x92, 0xBB, 0x49, 0x1B, 0xD8, 0x4D, 0x0C, 0xE9, 0x21, 0x46, 0x4D, 0x22, 0xD5, 0xC6, 0xA2, 0xC1, 
			0xEF, 0xA2, 0x90, 0x3F, 0x5B, 0x4C, 0xEA, 0x5B, 0x5D, 0x17, 0xF8, 0x81, 0x9A, 0x6A, 0xDE, 0x7E, 
			0x2A, 0x22, 0x65, 0x5A, 0x9A, 0x46, 0xB3, 0x90, 0xFD, 0x32, 0x6C, 0xB9, 0x4A, 0x03, 0x28, 0x6E, 
			0xC9, 0x65, 0x14, 0xFB, 0xA7, 0xB0, 0x4F, 0xE4, 0x58, 0xA6, 0x83, 0x2F, 0x45, 0xE2, 0xA2, 0x3E, 
			0x6B, 0xA0, 0xBC, 0x03, 0x0C, 0xD1, 0x8F, 0x84, 0x17, 0xBF, 0xF8, 0xC0, 0x30, 0x2C, 0xE7, 0xFF, 
			0x79, 0x22, 0x42, 0xB0, 0x37, 0xBC, 0x28, 0x2A, 0x26, 0xBE, 0x2D, 0xAB, 0xFA, 0x47, 0xAF, 0x6B, 
			0xC5, 0xAE, 0x21, 0xC2, 0xB4, 0x71, 0xC6, 0x52, 0x4B, 0xF3, 0xEF, 0xAA, 0x81, 0x4B, 0xD0, 0x4B, 
			0xB6, 0xA4, 0xDF, 0x9C, 0xD3, 0xBF, 0x3D, 0xD7, 0xC1, 0xA2, 0xE7, 0x89, 0x99, 0x7E, 0x09, 0x44, 
			0x7A, 0x64, 0x28, 0x14, 0x6D, 0x00, 0xE1, 0x42, 0x80, 0x19, 0xC0, 0x9C, 0xB2, 0x21, 0x0A, 0xDF, 
			0x19, 0xE3, 0xAB, 0x81, 0xAD, 0x37, 0x5D, 0x05, 0x5D, 0xDA, 0x65, 0x38, 0xC7, 0x94, 0x13, 0xA5, 
			0x74, 0x24, 0x4F, 0x55, 0xF8, 0x24, 0xDF, 0x40, 0xB1, 0xF7, 0x94, 0x73, 0x92, 0x1A, 0x53, 0x10, 
			0x4E, 0xAF, 0x42, 0xBC, 0x23, 0x7E, 0xF0, 0x2E, 0x1D, 0x5C, 0x57, 0xD0, 0x96, 0x38, 0x75, 0x73, 
			0xE5, 0x2B, 0x08, 0x1C, 0xD1, 0x7F, 0xC5, 0xDB, 0xC1, 0x9E, 0xCF, 0x2B, 0x7E, 0xF3, 0x21, 0x12, 
			0x7F, 0x20, 0x3C, 0x5C, 0x5C, 0x21, 0x84, 0x13, 0x88, 0xF3, 0xFB, 0x8D, 0xB8, 0xC9, 0xBD, 0x5D, 
			0xBC, 0x55, 0x36, 0xEA, 0xA8, 0x8B, 0x50, 0xC3, 0x1B, 0xB0, 0x2E, 0xDE, 0x96, 0x58, 0x3E, 0x0E, 
			0x59, 0xD5, 0x36, 0x3E, 0xFF, 0x19, 0x3B, 0xE7, 0x6B, 0xC0, 0x5E, 0xA5, 0x5E, 0xB1, 0xAB, 0x6D
		};
	}
}
