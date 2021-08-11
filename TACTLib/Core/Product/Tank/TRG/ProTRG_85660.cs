using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ResourceGraph;

namespace TACTLib.Core.Product.Tank.TRG
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProTRG_85660 : ITRGEncryptionProc
    {
        public byte[] Key(TRGHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[header.m_buildVersion & 511];
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += (uint)header.m_packageCount;
            }
            return buffer;
        }

        public byte[] IV(TRGHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[((3 * digest[11]) - length) & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.m_buildVersion - kidx;
                buffer[i] ^= digest[SignedMod(kidx + i, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x1D, 0xF3, 0xF6, 0x8A, 0x83, 0x2C, 0x56, 0x74, 0x7A, 0x88, 0xAE, 0x75, 0x1B, 0x86, 0x2E, 0x4C, 
            0x74, 0x17, 0x3E, 0xC4, 0x58, 0x4E, 0x59, 0xAC, 0xA9, 0xAD, 0xA0, 0x11, 0x14, 0x2D, 0x05, 0x76, 
            0x45, 0x8C, 0x5C, 0x6C, 0x4A, 0xCC, 0x5B, 0xBD, 0x84, 0x17, 0xF5, 0xFE, 0xC1, 0xC4, 0xEA, 0x46, 
            0x5F, 0x95, 0xF5, 0xF7, 0xDE, 0x8D, 0xD8, 0xC7, 0xE1, 0x17, 0x5C, 0x54, 0xB6, 0xA3, 0xF7, 0xED, 
            0x12, 0x71, 0x0A, 0xB1, 0xEE, 0xF4, 0xBF, 0x96, 0xF6, 0xD0, 0xB7, 0x21, 0x10, 0x75, 0x09, 0x3E, 
            0xE2, 0x3F, 0x47, 0x3D, 0x8B, 0x13, 0xB2, 0xC2, 0x6C, 0x4E, 0x76, 0xCA, 0x04, 0xB8, 0xFE, 0x9D, 
            0x07, 0x11, 0x13, 0x90, 0xCE, 0x51, 0x82, 0x39, 0xEA, 0x50, 0x1E, 0xE5, 0x0A, 0x34, 0x3D, 0x69, 
            0x35, 0xC9, 0x9E, 0x1D, 0xEE, 0x28, 0x62, 0xC2, 0xCC, 0xCF, 0xD9, 0x09, 0x84, 0x0F, 0x91, 0x7C, 
            0x38, 0x8A, 0x52, 0x8E, 0x01, 0x3B, 0x58, 0x01, 0x80, 0x7C, 0xFD, 0xE4, 0x30, 0xF3, 0xC8, 0xA4, 
            0x99, 0xCE, 0x31, 0x59, 0x24, 0x7A, 0x42, 0xFF, 0x01, 0xB5, 0x0C, 0x55, 0x64, 0x1A, 0x29, 0x38, 
            0x87, 0xAD, 0xB6, 0xD2, 0xA1, 0xFB, 0x54, 0x3C, 0xCB, 0x88, 0xE7, 0x42, 0x79, 0x8B, 0x3F, 0x4A, 
            0x4A, 0xFC, 0xAD, 0x60, 0x04, 0x24, 0xA9, 0x96, 0xAA, 0xB7, 0x90, 0x08, 0x53, 0x6A, 0x5B, 0x83, 
            0x0B, 0xF8, 0xC1, 0xA0, 0xA2, 0xA5, 0xCA, 0x97, 0x32, 0x5E, 0x18, 0x05, 0x20, 0xE8, 0xAF, 0xE1, 
            0x1C, 0xB0, 0xFF, 0xC4, 0x77, 0xDE, 0xF2, 0x67, 0xF8, 0x07, 0x7F, 0x93, 0xC6, 0xC5, 0xFA, 0x47, 
            0x32, 0x32, 0x09, 0xC6, 0xCB, 0x05, 0x22, 0x97, 0x7D, 0xA6, 0x73, 0x3F, 0x24, 0xD0, 0x12, 0x10, 
            0xCE, 0xB5, 0x07, 0xB1, 0xF4, 0x72, 0x8E, 0x18, 0xA2, 0x87, 0xD8, 0x13, 0x14, 0x12, 0x26, 0xD9, 
            0xA0, 0x69, 0x65, 0x57, 0xAB, 0x1B, 0x8E, 0xC9, 0xBF, 0x3E, 0x97, 0x71, 0x23, 0x17, 0x3E, 0x45, 
            0x9B, 0xF9, 0xCA, 0x1D, 0x93, 0x9F, 0x4B, 0x49, 0x62, 0x32, 0x7A, 0x1F, 0xAB, 0xF1, 0x95, 0x93, 
            0xFB, 0xFD, 0x95, 0x59, 0xFD, 0xF9, 0xF1, 0x6F, 0x0B, 0xFA, 0xC9, 0x0E, 0xFA, 0x6C, 0x5A, 0x6E, 
            0xC2, 0x9D, 0x4F, 0xC1, 0x50, 0xD2, 0x87, 0xA4, 0x4D, 0x08, 0x33, 0x44, 0x06, 0x19, 0x09, 0x18, 
            0x7C, 0x8A, 0x14, 0x14, 0x2C, 0x03, 0x5B, 0x43, 0x10, 0x8F, 0x40, 0x9F, 0x30, 0xCF, 0x92, 0x08, 
            0xA4, 0xAA, 0x88, 0x3A, 0x8D, 0xF8, 0xC7, 0x69, 0xDB, 0x31, 0xA6, 0x88, 0x92, 0x25, 0x4E, 0xF8, 
            0x97, 0x52, 0x05, 0x5C, 0xBB, 0xDD, 0x21, 0x57, 0x3C, 0x55, 0x6C, 0xD4, 0x91, 0xA3, 0xCD, 0xD5, 
            0xD4, 0xF9, 0xC6, 0x50, 0xF3, 0x34, 0x55, 0xFA, 0xCE, 0x80, 0x84, 0x7D, 0x44, 0xBB, 0xA9, 0x58, 
            0x08, 0x60, 0x1D, 0x3F, 0x4E, 0xDC, 0xAA, 0x62, 0x70, 0x70, 0x16, 0xA4, 0x26, 0x16, 0x90, 0xB1, 
            0x3D, 0x0D, 0x34, 0x27, 0x49, 0x18, 0x09, 0xB0, 0xA4, 0x9C, 0x40, 0x22, 0x3A, 0xB4, 0x78, 0x86, 
            0x39, 0x26, 0xDF, 0x86, 0x07, 0x61, 0xA7, 0x95, 0x97, 0x2E, 0xA8, 0x0A, 0xB5, 0x2E, 0x6B, 0xF2, 
            0xA3, 0x82, 0x9D, 0x5C, 0xCE, 0xD1, 0x65, 0x27, 0x64, 0x24, 0x9D, 0x00, 0xCC, 0xE9, 0x6D, 0xA6, 
            0x55, 0xBD, 0x82, 0x37, 0x67, 0xA2, 0x03, 0x9F, 0x4B, 0xEC, 0x4C, 0x03, 0xF7, 0xD3, 0x47, 0x20, 
            0x42, 0x79, 0x9D, 0x3C, 0x0E, 0x07, 0x05, 0x1C, 0x7F, 0xFF, 0xF4, 0x8B, 0x39, 0x9E, 0x8D, 0xA1, 
            0xD8, 0xF6, 0x52, 0x5F, 0xE2, 0xDE, 0xE9, 0x3A, 0xD5, 0xDA, 0xD2, 0x11, 0x15, 0xA9, 0x5F, 0x3B, 
            0xB7, 0xB3, 0x26, 0xDA, 0x91, 0xAF, 0x8D, 0xDF, 0x01, 0x22, 0x60, 0xCB, 0x3B, 0x81, 0x6D, 0x86
        };
    }
}
