using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
	[ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
	public class ProCMF_83050 : ICMFEncryptionProc
	{
		public byte[] Key(CMFHeader header, int length)
		{
			byte[] buffer = new byte[length];
			uint kidx, okidx;
			kidx = okidx = Keytable[header.m_buildVersion & 511];
			for (uint i = 0; i != length; ++i)
			{
				buffer[i] = Keytable[SignedMod(kidx, 512)];
				kidx -= header.m_buildVersion & 511;
			}
			return buffer;
		}

		public byte[] IV(CMFHeader header, byte[] digest, int length)
		{
			byte[] buffer = new byte[length];
			uint kidx, okidx;
			kidx = okidx = Keytable[header.m_dataCount & 511];
			for (int i = 0; i != length; ++i)
			{
				buffer[i] = Keytable[SignedMod(kidx, 512)];
				kidx += 3;
				buffer[i] ^= digest[SignedMod(kidx - i, SHA1_DIGESTSIZE)];
			}
			return buffer;
		}

		private static readonly byte[] Keytable =
		{
			0x1B, 0xEE, 0x16, 0xCA, 0x60, 0x25, 0x0C, 0xC0, 0x86, 0xC8, 0x12, 0x48, 0xBD, 0x16, 0x57, 0x1D, 
			0x21, 0xA4, 0x9B, 0xDC, 0x16, 0xDF, 0x81, 0xC8, 0x8F, 0x02, 0xAF, 0x36, 0x37, 0xE1, 0xD8, 0x7A, 
			0x38, 0x83, 0x7E, 0x59, 0xA4, 0xFA, 0x65, 0x5B, 0x31, 0x9D, 0x08, 0x1A, 0x96, 0xEC, 0x93, 0x3B, 
			0x55, 0xAD, 0xE5, 0x85, 0x16, 0xD8, 0x5E, 0x7D, 0x22, 0x7D, 0x7E, 0x46, 0x8C, 0xBD, 0x55, 0xDE, 
			0x34, 0x5D, 0x6E, 0x92, 0x4B, 0xE3, 0xA7, 0xD2, 0xA8, 0x43, 0x7A, 0x1E, 0xDD, 0x6B, 0x71, 0x97, 
			0x0F, 0x50, 0x01, 0xF2, 0x70, 0xF7, 0xF9, 0xA3, 0x8D, 0x1C, 0x0E, 0x94, 0xEE, 0xFF, 0x75, 0x06, 
			0x39, 0xC0, 0x23, 0x01, 0x6E, 0x38, 0xB8, 0x07, 0x8F, 0xC6, 0x53, 0x12, 0xD5, 0x6E, 0x0B, 0xC6, 
			0xC3, 0x59, 0xBD, 0x2A, 0x79, 0x1F, 0xE7, 0xD3, 0x0E, 0x01, 0xB8, 0x37, 0xCF, 0x96, 0x84, 0xAB, 
			0xA9, 0xFE, 0x80, 0xB4, 0x57, 0x80, 0x22, 0x5B, 0xAA, 0x55, 0x77, 0xF7, 0x75, 0x24, 0xF1, 0x6C, 
			0x0B, 0xBF, 0x2E, 0x54, 0x50, 0xDA, 0x8E, 0x3B, 0x5E, 0x50, 0x63, 0xD3, 0x67, 0xF0, 0x5A, 0x89, 
			0x2E, 0x84, 0x7A, 0xE7, 0x1A, 0xC8, 0xF3, 0x64, 0xEF, 0x76, 0x52, 0x14, 0xC3, 0x0C, 0xB6, 0x35, 
			0x6A, 0xF6, 0xB8, 0x71, 0x9B, 0xD6, 0x27, 0x4D, 0x38, 0x3B, 0xCC, 0xDC, 0x16, 0xE2, 0x39, 0x4D, 
			0x0F, 0x72, 0xA8, 0xF0, 0xE2, 0x4F, 0x40, 0xB6, 0x3A, 0x55, 0x8A, 0x3F, 0x84, 0xE7, 0x7C, 0x50, 
			0xF5, 0x4D, 0x04, 0x0C, 0xB2, 0xFC, 0x9B, 0x03, 0x63, 0xF1, 0x92, 0x20, 0xE0, 0xEE, 0x68, 0xE8, 
			0x2A, 0x06, 0x82, 0x39, 0x10, 0x6D, 0xE0, 0xE7, 0x52, 0xC6, 0x76, 0xEC, 0x6B, 0x9A, 0x6B, 0x7E, 
			0xF5, 0x0D, 0xC1, 0x52, 0x9A, 0xE4, 0x5C, 0xD8, 0x1A, 0xA6, 0x5E, 0xD8, 0xB4, 0x1F, 0xB4, 0x58, 
			0x60, 0x98, 0xC7, 0x04, 0x8F, 0x67, 0x71, 0xED, 0x81, 0x62, 0x51, 0xAE, 0x90, 0x59, 0x0E, 0x1F, 
			0xC3, 0xE9, 0x5A, 0xBA, 0x5C, 0x82, 0x8F, 0xD4, 0xBC, 0x0D, 0x09, 0xEB, 0xDD, 0x92, 0xD2, 0xAB, 
			0xED, 0x23, 0xE8, 0x32, 0x6F, 0x56, 0xA8, 0x1B, 0x14, 0x67, 0x0D, 0x03, 0xD2, 0xFE, 0x71, 0x01, 
			0x59, 0xE5, 0x0D, 0x9B, 0x7E, 0x05, 0x6D, 0x2C, 0x7E, 0x10, 0x18, 0xCA, 0x9F, 0xB0, 0x6A, 0xC0, 
			0xB1, 0xC4, 0x6F, 0x37, 0xCC, 0x48, 0x74, 0x0D, 0xE0, 0xC7, 0xC8, 0xDF, 0xFD, 0xBA, 0xB8, 0x59, 
			0x51, 0x3B, 0x52, 0xEC, 0x42, 0x88, 0xCC, 0x2C, 0x31, 0x2D, 0x9B, 0x6C, 0xE3, 0xD0, 0x98, 0x0C, 
			0x17, 0x01, 0x27, 0x9A, 0x2B, 0xE4, 0x6B, 0x0B, 0x9D, 0xD3, 0x16, 0xE6, 0x4E, 0x2A, 0x81, 0x01, 
			0x64, 0xD7, 0xE4, 0xB4, 0x97, 0x75, 0xC8, 0x0F, 0x13, 0x5F, 0x95, 0x0B, 0xEC, 0xA6, 0x23, 0x45, 
			0xFA, 0xB5, 0xDB, 0x83, 0x0B, 0x7C, 0x91, 0xEF, 0xB8, 0x6E, 0xA1, 0x34, 0x62, 0x6A, 0x50, 0x6C, 
			0x6F, 0x8F, 0x12, 0x20, 0x48, 0x91, 0xF0, 0xA7, 0xE0, 0x09, 0xF9, 0x26, 0x70, 0x8D, 0xF8, 0x4A, 
			0xD4, 0xA8, 0xF7, 0x36, 0xE2, 0xF5, 0xCE, 0xA6, 0x4D, 0x3D, 0xDE, 0xEE, 0xF5, 0x7A, 0xEF, 0xF9, 
			0x83, 0xAC, 0xB1, 0xD3, 0x5D, 0x38, 0x71, 0x93, 0x45, 0x65, 0xC3, 0x92, 0x71, 0x1A, 0x3B, 0xF2, 
			0x7C, 0xD0, 0x02, 0xE6, 0x20, 0x20, 0x34, 0xA1, 0x95, 0x18, 0x1F, 0x0F, 0xB7, 0xE4, 0x64, 0xBC, 
			0x63, 0x54, 0x6F, 0xBD, 0xB2, 0xC7, 0x95, 0x4C, 0x61, 0xE3, 0x0D, 0x3F, 0xA7, 0xD1, 0x29, 0xC0, 
			0xEC, 0x60, 0x8D, 0x54, 0x7A, 0x1D, 0x43, 0x2E, 0xC7, 0x0C, 0xDB, 0x4B, 0xEB, 0xDC, 0xD0, 0x09, 
			0x4A, 0x30, 0x63, 0x02, 0x32, 0x99, 0x46, 0x13, 0xD9, 0x4C, 0x56, 0xE7, 0x14, 0xE7, 0xD3, 0x04
		};
	}
}
