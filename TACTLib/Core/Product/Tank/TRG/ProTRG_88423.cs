using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ResourceGraph;

namespace TACTLib.Core.Product.Tank.TRG
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProTRG_88423 : ITRGEncryptionProc
    {
        public byte[] Key(TRGHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)(length * header.m_buildVersion);
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += okidx % 61;
            }
            return buffer;
        }

        public byte[] IV(TRGHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)(digest[5] + header.m_skinCount) & 511;
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                switch (SignedMod(kidx, 3))
                {
                case 0:
                    kidx += 341;
                    break;
                case 1:
                    kidx = (uint)SignedMod(kidx * 7, header.m_buildVersion);
                    break;
                case 2:
                    kidx -= 13;
                    break;
                }
                buffer[i] ^= digest[SignedMod(kidx + header.m_buildVersion, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x6F, 0x80, 0x0F, 0x44, 0xA7, 0xC7, 0xD8, 0x75, 0xC2, 0xEF, 0x9C, 0x88, 0xCD, 0xA6, 0x13, 0x0B,
            0x5D, 0x8A, 0x84, 0x68, 0xFA, 0x7C, 0x1E, 0x9A, 0x48, 0xEE, 0x81, 0xAA, 0x3A, 0x1B, 0xBA, 0x2B,
            0xDB, 0x40, 0x01, 0x8B, 0x2B, 0x00, 0x16, 0x46, 0x48, 0x7C, 0xF7, 0xE3, 0xC4, 0xCE, 0x49, 0x6E,
            0x74, 0x75, 0xA1, 0xB2, 0x8D, 0x89, 0x1B, 0x46, 0x81, 0xD8, 0x1F, 0x6B, 0xAF, 0x16, 0x85, 0x4A,
            0x59, 0x4F, 0x21, 0x02, 0xA1, 0xFC, 0x51, 0xC1, 0xDB, 0x29, 0x43, 0x88, 0x72, 0xA6, 0x9E, 0xF7,
            0xC4, 0x4F, 0xC8, 0x1B, 0x79, 0x07, 0xB6, 0x8B, 0x76, 0xB1, 0x5F, 0x97, 0x0F, 0x37, 0x9C, 0xF6,
            0xBB, 0x84, 0x1F, 0x4A, 0x75, 0x59, 0xFE, 0x8B, 0xDC, 0x20, 0xB6, 0x3C, 0xE6, 0x6E, 0x64, 0xCA,
            0x50, 0x15, 0x7D, 0x35, 0x40, 0x75, 0x65, 0xC2, 0x2C, 0xFC, 0x30, 0xDF, 0x3F, 0x53, 0xF5, 0xD6,
            0x2A, 0x53, 0x34, 0x11, 0x76, 0x4A, 0xF2, 0x09, 0xFD, 0x83, 0x45, 0x93, 0x8B, 0x53, 0x94, 0x1B,
            0xCF, 0xF0, 0x7A, 0x7D, 0x5E, 0x36, 0x2C, 0xCB, 0x6C, 0xF9, 0xAB, 0x9E, 0xAB, 0xFD, 0x31, 0x5D,
            0xC3, 0x0F, 0x44, 0xE4, 0x19, 0x25, 0xC9, 0x16, 0x8C, 0x61, 0x13, 0xD6, 0xA4, 0x2A, 0x22, 0xD1,
            0x8C, 0x79, 0x9B, 0x96, 0xF0, 0xD5, 0x5B, 0xDA, 0x6B, 0xE1, 0xEA, 0x4B, 0xF1, 0xCC, 0x1B, 0xC1,
            0x99, 0x23, 0x09, 0xE2, 0xA6, 0xFF, 0xA9, 0x30, 0xDF, 0xE4, 0x2A, 0xC7, 0x7A, 0x5C, 0x51, 0xA7,
            0x9D, 0x79, 0xF3, 0xFD, 0x29, 0x50, 0xC1, 0x37, 0xB4, 0xAA, 0xBF, 0x46, 0xDE, 0x20, 0x32, 0x59,
            0x07, 0x89, 0x6C, 0xB7, 0xBC, 0x8D, 0x42, 0x9C, 0x21, 0x7A, 0x94, 0x12, 0xB5, 0xA4, 0x4B, 0xDE,
            0xD9, 0xA2, 0x3F, 0x8B, 0xD5, 0x74, 0xAE, 0xC1, 0x3F, 0x52, 0x89, 0x35, 0xD8, 0x8B, 0xDD, 0xBC,
            0xC6, 0x94, 0x1B, 0x95, 0x80, 0x7C, 0x7A, 0x23, 0x44, 0x92, 0x21, 0x11, 0x62, 0x2A, 0x51, 0x09,
            0x9A, 0xF8, 0xE1, 0x24, 0x2C, 0xF8, 0x57, 0xF6, 0x37, 0xD8, 0x15, 0x9A, 0x62, 0xEB, 0xC6, 0x54,
            0xBB, 0xD3, 0x1A, 0x2D, 0x87, 0x92, 0x34, 0xCD, 0x17, 0xBC, 0xE5, 0xBE, 0xE5, 0xC0, 0x4B, 0xB5,
            0xE0, 0xA7, 0x7E, 0x9C, 0x38, 0x90, 0x8A, 0x1F, 0xB0, 0x4C, 0x4F, 0x3E, 0x18, 0xD7, 0x6E, 0x2D,
            0x27, 0x6C, 0x1A, 0x72, 0xAE, 0x62, 0xFF, 0x7B, 0x91, 0xB5, 0x24, 0x87, 0x22, 0x1A, 0xDB, 0x4F,
            0xAE, 0x33, 0x19, 0x58, 0x89, 0xA0, 0x56, 0x9D, 0xCB, 0xEF, 0x1D, 0x3B, 0x1C, 0x94, 0xB4, 0x8D,
            0x6F, 0xED, 0x21, 0xC9, 0xA1, 0xD3, 0xCA, 0x22, 0x5B, 0xE7, 0x93, 0x9A, 0x9E, 0x16, 0x67, 0xA6,
            0xF0, 0x65, 0x40, 0xF1, 0x47, 0x79, 0xAD, 0xA6, 0x9E, 0x48, 0xD7, 0x42, 0x46, 0x64, 0xB0, 0x99,
            0x0D, 0xBD, 0xD8, 0xD4, 0x5F, 0xB3, 0xE4, 0x93, 0xBC, 0xF8, 0xB8, 0xA9, 0x82, 0xA5, 0x53, 0xDB,
            0xA2, 0x70, 0x9F, 0x02, 0x25, 0x0A, 0x4C, 0xEB, 0xC1, 0x2C, 0xEE, 0x27, 0xC2, 0xA1, 0x12, 0x11,
            0x38, 0x0A, 0x34, 0x56, 0x68, 0x7E, 0xD9, 0x70, 0xA0, 0x7A, 0xD1, 0x53, 0x53, 0xA5, 0x07, 0x37,
            0xAB, 0xB5, 0x35, 0xBC, 0x7F, 0xCF, 0x0C, 0x90, 0x66, 0xB6, 0xB8, 0x37, 0x02, 0x27, 0x76, 0x05,
            0x33, 0xB7, 0x41, 0x60, 0x87, 0xC1, 0x4D, 0xC4, 0xA0, 0xCE, 0x2E, 0xCE, 0x78, 0x23, 0x30, 0x4D,
            0x78, 0x84, 0x5F, 0xC4, 0x7F, 0xA7, 0xE6, 0x70, 0xF2, 0x77, 0x63, 0x2D, 0xB7, 0x48, 0x41, 0xB8,
            0x77, 0x53, 0x67, 0xA6, 0x72, 0x99, 0x14, 0xD1, 0x71, 0x2F, 0xCB, 0xB8, 0x2B, 0x36, 0xB1, 0x2E,
            0xAC, 0xE4, 0xDB, 0x4C, 0xDD, 0xF4, 0x2B, 0x39, 0x9C, 0xD0, 0x4A, 0x06, 0x44, 0x0D, 0x86, 0x63
        };
    }
}