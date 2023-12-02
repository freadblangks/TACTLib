using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_105760 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[length + 256];
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += (header.m_buildVersion * (uint)header.m_dataCount) % 7;
            }
            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[(digest[7] * Keytable[0]) & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += ((digest[6] & 1) != 0) ? 37 : (okidx % 61);
                buffer[i] ^= digest[SignedMod(kidx - i, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x16, 0xF4, 0xB2, 0x13, 0x78, 0x2F, 0x4D, 0xA1, 0x99, 0x34, 0x5E, 0xEB, 0xF7, 0x73, 0x66, 0x7D,
            0x42, 0x71, 0xDE, 0xB5, 0xCB, 0x67, 0xF9, 0xC5, 0xF7, 0x6E, 0xC5, 0x7E, 0xC7, 0x5A, 0x76, 0xD6,
            0xAA, 0x26, 0x64, 0x68, 0xCC, 0x8A, 0x50, 0x4F, 0xB1, 0x7E, 0x45, 0x84, 0x5A, 0x19, 0xC1, 0x17,
            0x0C, 0x79, 0xC1, 0xD3, 0xEE, 0xE3, 0xC4, 0x81, 0xFB, 0x62, 0xD1, 0xB1, 0x48, 0x19, 0x94, 0x3C,
            0xAA, 0x3C, 0xEC, 0xDF, 0x0C, 0x70, 0x13, 0xA1, 0x61, 0x7A, 0xE0, 0x7F, 0x4E, 0xA4, 0xF6, 0x9A,
            0x22, 0x54, 0xA3, 0xE7, 0x69, 0x82, 0x7B, 0x58, 0xF0, 0x69, 0xE1, 0x8D, 0x5C, 0x7A, 0xCB, 0x42,
            0x55, 0x4B, 0x90, 0x73, 0x07, 0x27, 0xB5, 0x29, 0x7E, 0xEF, 0xB0, 0xFD, 0x02, 0x65, 0x5E, 0x62,
            0x4C, 0x37, 0xE2, 0x9A, 0xE5, 0x46, 0x48, 0x43, 0x02, 0xE4, 0x88, 0xE7, 0xBD, 0x79, 0x16, 0x47,
            0x12, 0x73, 0xCD, 0x91, 0x6A, 0xED, 0xA2, 0x83, 0xD1, 0x4B, 0x93, 0xBF, 0x4E, 0x12, 0x03, 0x38,
            0x39, 0xA2, 0x23, 0xC1, 0x33, 0x46, 0xFB, 0x5C, 0xB1, 0x43, 0xF8, 0x86, 0x7F, 0x1D, 0x6E, 0x45,
            0xBF, 0xE0, 0x33, 0x1F, 0x8A, 0xDE, 0x4D, 0xDC, 0x76, 0x74, 0xAB, 0x26, 0xBE, 0x81, 0xBC, 0x22,
            0xAA, 0x38, 0xD7, 0x3C, 0x50, 0x6A, 0xF1, 0x65, 0xB3, 0x79, 0x46, 0x7F, 0xE6, 0x8A, 0x0B, 0x15,
            0x94, 0xF6, 0x42, 0x75, 0xF3, 0x8D, 0xD4, 0x4B, 0xFF, 0x15, 0x2A, 0xD7, 0xC5, 0x45, 0x63, 0x5F,
            0x62, 0xAE, 0x2E, 0x07, 0xB2, 0x92, 0x4B, 0x10, 0x6A, 0xFD, 0x67, 0x27, 0x76, 0xB7, 0x66, 0xF4,
            0x53, 0x85, 0x1F, 0xB5, 0xC5, 0xE0, 0x26, 0xF8, 0xE4, 0xA0, 0x15, 0x27, 0xEE, 0xD4, 0x10, 0x2E,
            0x19, 0x26, 0x4D, 0xC9, 0x84, 0x76, 0x1F, 0xBE, 0x8F, 0x39, 0xE0, 0x51, 0x0F, 0x0D, 0x36, 0x45,
            0xF1, 0xAB, 0xC1, 0x2A, 0x73, 0xFF, 0x09, 0x81, 0xAA, 0x27, 0x0F, 0x3F, 0xC5, 0x86, 0x36, 0x5D,
            0x6E, 0x7C, 0x05, 0x7C, 0x8C, 0x52, 0x6F, 0x3D, 0xE0, 0x02, 0x80, 0x8A, 0xC9, 0x41, 0x92, 0xE9,
            0xE8, 0xAD, 0xD8, 0x4D, 0x03, 0xE6, 0x43, 0xFD, 0x80, 0xF0, 0xC5, 0x23, 0xFD, 0xAA, 0xA4, 0x26,
            0xEF, 0x24, 0xAA, 0x8E, 0xCD, 0xFF, 0xC3, 0x0B, 0xC3, 0xDE, 0x82, 0x92, 0x16, 0xD9, 0x5F, 0xF6,
            0xF3, 0x96, 0x8C, 0x9A, 0x57, 0x7C, 0x01, 0xC8, 0x6C, 0x0E, 0x3F, 0xD8, 0xF4, 0xA5, 0xC4, 0x2E,
            0xE6, 0x14, 0x99, 0xCC, 0xF2, 0x7C, 0x98, 0x04, 0xB0, 0x48, 0x1B, 0xC5, 0x87, 0xA9, 0x91, 0xD3,
            0x05, 0x54, 0xFB, 0xE7, 0xAB, 0xDE, 0xDA, 0x2A, 0xE1, 0xD7, 0xD2, 0xD2, 0xCD, 0xC3, 0x11, 0xA1,
            0x22, 0xE2, 0xFD, 0x70, 0xA1, 0xAE, 0x04, 0x93, 0x79, 0x2A, 0x23, 0x31, 0x8A, 0x6C, 0xE8, 0x34,
            0xC6, 0x04, 0xBE, 0x63, 0x2E, 0x49, 0xE4, 0x40, 0x49, 0xDF, 0x0E, 0xA2, 0x83, 0x3B, 0xD3, 0xB5,
            0x15, 0x32, 0x54, 0x5C, 0xAD, 0x74, 0x4E, 0x82, 0x5B, 0x9B, 0x4C, 0x55, 0x61, 0x67, 0xD8, 0xD8,
            0xE4, 0x24, 0x9D, 0xB8, 0x8C, 0x8C, 0x3D, 0x0C, 0xC3, 0x90, 0x12, 0xF7, 0x76, 0xC1, 0x89, 0xAC,
            0x4C, 0x0B, 0x9A, 0xC2, 0x3C, 0x68, 0xFF, 0xFF, 0x35, 0xC5, 0x60, 0x63, 0x76, 0xC6, 0x45, 0xF6,
            0x1B, 0x9C, 0x73, 0x65, 0x8A, 0x50, 0x8E, 0x55, 0x95, 0x7A, 0xCA, 0x24, 0xA6, 0x05, 0x9D, 0x9A,
            0x42, 0x82, 0xFF, 0x60, 0xAF, 0x04, 0x6A, 0xE2, 0x4A, 0xB2, 0x1E, 0xF8, 0x9E, 0xCE, 0x4A, 0x22,
            0xE7, 0x39, 0x74, 0xE1, 0x8D, 0x47, 0x02, 0x4F, 0xB3, 0x61, 0x8F, 0x92, 0x1F, 0x76, 0x08, 0x71,
            0xE2, 0x6E, 0xFA, 0x5D, 0xF9, 0xE3, 0x19, 0x16, 0x71, 0xEA, 0xF3, 0x95, 0x52, 0x1B, 0x66, 0xC1
        };
    }
}