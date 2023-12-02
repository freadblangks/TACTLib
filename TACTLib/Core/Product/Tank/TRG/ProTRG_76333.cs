using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ResourceGraph;

namespace TACTLib.Core.Product.Tank.TRG
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProTRG_76333 : ITRGEncryptionProc
    {
        public byte[] Key(TRGHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)(length * header.m_buildVersion);
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
                kidx += (header.m_buildVersion * (uint)header.m_skinCount) % 7;
                buffer[i] ^= digest[SignedMod(kidx - 73, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x92, 0x25, 0x6E, 0x53, 0x93, 0x64, 0x2C, 0x1E, 0x6E, 0x03, 0x23, 0x5C, 0x89, 0x20, 0xBF, 0xA4, 
            0xBD, 0x4C, 0x88, 0xE9, 0x40, 0x28, 0x6A, 0x54, 0xD6, 0xBC, 0x20, 0x01, 0x88, 0x80, 0x71, 0xF7, 
            0x73, 0x1F, 0xC1, 0x65, 0x99, 0x7B, 0x18, 0x20, 0xF4, 0x9B, 0xEF, 0xD9, 0x6A, 0x94, 0x5C, 0xEE, 
            0x36, 0x39, 0x12, 0xEF, 0x91, 0x5D, 0xDB, 0x79, 0xB7, 0xA7, 0xD6, 0x5F, 0xDF, 0x70, 0x54, 0x27, 
            0xDF, 0x52, 0xD0, 0xBD, 0x67, 0x71, 0xE1, 0x8D, 0x29, 0xDA, 0x43, 0x92, 0x28, 0x13, 0x35, 0x18, 
            0xD4, 0xC7, 0xEB, 0x97, 0x4D, 0xEA, 0x47, 0x1E, 0xDF, 0xCA, 0x39, 0x67, 0x8E, 0xAC, 0x3F, 0xD8, 
            0x04, 0x08, 0x5B, 0x8A, 0x95, 0xC9, 0x7B, 0xD0, 0x5B, 0x13, 0x2F, 0x1B, 0x95, 0x56, 0xE9, 0x54, 
            0x68, 0x1B, 0x01, 0x1E, 0x24, 0xBD, 0x11, 0xB0, 0x9C, 0x89, 0xC0, 0xE0, 0xD2, 0xDE, 0x87, 0x4D, 
            0x50, 0x1C, 0x25, 0xA6, 0xC8, 0x8E, 0x0D, 0xE0, 0x6C, 0xBC, 0xA3, 0xC3, 0x58, 0x8F, 0x7C, 0x00, 
            0xF9, 0x13, 0x40, 0xAE, 0x52, 0xDF, 0xDC, 0x22, 0x3D, 0xD3, 0x11, 0x64, 0x61, 0x08, 0x37, 0xBD, 
            0x79, 0xAD, 0x9C, 0x19, 0x13, 0x2F, 0x5E, 0x98, 0x1C, 0xB5, 0x4D, 0xA3, 0x20, 0x40, 0x2E, 0x86, 
            0xF9, 0x0C, 0x62, 0xF0, 0x6F, 0xB0, 0xF4, 0x14, 0x34, 0xE8, 0x04, 0xE3, 0x14, 0x73, 0x3E, 0x6C, 
            0xE5, 0x97, 0x85, 0x48, 0x43, 0xE3, 0x08, 0x87, 0xC1, 0x49, 0xCE, 0xAE, 0xBD, 0x3F, 0x78, 0xA4, 
            0xF2, 0xFE, 0xE1, 0x75, 0xC4, 0xEE, 0x0F, 0xBD, 0xA2, 0xE9, 0x6E, 0x20, 0x88, 0x49, 0xC7, 0xB8, 
            0x73, 0xAD, 0x0F, 0x6B, 0x8C, 0x79, 0x89, 0x92, 0x27, 0x2D, 0xA8, 0x0D, 0x79, 0xAF, 0xB3, 0x02, 
            0x1C, 0xE7, 0x06, 0x03, 0xAB, 0x33, 0xE0, 0x79, 0x46, 0x23, 0xA4, 0xCF, 0xD4, 0x31, 0xDF, 0x1F, 
            0x1A, 0x0C, 0x87, 0xDC, 0x05, 0xB8, 0xEF, 0x90, 0xC6, 0xFF, 0x6A, 0x3F, 0xE8, 0xFB, 0x7C, 0x3F, 
            0xE1, 0x9E, 0x0D, 0xEA, 0x7E, 0xC2, 0xAB, 0xB7, 0x88, 0x71, 0x4D, 0x12, 0xB3, 0x5D, 0x67, 0xDF, 
            0xD1, 0x18, 0x47, 0x70, 0x80, 0x3C, 0xDC, 0x71, 0xD8, 0xDF, 0xEA, 0xD6, 0x38, 0x13, 0xB7, 0x5D, 
            0x80, 0xB3, 0xD3, 0x3F, 0x16, 0x76, 0xE8, 0x66, 0xE2, 0x6F, 0xF2, 0xEC, 0xD8, 0xA3, 0x00, 0xEF, 
            0xC7, 0xC1, 0xA3, 0x4F, 0x86, 0x2B, 0xF0, 0xE2, 0xE2, 0x5A, 0xBB, 0x95, 0xC5, 0x1D, 0x17, 0xD2, 
            0xA1, 0x83, 0x16, 0xB9, 0x06, 0xF3, 0x81, 0xDA, 0x7C, 0x7A, 0xC6, 0x5D, 0x7E, 0x33, 0xE5, 0x19, 
            0x79, 0x6C, 0x77, 0x68, 0x14, 0xC8, 0xC0, 0x39, 0x07, 0x22, 0xDA, 0x5A, 0xF1, 0x7C, 0x5B, 0xF7, 
            0xCF, 0x3D, 0xBA, 0xF8, 0x44, 0x2A, 0x74, 0x4B, 0x0A, 0xB9, 0xFE, 0xA0, 0x6B, 0x8F, 0x1B, 0xF7, 
            0x3A, 0x8A, 0x0B, 0xE0, 0xCC, 0x3F, 0x59, 0xE8, 0xF2, 0x50, 0x0B, 0x6D, 0xFF, 0xDA, 0xAA, 0x2B, 
            0x28, 0xD1, 0xE7, 0xD1, 0x68, 0xAE, 0x77, 0x2F, 0xC1, 0xEC, 0x49, 0x1F, 0xEC, 0x33, 0x5D, 0x2D, 
            0xEC, 0x4D, 0x5A, 0x39, 0xAE, 0x1E, 0x49, 0x14, 0xA1, 0x72, 0x32, 0x09, 0x3D, 0x6E, 0xD2, 0xC2, 
            0xE0, 0x46, 0x2E, 0xD0, 0xB6, 0xC9, 0xFB, 0x0E, 0xFA, 0xC9, 0x7E, 0xF1, 0xB3, 0x75, 0x57, 0xDE, 
            0x9E, 0xBF, 0x1C, 0xC7, 0x18, 0x2F, 0xCE, 0x7A, 0x11, 0x4A, 0xF5, 0xE4, 0xAA, 0x34, 0x47, 0x6B, 
            0x03, 0xE8, 0x62, 0xE3, 0x23, 0xB5, 0x64, 0xF9, 0x5C, 0xE0, 0x61, 0x98, 0xC4, 0x3E, 0x09, 0x7B, 
            0xE7, 0xA3, 0x95, 0x8E, 0x16, 0xF7, 0xC9, 0xBA, 0x05, 0x4E, 0x3F, 0x34, 0x3E, 0x22, 0xF3, 0x65, 
            0xFC, 0xF9, 0xE3, 0x54, 0x51, 0x7A, 0xFB, 0xC5, 0x2A, 0x46, 0x97, 0xDC, 0x81, 0x67, 0x55, 0x67
        };
    }
}