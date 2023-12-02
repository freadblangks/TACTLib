using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_59573 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = header.m_buildVersion * (uint)length;
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.m_buildVersion - kidx;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];

            uint increment = Keytable[header.m_buildVersion & 511];
            uint kidx = increment;

            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];

                kidx += increment % 29;

                buffer[i] ^= (byte)(digest[SignedMod(kidx + header.m_entryCount, SHA1_DIGESTSIZE)] + 1);
            }

            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x77, 0xFF, 0xF5, 0x20, 0xF2, 0x2C, 0x44, 0x43, 0x15, 0x5D, 0x48, 0x1A, 0xDF, 0x6A, 0x0A, 0xD2,
            0xC5, 0x1E, 0xB3, 0x30, 0x4E, 0xA9, 0xAB, 0xFF, 0xC1, 0xC8, 0x9B, 0x71, 0xAC, 0x57, 0xE9, 0x43,
            0x30, 0xC9, 0x5B, 0x67, 0x9C, 0x69, 0x06, 0xAF, 0x14, 0x14, 0x0C, 0x39, 0xB9, 0xF2, 0xBB, 0x29,
            0x8F, 0x31, 0xF5, 0xD9, 0x2B, 0x32, 0x07, 0x51, 0xF4, 0xA3, 0x8E, 0xF0, 0xC2, 0xCF, 0xEF, 0x9D,
            0x24, 0xF5, 0xF9, 0x29, 0xB5, 0x00, 0x37, 0xCC, 0x15, 0x14, 0x79, 0x9A, 0x3B, 0x33, 0xEC, 0xE3,
            0xE9, 0x93, 0xD3, 0xFA, 0xA0, 0xD7, 0x5B, 0xC0, 0xD6, 0x27, 0x72, 0x1D, 0xAB, 0x5E, 0x9D, 0xAB,
            0xC8, 0xEE, 0x1E, 0xC6, 0x59, 0x90, 0x43, 0xBA, 0x58, 0xC1, 0x1E, 0x3A, 0x0F, 0x26, 0x85, 0x49,
            0x8E, 0xE8, 0x1C, 0xFE, 0x0F, 0xCD, 0x2B, 0x2C, 0xEF, 0xB2, 0x0E, 0xA9, 0x1D, 0x6E, 0x2C, 0xFB,
            0xC1, 0x04, 0x24, 0x13, 0x80, 0xC4, 0x1F, 0x18, 0x0C, 0x4A, 0x62, 0xB6, 0xDC, 0x70, 0xDA, 0x60,
            0x21, 0x65, 0x56, 0x2B, 0x1F, 0x1A, 0x70, 0xAA, 0x66, 0xD2, 0xBC, 0xEB, 0xDB, 0x13, 0x18, 0xAB,
            0xCF, 0x0D, 0x01, 0xE9, 0xCA, 0x1F, 0x5D, 0x1A, 0x1F, 0xCD, 0x6D, 0xD7, 0xDF, 0xF9, 0x81, 0xA0,
            0x09, 0x4D, 0xA0, 0x75, 0xAA, 0xBF, 0x32, 0x3A, 0x06, 0x42, 0xB0, 0x1B, 0xD7, 0x7F, 0x11, 0xDF,
            0x4E, 0xEF, 0xC5, 0x5B, 0xB0, 0xF9, 0xB5, 0x24, 0x69, 0x50, 0x45, 0x1F, 0xB7, 0xEF, 0xC2, 0x17,
            0xD2, 0xDA, 0x13, 0x98, 0xAB, 0xE6, 0x3D, 0xDE, 0xCA, 0x37, 0xE9, 0x6F, 0x35, 0xC8, 0xE0, 0x68,
            0xBA, 0x85, 0x36, 0x59, 0xDA, 0x44, 0x2F, 0x52, 0xB4, 0xDA, 0xA2, 0xCC, 0x2D, 0x40, 0xDD, 0x21,
            0x30, 0x60, 0x55, 0x86, 0x98, 0x67, 0x02, 0x86, 0x28, 0x0A, 0x67, 0xD5, 0x2B, 0x16, 0xEB, 0xEF,
            0xD7, 0xCA, 0x67, 0x59, 0x07, 0x23, 0x46, 0x32, 0x1A, 0x45, 0x53, 0xE5, 0x86, 0xF3, 0x30, 0xEF,
            0x8E, 0xD7, 0xBE, 0xAF, 0xD3, 0xCA, 0x9E, 0x87, 0x77, 0x71, 0x85, 0x69, 0x5C, 0x0A, 0xAA, 0xC8,
            0xD5, 0x4B, 0x4D, 0x9C, 0x5F, 0xA2, 0x8B, 0x98, 0xE1, 0x47, 0xBC, 0x46, 0x94, 0xDE, 0x4A, 0xF4,
            0xB1, 0x53, 0xC8, 0xD4, 0xF9, 0x69, 0x72, 0x5F, 0x4F, 0x67, 0x9F, 0x5D, 0xEF, 0xAA, 0x4A, 0x13,
            0x57, 0xEF, 0xF9, 0x79, 0x9A, 0x52, 0x03, 0xC7, 0xAD, 0xFA, 0x28, 0xA4, 0xE8, 0xEA, 0x53, 0x5E,
            0xD8, 0x94, 0xE4, 0xB8, 0x15, 0x27, 0x80, 0x83, 0xEA, 0xB0, 0x8A, 0xCF, 0x67, 0xCD, 0x70, 0xF3,
            0x87, 0x02, 0x17, 0x29, 0x91, 0xC9, 0xBD, 0x89, 0x84, 0x1C, 0x8B, 0x24, 0x31, 0xEF, 0x97, 0x5D,
            0xBC, 0x75, 0x5A, 0x43, 0x50, 0xD9, 0xF9, 0xF5, 0x39, 0xB9, 0x5D, 0xE1, 0x32, 0xA0, 0xBF, 0x98,
            0x7B, 0x6D, 0xBC, 0x7B, 0x35, 0x7C, 0xD0, 0x52, 0xA3, 0x68, 0x75, 0x92, 0x4A, 0xE6, 0x3F, 0x86,
            0x49, 0xA1, 0x8F, 0x5D, 0x6A, 0xAA, 0x9D, 0xE3, 0x86, 0x87, 0x3E, 0xC8, 0x3E, 0x8D, 0xC1, 0x4C,
            0x5A, 0x38, 0x01, 0x90, 0x56, 0x32, 0x66, 0x50, 0x98, 0x76, 0xD2, 0x1F, 0xB7, 0xE6, 0xD8, 0x43,
            0x8A, 0x46, 0x6D, 0x57, 0x4B, 0xAE, 0xD7, 0x20, 0x91, 0xD2, 0xC7, 0x12, 0x09, 0xFA, 0xAF, 0x8F,
            0x65, 0xC2, 0x63, 0x0E, 0x35, 0xE5, 0x53, 0x63, 0x3B, 0x6F, 0xBC, 0x33, 0xFF, 0xA4, 0x3D, 0x21,
            0x4D, 0x19, 0xA0, 0x5C, 0x55, 0x81, 0x6C, 0x02, 0x56, 0x21, 0xE1, 0x55, 0x40, 0x1A, 0xB9, 0xDE,
            0x74, 0x3D, 0x35, 0x50, 0x71, 0x9D, 0x29, 0x4F, 0x7C, 0x76, 0x62, 0xBE, 0x1B, 0x69, 0x1B, 0xEB,
            0x33, 0xEC, 0x45, 0xCB, 0xA1, 0x74, 0xE8, 0x3B, 0xF9, 0xA7, 0x62, 0x4F, 0x5F, 0xFE, 0x7C, 0x7C
        };
    }
}