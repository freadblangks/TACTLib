using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ResourceGraph;

namespace TACTLib.Core.Product.Tank.TRG
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProTRG_73258 : ITRGEncryptionProc
    {
        public byte[] Key(TRGHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)length * header.m_buildVersion;
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.m_buildVersion - kidx;
            }
            return buffer;
        }

        public byte[] IV(TRGHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[header.m_buildVersion & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += (uint)header.m_packageCount + digest[SignedMod(header.m_packageCount, SHA1_DIGESTSIZE)];
                buffer[i] ^= digest[SignedMod(header.m_buildVersion + i, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x29, 0xA8, 0x91, 0xA0, 0xDF, 0x85, 0x03, 0x56, 0x21, 0xDC, 0x3F, 0x78, 0x7C, 0xB3, 0xF0, 0x1B, 
            0x50, 0x47, 0x18, 0x52, 0xE4, 0x7D, 0xB8, 0x1C, 0xAF, 0x74, 0x17, 0x6D, 0x5F, 0xD0, 0xB5, 0x0E, 
            0xDC, 0x55, 0x4A, 0x10, 0x5E, 0xCB, 0x93, 0x04, 0xB0, 0x68, 0x9F, 0x33, 0x5C, 0x1D, 0x27, 0xED, 
            0xE7, 0x67, 0x03, 0x6D, 0xE6, 0xCE, 0xE8, 0xE1, 0x8D, 0xAC, 0x07, 0xA7, 0x89, 0xBF, 0xD7, 0x44, 
            0x0F, 0x51, 0xFE, 0x56, 0x7E, 0x2A, 0x7A, 0x6C, 0xB9, 0x75, 0xFF, 0xDC, 0xC3, 0x82, 0xFF, 0x68, 
            0x51, 0x20, 0x65, 0x64, 0x4B, 0xA4, 0x7E, 0xA5, 0xE6, 0x2A, 0x29, 0xC1, 0xBE, 0xD8, 0xB5, 0x0C, 
            0x5C, 0x14, 0x7C, 0xD0, 0xA0, 0xC2, 0xBB, 0x6F, 0x54, 0xF2, 0x2B, 0x35, 0x7E, 0xCE, 0xBE, 0x45, 
            0x74, 0x77, 0x61, 0x3F, 0x12, 0x70, 0xEE, 0xAB, 0x2C, 0x44, 0x8D, 0x46, 0x42, 0xF5, 0x47, 0x56, 
            0xF3, 0x8E, 0x38, 0x30, 0x12, 0xB6, 0x69, 0xC7, 0x2E, 0x7A, 0x84, 0xF3, 0x8F, 0x71, 0x5B, 0x4F, 
            0x45, 0x7C, 0x99, 0x6D, 0x22, 0x8B, 0x5B, 0xEE, 0xF6, 0xB5, 0x12, 0x89, 0x25, 0xA7, 0x3C, 0x7D, 
            0x0E, 0xC0, 0xC1, 0x70, 0xD3, 0x92, 0xE3, 0x6F, 0x34, 0x04, 0xEC, 0x1F, 0x04, 0x04, 0xBB, 0xE6, 
            0xDC, 0x5C, 0xF1, 0xE1, 0x9E, 0xD7, 0x1A, 0x8D, 0x54, 0xD6, 0x43, 0x5C, 0x7D, 0xDE, 0xF5, 0xD1, 
            0xD7, 0x26, 0x7E, 0x72, 0x56, 0xBD, 0x38, 0x28, 0xDC, 0xD8, 0x0F, 0x31, 0x57, 0x1E, 0xA4, 0x26, 
            0xB0, 0xA8, 0xB4, 0x42, 0x17, 0x60, 0x2C, 0xD5, 0x3B, 0x59, 0x02, 0x0F, 0x5F, 0x2B, 0xC9, 0xE7, 
            0x2F, 0x3C, 0x39, 0x6E, 0x19, 0xE3, 0x5E, 0x09, 0x30, 0xEB, 0x47, 0x5B, 0xFD, 0x24, 0xCA, 0x14, 
            0x1C, 0x2A, 0x6F, 0xA1, 0xFC, 0x06, 0x24, 0x14, 0xC2, 0x70, 0xA4, 0x86, 0xDE, 0x46, 0xA8, 0x4A, 
            0x1E, 0x34, 0xB5, 0x40, 0x50, 0x70, 0xE2, 0xCA, 0x87, 0x0F, 0xDB, 0x23, 0xF7, 0x4A, 0x0E, 0x67, 
            0xC3, 0xB9, 0xE4, 0xA6, 0x08, 0x6B, 0x14, 0x6B, 0xB3, 0x94, 0x60, 0x31, 0xFC, 0x00, 0x3F, 0x37, 
            0x91, 0x69, 0x61, 0x49, 0xBE, 0x31, 0xF9, 0x8C, 0xDD, 0xF4, 0xF9, 0x86, 0x52, 0xAA, 0x1E, 0x56, 
            0x25, 0x0B, 0x50, 0x3B, 0xD8, 0x63, 0x4C, 0x66, 0xBE, 0x42, 0xFC, 0x23, 0x24, 0x26, 0xCE, 0xF7, 
            0x57, 0x70, 0x7D, 0x03, 0x10, 0x48, 0x1B, 0x73, 0x40, 0xD5, 0xE3, 0x58, 0x18, 0x45, 0x19, 0x61, 
            0x7E, 0x7F, 0x59, 0xF3, 0x0E, 0xF6, 0xA0, 0x94, 0x35, 0xF0, 0x7C, 0x31, 0x0B, 0x42, 0x39, 0xC8, 
            0x2E, 0xD0, 0x88, 0x2C, 0xB1, 0x01, 0x47, 0xC6, 0x8B, 0xC6, 0x72, 0x25, 0xC7, 0x37, 0x3B, 0x2A, 
            0x3C, 0xF2, 0x1D, 0xD5, 0x14, 0x7E, 0xC6, 0xE8, 0xC5, 0x56, 0x84, 0xD6, 0x18, 0x9D, 0xBF, 0xC4, 
            0x21, 0xDF, 0xB6, 0xE5, 0x1B, 0x1D, 0x05, 0xAF, 0xA9, 0xD3, 0x09, 0xD3, 0xE0, 0x7B, 0x5D, 0xFE, 
            0x25, 0xB9, 0x3F, 0x28, 0x3A, 0x03, 0x9B, 0x3A, 0x3A, 0x04, 0x1C, 0xE8, 0xBC, 0x64, 0x53, 0x79, 
            0xA2, 0x94, 0x4C, 0x79, 0x77, 0x02, 0x86, 0x43, 0x63, 0x3A, 0x44, 0xFB, 0x2E, 0x0F, 0x35, 0x4A, 
            0x06, 0xA6, 0x1D, 0x5A, 0x47, 0xB5, 0x55, 0x14, 0x7D, 0x34, 0x28, 0x2B, 0x1E, 0x6A, 0x31, 0x19, 
            0x6D, 0x4D, 0x71, 0xC8, 0xFC, 0x57, 0x59, 0xA8, 0x37, 0xC3, 0x52, 0x91, 0x83, 0x6A, 0x4B, 0x09, 
            0x10, 0xA6, 0x49, 0xF4, 0xA3, 0x57, 0x5E, 0x20, 0x9D, 0x92, 0xBA, 0x09, 0xF9, 0x4D, 0x83, 0xDB, 
            0x49, 0x52, 0xD0, 0xAF, 0x76, 0xB3, 0x7D, 0xAD, 0xF8, 0xCF, 0x0A, 0x6D, 0x91, 0x7A, 0x74, 0x47, 
            0xC5, 0x44, 0xA8, 0xFF, 0xE3, 0xAB, 0x2E, 0xCC, 0x1D, 0xF2, 0x22, 0x4C, 0xCB, 0x5E, 0xEC, 0x42
        };
    }
}