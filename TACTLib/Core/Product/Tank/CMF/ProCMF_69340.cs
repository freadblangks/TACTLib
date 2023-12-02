using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_69340 : ICMFEncryptionProc
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
                kidx += okidx % 13;
                buffer[i] ^= digest[SignedMod(kidx + header.GetNonEncryptedMagic(), SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0xAB, 0xD8, 0x24, 0x4F, 0x30, 0x91, 0xD3, 0x13, 0x33, 0x52, 0x84, 0x5A, 0xFB, 0x99, 0xC9, 0x6C, 
            0x6D, 0xAD, 0x41, 0xCB, 0x3A, 0x09, 0x6E, 0x9C, 0x3D, 0x9B, 0xAB, 0x89, 0xF2, 0x2D, 0xB6, 0x0F, 
            0x2B, 0x55, 0xFC, 0x47, 0x3D, 0x91, 0x74, 0x60, 0xBD, 0xE5, 0x50, 0x2A, 0xD4, 0x22, 0x66, 0x6F, 
            0xEC, 0x07, 0x82, 0xB9, 0x64, 0x8F, 0xB4, 0x3E, 0xF4, 0x4A, 0x80, 0x24, 0x4C, 0x9A, 0x72, 0xE2, 
            0xC9, 0x85, 0xF9, 0x45, 0x07, 0x42, 0xF3, 0xB2, 0x23, 0x18, 0x82, 0xDB, 0x3A, 0x5E, 0xC5, 0x7E, 
            0x12, 0x71, 0x34, 0xB1, 0xD5, 0x72, 0x83, 0x68, 0x58, 0xB8, 0xAA, 0xE0, 0x89, 0xC0, 0x21, 0x4B, 
            0x37, 0x60, 0xE2, 0x17, 0xF7, 0xCA, 0x77, 0x87, 0x9C, 0xAC, 0x9F, 0xED, 0xF7, 0x5E, 0x4B, 0xC5, 
            0x76, 0xAB, 0x27, 0x23, 0xB9, 0x7E, 0x26, 0xAE, 0xB8, 0xB7, 0xA2, 0x32, 0x46, 0xE0, 0xA8, 0x85, 
            0xC1, 0x0D, 0x72, 0x6A, 0x43, 0x8D, 0x5D, 0xBA, 0x77, 0xDE, 0x65, 0x18, 0x8E, 0xDC, 0xDB, 0x94, 
            0x6E, 0x02, 0x44, 0xA6, 0xA2, 0xD4, 0xC9, 0xE8, 0x2B, 0x48, 0x5D, 0x97, 0x6A, 0x22, 0x1C, 0x58, 
            0x97, 0xB7, 0x71, 0x9B, 0xC5, 0x6A, 0xEC, 0xE1, 0x31, 0x72, 0xB2, 0xF9, 0x8A, 0xEE, 0xC6, 0x36, 
            0x46, 0x6E, 0x61, 0xDD, 0x78, 0xEF, 0x6A, 0xAD, 0xCF, 0x2F, 0xCD, 0x1C, 0xCA, 0xED, 0x53, 0xE2, 
            0x90, 0x23, 0x00, 0xCD, 0xAA, 0x97, 0xA4, 0xA0, 0x60, 0x63, 0x9A, 0xD2, 0xB6, 0xEB, 0x5C, 0x61, 
            0x34, 0x7F, 0x26, 0x71, 0x79, 0x2D, 0xE0, 0x56, 0x90, 0xB0, 0x61, 0xFA, 0xA3, 0x0C, 0x3B, 0x8F, 
            0x57, 0x7F, 0x95, 0x5F, 0x9D, 0x23, 0xEE, 0x91, 0x1D, 0x15, 0x5A, 0xB9, 0x6F, 0xED, 0xF3, 0x67, 
            0xA0, 0x0B, 0x7B, 0xD4, 0x22, 0x62, 0x62, 0xD4, 0xD0, 0x71, 0xF5, 0xE4, 0x06, 0xF4, 0xCF, 0xB0, 
            0xD5, 0x05, 0xC9, 0x49, 0x56, 0x54, 0xFE, 0xD5, 0x5F, 0xF1, 0xAD, 0xE2, 0xA5, 0x08, 0x37, 0x68, 
            0xD5, 0x7A, 0xD0, 0x18, 0xC5, 0x8D, 0xA3, 0x64, 0x9F, 0xFE, 0x20, 0x77, 0x03, 0x77, 0xA1, 0x11, 
            0x5F, 0x95, 0x8A, 0x53, 0x05, 0xCB, 0x0E, 0x02, 0x03, 0x6B, 0xFF, 0xC8, 0xD1, 0x11, 0xE4, 0x9D, 
            0xE0, 0x18, 0x4C, 0x28, 0x67, 0x9A, 0x7C, 0xB9, 0x9D, 0x14, 0x6B, 0x7D, 0xCD, 0xB8, 0xD9, 0xC2, 
            0xC9, 0x1F, 0x44, 0x42, 0xAF, 0x82, 0xD3, 0x19, 0x76, 0x90, 0x87, 0xAF, 0x62, 0x8D, 0x42, 0x03, 
            0x56, 0x7E, 0x41, 0xDB, 0xC1, 0x5A, 0x4C, 0x05, 0x93, 0x3C, 0x0F, 0x20, 0xC1, 0x51, 0xE0, 0x86, 
            0xBA, 0xFA, 0xA2, 0x8E, 0xFA, 0x66, 0xDD, 0x04, 0xA2, 0xB6, 0x35, 0xD5, 0xBB, 0x33, 0x7A, 0xE0, 
            0x1B, 0x46, 0xC6, 0xB1, 0x53, 0x88, 0x80, 0xFD, 0xB1, 0x05, 0x5D, 0xAE, 0x51, 0x8C, 0x2B, 0xAF, 
            0xB1, 0xC7, 0x78, 0xDF, 0x03, 0x91, 0x9C, 0xC2, 0x20, 0xB0, 0x47, 0x1F, 0xFB, 0x01, 0x49, 0xA8, 
            0x02, 0x89, 0x31, 0x05, 0x6A, 0xF3, 0xD3, 0xDC, 0x8B, 0xC0, 0x62, 0xA9, 0x2F, 0xA8, 0x2F, 0x3F, 
            0xFA, 0xEC, 0xAF, 0xB2, 0x75, 0xAC, 0x0A, 0x3F, 0x0E, 0xCF, 0x8F, 0x53, 0x4A, 0xFD, 0x85, 0xFC, 
            0xDF, 0x2D, 0x3B, 0xB7, 0x42, 0x8A, 0x47, 0x2A, 0xC1, 0xFE, 0x84, 0xC2, 0xCC, 0x31, 0x38, 0x1A, 
            0xB6, 0xCD, 0xCD, 0x15, 0x81, 0xEF, 0xE1, 0xCE, 0xCB, 0x98, 0x09, 0x8B, 0x0D, 0x6F, 0xAA, 0x28, 
            0x15, 0x33, 0xEF, 0x92, 0x42, 0x02, 0x4C, 0xEC, 0x15, 0xAD, 0x26, 0x20, 0xF0, 0x5B, 0xF6, 0x3D, 
            0x71, 0x36, 0xD5, 0x85, 0x74, 0xE2, 0x39, 0xA1, 0xA4, 0xED, 0xD0, 0xA8, 0x44, 0xF4, 0xF0, 0x8F, 
            0x90, 0x50, 0x8E, 0x7E, 0x2B, 0xE4, 0x6A, 0xE4, 0x6F, 0xFB, 0xC9, 0x6E, 0x78, 0x42, 0xA8, 0xAE
        };
    }
}