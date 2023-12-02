using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ResourceGraph;

namespace TACTLib.Core.Product.Tank.TRG
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProTRG_71620 : ITRGEncryptionProc
    {
        public byte[] Key(TRGHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[header.m_buildVersion & 511];
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
                kidx -= 43;
                buffer[i] ^= digest[SignedMod(kidx + header.m_skinCount, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0xDD, 0x5E, 0xA7, 0x40, 0x5F, 0x7F, 0x6E, 0x78, 0xAF, 0x1E, 0x39, 0x74, 0xF9, 0x09, 0x18, 0xDB, 
            0x62, 0x20, 0x8F, 0xCE, 0x55, 0x58, 0xEC, 0x41, 0x8B, 0x09, 0x4A, 0xEE, 0x6A, 0xEA, 0x94, 0xFF, 
            0xAA, 0x72, 0x9F, 0x94, 0x6F, 0x7B, 0x01, 0xC7, 0x8B, 0x9E, 0xC8, 0x53, 0xDA, 0xB4, 0x06, 0xAF, 
            0x69, 0x9C, 0x69, 0xB4, 0xBF, 0x0E, 0x29, 0x52, 0x70, 0x4D, 0xCE, 0x3B, 0xCD, 0xC9, 0x8C, 0x6C, 
            0xEC, 0x07, 0x65, 0xE0, 0xD6, 0xE3, 0xD7, 0x36, 0xD4, 0x6E, 0x8E, 0xFB, 0x4F, 0x1B, 0xDF, 0xF1, 
            0x08, 0xA8, 0x52, 0x09, 0x5C, 0x46, 0xCE, 0x87, 0x52, 0xC9, 0x59, 0x12, 0xF3, 0x2C, 0x3B, 0x6D, 
            0x8C, 0xEB, 0x2A, 0x3B, 0x91, 0xA1, 0x64, 0x49, 0x70, 0x85, 0x59, 0x9A, 0x0D, 0xE6, 0x51, 0xEC, 
            0x84, 0x09, 0xFA, 0x79, 0x6B, 0xB1, 0x76, 0x29, 0xCC, 0xFB, 0x78, 0x00, 0xAA, 0xD1, 0xF0, 0x7E, 
            0x9E, 0x65, 0x34, 0xBA, 0x50, 0x5D, 0xA1, 0xB3, 0x3F, 0x2A, 0xC6, 0xAF, 0x4C, 0x4F, 0x2D, 0xD6, 
            0xAB, 0xBE, 0xEF, 0xFF, 0x8B, 0x8B, 0xAB, 0xE6, 0xE0, 0xAA, 0xBE, 0x83, 0xB7, 0x12, 0x88, 0x65, 
            0xFA, 0x2E, 0xF2, 0x73, 0x0E, 0x9E, 0x16, 0x6E, 0x5F, 0x71, 0x2F, 0x02, 0xDA, 0x0D, 0xC8, 0x59, 
            0x34, 0x9B, 0x3F, 0x2A, 0x55, 0x14, 0xC9, 0x73, 0x60, 0x24, 0xD1, 0x52, 0xBD, 0xCC, 0x54, 0x1B, 
            0xFF, 0xA4, 0x4D, 0x60, 0x70, 0x5F, 0xD9, 0xBF, 0x7C, 0x52, 0xBA, 0x00, 0xA9, 0x1C, 0xEB, 0xCD, 
            0x11, 0x48, 0xEA, 0x30, 0x58, 0x01, 0xFB, 0x60, 0xD7, 0x32, 0x7D, 0x95, 0xAD, 0x54, 0xD6, 0xCD, 
            0xF6, 0x3A, 0x13, 0x85, 0x2C, 0x51, 0x8D, 0x7A, 0x92, 0x4B, 0x8E, 0xE8, 0x03, 0x4D, 0xE9, 0x54, 
            0xC8, 0x41, 0x94, 0xB8, 0x15, 0x0D, 0xD5, 0x84, 0xAA, 0x9A, 0x24, 0x7C, 0xBF, 0xC1, 0x19, 0x49, 
            0x80, 0x96, 0x26, 0xD2, 0xF8, 0x04, 0x55, 0x76, 0x06, 0x90, 0xE4, 0x3C, 0x5F, 0x7B, 0x65, 0x4E, 
            0x61, 0xED, 0x28, 0x7A, 0xC8, 0xF8, 0xFC, 0x0D, 0xAD, 0x91, 0x1E, 0x3A, 0xF2, 0xBB, 0x1B, 0xFC, 
            0xB3, 0xDF, 0xA1, 0x23, 0x54, 0xA6, 0xF4, 0x44, 0x7C, 0xF0, 0xE1, 0xC1, 0xBD, 0xB8, 0x13, 0x3F, 
            0x21, 0x8B, 0x3B, 0xBE, 0xA2, 0x5E, 0xE7, 0x3D, 0xF9, 0x32, 0x2E, 0x4D, 0x33, 0x60, 0xA6, 0x30, 
            0xAA, 0x27, 0x8D, 0x22, 0x9A, 0xCF, 0xEA, 0x11, 0x06, 0x07, 0xC7, 0x09, 0x3E, 0x8F, 0x1C, 0x33, 
            0x19, 0xBF, 0xB1, 0xE0, 0x60, 0x69, 0x74, 0x3E, 0x69, 0xE7, 0x2C, 0x16, 0x0D, 0xDC, 0xE7, 0xA7, 
            0x58, 0x18, 0x72, 0x87, 0x29, 0x3A, 0x1D, 0x10, 0x1D, 0xF0, 0x34, 0x4B, 0x19, 0x79, 0xDA, 0xFD, 
            0x21, 0x16, 0x95, 0x8A, 0x0C, 0x78, 0xA3, 0xAC, 0xE8, 0x19, 0xEA, 0x39, 0xFC, 0xB3, 0x2F, 0xAA, 
            0xC7, 0x5D, 0x79, 0x94, 0xBA, 0x8A, 0xCC, 0xA1, 0x7F, 0xA1, 0x1A, 0xBB, 0x81, 0xAA, 0xF0, 0x64, 
            0x27, 0xA0, 0x95, 0x77, 0xE4, 0x03, 0x02, 0x4E, 0xB1, 0x5C, 0x2E, 0xFE, 0x07, 0x94, 0xC4, 0x73, 
            0x8C, 0xA2, 0x60, 0xB5, 0x93, 0x70, 0xE3, 0xA8, 0x4B, 0xAC, 0xA5, 0x16, 0xB5, 0x62, 0xD4, 0x33, 
            0xA1, 0x8D, 0xE3, 0xE6, 0x95, 0x07, 0x33, 0x4D, 0x8C, 0x9D, 0x14, 0x50, 0x80, 0x9A, 0xFD, 0xBE, 
            0xCC, 0x6C, 0x33, 0x7E, 0xAB, 0xA5, 0x80, 0x73, 0x1D, 0xE4, 0xF5, 0x1A, 0x62, 0x8B, 0xE2, 0x1C, 
            0x63, 0xD5, 0x32, 0x36, 0x65, 0x3A, 0xC9, 0x54, 0xBE, 0x4F, 0x4B, 0xEA, 0xEF, 0x46, 0xEA, 0xD0, 
            0x08, 0x41, 0x28, 0x9C, 0x0E, 0xEC, 0x7A, 0x3A, 0x00, 0x18, 0x8A, 0x69, 0xDC, 0x04, 0x28, 0xCF, 
            0x9D, 0x9E, 0x31, 0x0B, 0x62, 0x2D, 0x5D, 0xEE, 0x1A, 0x33, 0x24, 0xD5, 0xB6, 0x01, 0xA3, 0xC7
        };
    }
}