using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_68780 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
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

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)(2 * digest[5]);
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += okidx % 29;
                buffer[i] ^= (byte)(digest[SignedMod(kidx + header.m_dataCount, SHA1_DIGESTSIZE)] + 1);
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0xA0, 0x18, 0x1B, 0x6F, 0x84, 0xB6, 0x9A, 0x3F, 0x55, 0x24, 0x8D, 0xBD, 0x04, 0xAF, 0xD6, 0xD5, 
            0x6F, 0x71, 0xC4, 0x49, 0x52, 0xCF, 0x41, 0x4E, 0xC0, 0xD9, 0xE9, 0x4B, 0x21, 0x0B, 0xD3, 0x07, 
            0x4A, 0x29, 0xE6, 0x2B, 0x22, 0xCA, 0x70, 0x94, 0xE2, 0x06, 0x99, 0xDB, 0x1F, 0x59, 0x19, 0xF0, 
            0xD3, 0x70, 0x29, 0xEC, 0x9A, 0x96, 0xCC, 0xEF, 0x04, 0x6F, 0xDE, 0xE7, 0x6B, 0x44, 0xD4, 0xA3, 
            0x62, 0x83, 0x50, 0x31, 0x8C, 0x21, 0x0E, 0x41, 0x44, 0xFC, 0x21, 0x4B, 0x88, 0x18, 0x55, 0x1D, 
            0xAE, 0x36, 0x2E, 0x7E, 0xB4, 0xD6, 0x75, 0x39, 0x44, 0xC9, 0xE3, 0xF2, 0xA9, 0x95, 0xE0, 0x20, 
            0xB7, 0x1E, 0xF6, 0x6F, 0xBD, 0xDA, 0x74, 0x46, 0x40, 0x13, 0x21, 0xD1, 0x51, 0xCA, 0x0B, 0x71, 
            0xD8, 0x72, 0x94, 0x88, 0x46, 0x8F, 0x66, 0xF8, 0xDB, 0x12, 0xE5, 0x42, 0xCA, 0xB4, 0xD7, 0x05, 
            0x71, 0x8B, 0xF9, 0xD2, 0x33, 0xC5, 0xC8, 0xBC, 0x2E, 0x25, 0x9C, 0x6C, 0x02, 0xCB, 0xDB, 0xD3, 
            0xF4, 0x7E, 0x54, 0xFB, 0x0C, 0xD2, 0x40, 0x2D, 0xB8, 0x6E, 0x12, 0xBC, 0x5D, 0x8C, 0x32, 0x87, 
            0x97, 0xD3, 0xB4, 0x73, 0xEE, 0xF9, 0x4C, 0x2A, 0x6A, 0x4D, 0x81, 0x07, 0x6D, 0xB3, 0xF5, 0xE9, 
            0xAF, 0x13, 0xB9, 0x0E, 0x5C, 0xE1, 0x7A, 0x05, 0xE7, 0x94, 0x91, 0x40, 0x09, 0xA3, 0x32, 0x4C, 
            0x0B, 0x30, 0x9B, 0x28, 0x89, 0xEC, 0xB6, 0x37, 0x36, 0xDF, 0x89, 0x51, 0x15, 0x25, 0xD6, 0xE1, 
            0x4D, 0x42, 0xD6, 0x90, 0x2C, 0x66, 0xEC, 0x4F, 0x96, 0x95, 0xCD, 0xBD, 0xDB, 0x91, 0x77, 0x6F, 
            0x20, 0xBD, 0xF5, 0x91, 0x40, 0x85, 0x02, 0x39, 0x4E, 0x0D, 0x40, 0x45, 0x25, 0x2E, 0x0D, 0x2C, 
            0x4A, 0x6E, 0x3C, 0xB1, 0xD2, 0x14, 0xDD, 0x6C, 0x87, 0xF4, 0xA1, 0x8A, 0x21, 0xCF, 0x51, 0x75, 
            0x61, 0xFD, 0x20, 0x1D, 0x7E, 0xF8, 0x1B, 0xD4, 0x02, 0x62, 0xE8, 0x7E, 0x94, 0x2A, 0x76, 0x73, 
            0x8B, 0x3A, 0xF5, 0xDE, 0x10, 0x5F, 0xC6, 0x8E, 0x99, 0xBC, 0x73, 0x15, 0x87, 0x8A, 0x71, 0xA5, 
            0x10, 0x18, 0xA3, 0xF7, 0x33, 0xBD, 0x62, 0x90, 0x7E, 0xB1, 0x32, 0xC7, 0xAF, 0xA2, 0xDE, 0xE5, 
            0x39, 0x6E, 0x05, 0x34, 0xC5, 0xBE, 0xF5, 0x13, 0x5D, 0x9F, 0xB7, 0x13, 0xAE, 0xA3, 0xA8, 0xC6, 
            0xEB, 0xDA, 0x83, 0x95, 0xDA, 0x3C, 0x25, 0xD0, 0x8E, 0xC4, 0x0F, 0x0C, 0x23, 0xF7, 0x2C, 0x67, 
            0x98, 0xF3, 0xF4, 0xD9, 0x05, 0x2C, 0x02, 0x55, 0xE3, 0x64, 0xFB, 0x90, 0xAD, 0x3D, 0xCD, 0x31, 
            0x1D, 0xF8, 0xEC, 0x5A, 0x81, 0x76, 0x59, 0x9B, 0xDE, 0x57, 0xB9, 0x9F, 0xF0, 0x95, 0x12, 0xEE, 
            0x35, 0xFA, 0x16, 0x76, 0x79, 0x93, 0x34, 0xEA, 0xDC, 0x6B, 0x21, 0xF6, 0xC7, 0x08, 0x04, 0xD4, 
            0xFD, 0xC7, 0xCB, 0xE6, 0xA1, 0xED, 0xF0, 0xCF, 0x41, 0x5B, 0x3D, 0xEE, 0xDF, 0xC5, 0x6D, 0x6C, 
            0x8D, 0xB8, 0x65, 0x5F, 0x91, 0x13, 0x5B, 0x4C, 0xD6, 0x67, 0x57, 0x9D, 0x01, 0x79, 0x35, 0x2B, 
            0x9B, 0xAB, 0xBC, 0x67, 0xD4, 0x37, 0x4A, 0x45, 0x1D, 0x8A, 0xA7, 0x12, 0x38, 0x1E, 0xF6, 0xE6, 
            0xA8, 0xE4, 0xAC, 0x26, 0x59, 0xCD, 0x35, 0x26, 0x0D, 0xB3, 0xBC, 0x5A, 0x66, 0xF4, 0xDB, 0x41, 
            0x49, 0x76, 0x3B, 0x33, 0x14, 0x68, 0x73, 0xDF, 0xA9, 0x62, 0x8F, 0x90, 0x7F, 0xA4, 0xE6, 0xF1, 
            0x1F, 0xDE, 0x0A, 0x7C, 0x32, 0x02, 0x91, 0x14, 0x60, 0x8D, 0x12, 0x6C, 0x7E, 0x11, 0x1C, 0x4C, 
            0xFC, 0xD5, 0xF2, 0x27, 0x35, 0x55, 0x96, 0x94, 0x8D, 0x88, 0x08, 0xB7, 0x00, 0x59, 0x01, 0xD0, 
            0x89, 0x27, 0x2D, 0xB5, 0x76, 0x38, 0x61, 0x7C, 0x43, 0x49, 0xDD, 0x3F, 0x0A, 0xCD, 0xD6, 0x1B
        };
    }
}