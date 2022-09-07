using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ResourceGraph;

namespace TACTLib.Core.Product.Tank.TRG
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProTRG_104319 : ITRGEncryptionProc
    {
        public byte[] Key(TRGHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[length + 256];
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                switch (SignedMod(kidx, 3))
                {
                case 0:
                    kidx += 103;
                    break;
                case 1:
                    kidx = (uint)SignedMod(kidx * 4, header.m_buildVersion);
                    break;
                case 2:
                    --kidx;
                    break;
                }
            }
            return buffer;
        }

        public byte[] IV(TRGHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)(2 * digest[7]);
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += okidx % 61;
                buffer[i] ^= digest[SignedMod(kidx - i, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0xF5, 0xA1, 0x1C, 0xC6, 0xB9, 0xCF, 0x1F, 0xA2, 0x87, 0x51, 0x0A, 0x92, 0x57, 0x20, 0xA9, 0xBA, 
            0xE5, 0xAE, 0xB7, 0xC0, 0x5B, 0xDB, 0x8B, 0x7A, 0x5C, 0x0D, 0xE3, 0x14, 0x05, 0x42, 0xED, 0xBD, 
            0xA2, 0xA4, 0x8B, 0x6C, 0xBB, 0x30, 0xC2, 0x56, 0xBD, 0x35, 0xCC, 0xB3, 0xEC, 0x9A, 0x58, 0x95, 
            0x05, 0xF6, 0xEB, 0x52, 0x14, 0xC2, 0x71, 0x9E, 0x5C, 0x7E, 0x29, 0x3E, 0xD0, 0x72, 0xE9, 0x49, 
            0x50, 0xF3, 0xBF, 0xF6, 0x1B, 0xD2, 0x7E, 0xE7, 0x5F, 0x77, 0x4F, 0x1E, 0xB3, 0xC8, 0x4C, 0xAE, 
            0xCE, 0xE3, 0xCE, 0x66, 0xE2, 0xC9, 0x13, 0x2F, 0x6C, 0x19, 0x16, 0xFA, 0x8D, 0xD8, 0x48, 0xD2, 
            0xAD, 0xC0, 0x46, 0x8F, 0xF4, 0xC9, 0xBB, 0x55, 0x50, 0xCA, 0x41, 0xBF, 0x85, 0xFE, 0xC2, 0x58, 
            0xC7, 0x12, 0xC7, 0xDD, 0x1E, 0xC9, 0xB3, 0xB8, 0xDD, 0x89, 0x9B, 0x8B, 0x8E, 0x6F, 0x64, 0xF1, 
            0xE4, 0x09, 0x93, 0x93, 0x66, 0xEE, 0x61, 0x66, 0xCE, 0x66, 0x4A, 0x63, 0x2E, 0xC9, 0x49, 0x16, 
            0x48, 0xB7, 0x72, 0x16, 0x52, 0x57, 0x8D, 0xF0, 0xCF, 0x99, 0x79, 0x66, 0x6C, 0xCB, 0x82, 0xD2, 
            0x19, 0x9D, 0xBD, 0x38, 0x9E, 0x2B, 0x6A, 0x7B, 0x41, 0xD9, 0x72, 0x64, 0x88, 0x78, 0x69, 0x75, 
            0x4E, 0xCE, 0xBE, 0x35, 0x14, 0x45, 0x06, 0x33, 0xB3, 0x62, 0x25, 0x58, 0xAA, 0x00, 0x6E, 0xC6, 
            0x8A, 0x59, 0xEE, 0x80, 0xBE, 0x29, 0x37, 0xBE, 0x36, 0x5C, 0xF9, 0x12, 0x0A, 0x9A, 0x94, 0x92, 
            0xC7, 0x31, 0x6D, 0xA4, 0x66, 0x0A, 0x7F, 0x91, 0x94, 0xBA, 0x47, 0x49, 0x79, 0xAF, 0x1D, 0xE5, 
            0x5B, 0xCD, 0xEF, 0xE9, 0x66, 0x95, 0xE2, 0x5A, 0xB9, 0xED, 0xB8, 0xEA, 0xDA, 0xAF, 0x2C, 0x6D, 
            0xDF, 0xD0, 0x5A, 0x6A, 0x50, 0x4D, 0x37, 0x39, 0xA1, 0xE6, 0x38, 0x7B, 0x7B, 0x89, 0xBD, 0x42, 
            0x32, 0xE9, 0xE3, 0x93, 0xBC, 0x0C, 0x0B, 0xA0, 0xAD, 0x19, 0x27, 0x01, 0xA3, 0xE9, 0x38, 0x40, 
            0x68, 0xB5, 0x42, 0x0C, 0xFD, 0x27, 0xA7, 0x49, 0x08, 0x2A, 0x70, 0xE7, 0x72, 0x3B, 0x46, 0x34, 
            0x19, 0x19, 0x94, 0x19, 0x4D, 0x69, 0x77, 0x22, 0x3E, 0xDF, 0x77, 0xE4, 0x0C, 0x1B, 0x55, 0x43, 
            0x16, 0xD8, 0x14, 0x19, 0xFE, 0xA5, 0xEF, 0x68, 0xCB, 0x86, 0x89, 0x5B, 0x2E, 0x52, 0xDE, 0x36, 
            0x13, 0xFC, 0x60, 0x4C, 0x11, 0x2F, 0xFB, 0xF1, 0xA7, 0xDA, 0x2B, 0xDB, 0x4C, 0x4B, 0x2C, 0x11, 
            0x95, 0x10, 0xAB, 0x12, 0x27, 0x6A, 0x9F, 0x59, 0x00, 0x17, 0xEF, 0xC8, 0x1D, 0xEF, 0xCD, 0xD7, 
            0x82, 0x42, 0xED, 0x96, 0x59, 0xCE, 0x38, 0xF2, 0x77, 0x95, 0x4D, 0x61, 0x5D, 0x7B, 0xAA, 0xCA, 
            0xD8, 0x60, 0x0B, 0xA6, 0x7F, 0x48, 0x42, 0x60, 0xA1, 0xE5, 0x37, 0xEB, 0x1B, 0x2E, 0x14, 0x3B, 
            0xB5, 0xB5, 0xE5, 0x79, 0x1D, 0xF3, 0x94, 0x1F, 0xCE, 0xC9, 0x91, 0xF5, 0x86, 0xE0, 0x55, 0x58, 
            0x73, 0x1B, 0xEF, 0x1D, 0xEA, 0x73, 0x2F, 0x0A, 0x69, 0xA8, 0xEE, 0x4F, 0x06, 0x98, 0x7A, 0x71, 
            0x22, 0x23, 0x10, 0x43, 0xE5, 0x14, 0xE2, 0xB5, 0x3C, 0x05, 0xDF, 0x0E, 0x13, 0xFC, 0x60, 0x74, 
            0x7E, 0x33, 0xAB, 0x11, 0xC7, 0x0E, 0x67, 0x64, 0x9D, 0xD3, 0x01, 0xDC, 0xEB, 0x99, 0x0D, 0x5F, 
            0xA3, 0xCB, 0xB5, 0x46, 0x98, 0xA8, 0x8D, 0xE8, 0xC0, 0x14, 0x91, 0xD2, 0x83, 0xBA, 0x75, 0x27, 
            0x42, 0xCE, 0x5F, 0x41, 0xA1, 0x2E, 0xFB, 0x96, 0xF7, 0x16, 0xCD, 0x22, 0x7A, 0x5A, 0x38, 0x20, 
            0x36, 0x1C, 0xE5, 0x24, 0xB3, 0x0E, 0x06, 0x41, 0xC6, 0xBD, 0x14, 0xF6, 0x34, 0xF1, 0xF7, 0xF8, 
            0x2F, 0xD2, 0x83, 0xE3, 0xB1, 0x26, 0x0A, 0x24, 0xE9, 0x5D, 0xE0, 0x93, 0xAB, 0xFC, 0x69, 0xAE
        };
    }
}