using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ResourceGraph;

namespace TACTLib.Core.Product.Tank.TRG
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProTRG_70250 : ITRGEncryptionProc
    {
        public byte[] Key(TRGHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[header.m_buildVersion & 511];
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                switch (SignedMod(kidx, 3))
                {
                case 0:
                    kidx += 1273;
                    break;
                case 1:
                    kidx = (uint)SignedMod(kidx * 4, header.m_buildVersion);
                    break;
                case 2:
                    kidx -= 17;
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
                kidx += (uint)(header.m_buildVersion * header.m_skinCount) % 7;
                buffer[i] ^= digest[SignedMod(kidx - 73, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x4B, 0xFA, 0xBB, 0x77, 0x48, 0x36, 0xB3, 0x0A, 0xC8, 0xC1, 0xFE, 0xA4, 0x5C, 0x45, 0x06, 0x62, 
            0x18, 0x70, 0xE3, 0x37, 0xFA, 0x5D, 0xCA, 0xAB, 0xA3, 0x56, 0xA8, 0x3E, 0x94, 0x80, 0x13, 0xCD, 
            0xA6, 0x1A, 0x4C, 0x73, 0x18, 0xFD, 0xBA, 0xF3, 0x9D, 0xD1, 0xAD, 0xF0, 0x10, 0x16, 0x76, 0x0B, 
            0xBC, 0xBF, 0xF4, 0x52, 0xE6, 0xFA, 0x5C, 0x46, 0xBD, 0x17, 0x7A, 0x60, 0x8C, 0xC8, 0x1F, 0x68, 
            0xB3, 0x1E, 0x03, 0x3C, 0xC6, 0x84, 0x11, 0xE3, 0x41, 0xBC, 0xE7, 0xC0, 0xDB, 0x7B, 0x38, 0x4B, 
            0xCF, 0xEB, 0xAB, 0x40, 0x1C, 0x87, 0x31, 0xD4, 0xE0, 0x19, 0x29, 0xF2, 0x0F, 0x86, 0x50, 0xDF, 
            0x93, 0x3A, 0xC4, 0xEE, 0xFB, 0xB0, 0xBF, 0x15, 0xE7, 0x9A, 0x33, 0x29, 0x69, 0x92, 0xB8, 0x9F, 
            0x8F, 0xE0, 0x9C, 0xBC, 0x21, 0x66, 0x19, 0xF9, 0x7B, 0x50, 0x21, 0x87, 0xCC, 0xF0, 0xC6, 0xDA, 
            0x5E, 0x45, 0x42, 0xA2, 0xC9, 0xEA, 0x0D, 0xBE, 0xBE, 0x03, 0x76, 0x03, 0xCE, 0xAB, 0xD2, 0x21, 
            0x3D, 0x40, 0x23, 0xB1, 0x67, 0x3E, 0x71, 0x32, 0xAB, 0xB2, 0x16, 0x42, 0xDB, 0xBF, 0x07, 0x72, 
            0x15, 0xDB, 0xD4, 0x4E, 0xA9, 0x8B, 0x95, 0xF9, 0x5D, 0x52, 0xE8, 0xAB, 0xFB, 0xB4, 0xC2, 0x80, 
            0xA4, 0xF8, 0x1A, 0x15, 0x3F, 0xFA, 0xD8, 0x80, 0x07, 0xCF, 0xA4, 0x52, 0x2E, 0x9D, 0xB9, 0x91, 
            0xBB, 0x3A, 0x80, 0x17, 0xF5, 0x56, 0xFC, 0xA7, 0x8F, 0x7D, 0x57, 0x05, 0x24, 0xD9, 0xAE, 0xD4, 
            0x06, 0xDC, 0x24, 0xCE, 0x3B, 0x93, 0x4D, 0xF3, 0x0E, 0x9E, 0xDB, 0x45, 0xD2, 0x86, 0x2E, 0xAC, 
            0x1B, 0x43, 0x04, 0x30, 0xDB, 0x71, 0x15, 0x5F, 0xE8, 0x9E, 0x12, 0xF2, 0xCE, 0x75, 0x33, 0xA4, 
            0xD2, 0x95, 0xF7, 0x6D, 0x58, 0x87, 0x13, 0x36, 0xAA, 0xE5, 0xA5, 0x2B, 0x63, 0xF1, 0x98, 0xFB, 
            0x28, 0x7D, 0x1B, 0x09, 0x9C, 0x57, 0x81, 0x56, 0x01, 0x5F, 0x85, 0x6F, 0xED, 0xE0, 0x37, 0xEC, 
            0xF7, 0xC7, 0xD1, 0xD9, 0x09, 0x39, 0xFF, 0xEF, 0xBB, 0x97, 0x3E, 0xD1, 0xBD, 0xE1, 0x81, 0x53, 
            0x7F, 0x02, 0x07, 0xF5, 0x42, 0x50, 0x53, 0xE8, 0x71, 0x00, 0x8C, 0xFA, 0x14, 0x3B, 0xDB, 0xEE, 
            0xC7, 0x75, 0x07, 0x62, 0x4B, 0x93, 0x9C, 0x1C, 0x9B, 0x43, 0x4F, 0xFF, 0xB8, 0x78, 0xEF, 0x1C, 
            0xA7, 0xBD, 0xD5, 0xD8, 0x42, 0x41, 0xB8, 0x7D, 0xCC, 0xEC, 0x38, 0xE5, 0x3A, 0xA2, 0x0A, 0x5E, 
            0x15, 0x86, 0x9D, 0xE4, 0x05, 0xB3, 0xDD, 0x31, 0x96, 0x5E, 0x6D, 0x5D, 0xF3, 0x08, 0x7F, 0xB0, 
            0xAB, 0xE4, 0x01, 0x8D, 0xA1, 0x95, 0x6C, 0xB1, 0x32, 0x3E, 0xDD, 0x66, 0x9F, 0x5B, 0x07, 0xDE, 
            0x5F, 0xCC, 0x4D, 0xBA, 0xB6, 0xE4, 0x06, 0x97, 0x63, 0x49, 0x1A, 0x9C, 0x3B, 0xBE, 0x20, 0xF3, 
            0x2C, 0xA2, 0xC0, 0x6F, 0xEB, 0x16, 0x36, 0x9E, 0x18, 0xC5, 0x23, 0xD7, 0x00, 0xAF, 0xA6, 0xCB, 
            0x2F, 0x49, 0x87, 0x27, 0xE6, 0xF3, 0x00, 0xA4, 0x6D, 0x0C, 0x08, 0xA0, 0xA5, 0xF2, 0xCC, 0x2F, 
            0xEE, 0x6D, 0xB8, 0xDA, 0xF2, 0x70, 0x94, 0xED, 0x97, 0x43, 0xAF, 0x44, 0x3D, 0xCF, 0x53, 0xAA, 
            0x3E, 0x3A, 0x03, 0xF1, 0x81, 0x0C, 0xDB, 0x0C, 0x7D, 0x81, 0xF8, 0x44, 0xB3, 0x63, 0x4F, 0xBA, 
            0x1D, 0x2B, 0x64, 0x9D, 0xDE, 0x4C, 0x7A, 0xA6, 0x09, 0x37, 0x8A, 0x46, 0x41, 0x20, 0xF6, 0xE6, 
            0xAF, 0xB1, 0xA8, 0x21, 0x4B, 0x18, 0xFC, 0x7B, 0x14, 0xA0, 0xA3, 0x58, 0x29, 0x84, 0xDE, 0x13, 
            0x2F, 0x63, 0xFA, 0x2A, 0xEB, 0xEA, 0x8E, 0xAD, 0x0C, 0xC5, 0x5E, 0x98, 0xAF, 0xCB, 0x8D, 0x37, 
            0xFA, 0x1E, 0x4F, 0x7E, 0xD9, 0xC0, 0x36, 0x5E, 0xFC, 0x1E, 0xC3, 0x07, 0x3E, 0x5F, 0x06, 0xCD
        };
    }
}