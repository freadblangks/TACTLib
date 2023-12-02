using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_60360 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.m_buildVersion & 511];
            uint increment = kidx % 61;
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = (uint)((digest[7] + header.m_dataCount) & 511);
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
            0xE1, 0x89, 0x88, 0x7A, 0xCA, 0xC1, 0xA2, 0x53, 0x00, 0xF1, 0xED, 0xA0, 0x83, 0xB0, 0x8D, 0xB2, 
            0xA4, 0xEE, 0x2F, 0x9A, 0x1E, 0x6C, 0x69, 0x30, 0x41, 0x0D, 0x67, 0x53, 0x9A, 0xE9, 0xC1, 0x27, 
            0xBD, 0xFD, 0x3F, 0x3E, 0x92, 0x8F, 0x63, 0xD2, 0xC8, 0x29, 0x26, 0x02, 0x05, 0x5E, 0xD9, 0xD9, 
            0xBE, 0x6A, 0xFD, 0xD2, 0xAE, 0xF7, 0x41, 0x25, 0x8F, 0xAE, 0xC3, 0xAB, 0x66, 0xEB, 0xE4, 0x9C, 
            0xC6, 0x83, 0x2A, 0x89, 0x3E, 0xC2, 0x4E, 0xBD, 0x89, 0xE0, 0x7E, 0x4B, 0x76, 0xA7, 0xD3, 0x18, 
            0xEF, 0x78, 0x7E, 0x43, 0xD1, 0x69, 0x2D, 0x78, 0x64, 0x72, 0xFA, 0xFD, 0x51, 0x65, 0x79, 0x70, 
            0x38, 0x20, 0x3B, 0xF2, 0x6A, 0xCE, 0x3C, 0xEB, 0x88, 0xAF, 0xD9, 0x74, 0x41, 0x6F, 0xB5, 0x1F, 
            0x78, 0xDA, 0x58, 0x2A, 0xB0, 0xEF, 0x5D, 0x39, 0x97, 0x02, 0xCA, 0x51, 0x1B, 0x4E, 0x58, 0x65, 
            0xC4, 0x03, 0x01, 0xDD, 0xA7, 0xA9, 0x41, 0xE6, 0x0E, 0x67, 0x7D, 0x2C, 0x69, 0xE3, 0x52, 0x2B, 
            0x1E, 0x1D, 0x4C, 0xA6, 0xF8, 0x21, 0x03, 0xDA, 0x7E, 0x84, 0xF8, 0x15, 0x98, 0x22, 0xBA, 0x82, 
            0xAC, 0xBA, 0x8D, 0x87, 0xE1, 0xC3, 0xA3, 0x32, 0xDE, 0x0F, 0x75, 0x1D, 0x5E, 0x8E, 0x85, 0xA4, 
            0xD4, 0x68, 0x7B, 0x3B, 0x73, 0x7E, 0xDF, 0x40, 0x98, 0x5E, 0xD2, 0x82, 0x82, 0x68, 0xAF, 0xC4, 
            0x50, 0x71, 0x32, 0x5B, 0x6C, 0x93, 0x67, 0xCA, 0xF8, 0xB3, 0x80, 0x1F, 0xE8, 0x27, 0x72, 0x4D, 
            0x1C, 0x16, 0x08, 0xAF, 0x54, 0x67, 0x8C, 0x40, 0x8A, 0xEF, 0x46, 0x4C, 0x64, 0x77, 0x7F, 0x01, 
            0x9D, 0xBC, 0xCC, 0xCD, 0x51, 0xDB, 0xA4, 0x25, 0xF4, 0xA2, 0x0F, 0xF6, 0xE3, 0x82, 0x73, 0x61, 
            0xF2, 0x66, 0x88, 0xBE, 0xF3, 0x35, 0xD0, 0xD3, 0x11, 0xD9, 0xBA, 0xF9, 0x5A, 0x8D, 0x6B, 0xC9, 
            0x6F, 0x79, 0x5C, 0xE7, 0x52, 0x89, 0x66, 0x64, 0xE0, 0x40, 0x8A, 0xDD, 0x1D, 0x29, 0xEF, 0xEF, 
            0x8A, 0x60, 0x30, 0x66, 0x99, 0xA1, 0x3E, 0x15, 0x4D, 0x23, 0xB0, 0x2B, 0x46, 0x7D, 0x59, 0xC4, 
            0x82, 0x7F, 0x41, 0x5F, 0x93, 0xDB, 0xB4, 0xCD, 0x60, 0x1D, 0xF7, 0x48, 0x80, 0x18, 0x5D, 0xF8, 
            0x7D, 0xBD, 0x1F, 0xF4, 0xB5, 0x29, 0xD7, 0x45, 0x16, 0x1C, 0xFD, 0xF0, 0x33, 0xA6, 0xF5, 0x55, 
            0xEC, 0xCC, 0x06, 0x5E, 0x58, 0x64, 0xFF, 0x19, 0x6E, 0xAB, 0x8B, 0xAC, 0x89, 0xD1, 0x5A, 0xD1, 
            0x07, 0xCA, 0x19, 0x98, 0x41, 0xBA, 0x29, 0x61, 0x3C, 0xFB, 0xA4, 0x93, 0xB8, 0x5B, 0xFD, 0xE4, 
            0xB9, 0xAF, 0x29, 0x83, 0x75, 0xF0, 0xB3, 0xF5, 0x51, 0x94, 0x92, 0xA2, 0xC0, 0xFF, 0x52, 0x24, 
            0xCB, 0xB4, 0xCA, 0x3A, 0xF8, 0xBB, 0xEF, 0x9A, 0x45, 0xAD, 0x6F, 0x09, 0x8E, 0x59, 0x58, 0x61, 
            0xCA, 0x75, 0x62, 0x36, 0x03, 0x5D, 0x14, 0xF5, 0x39, 0xA0, 0x11, 0x06, 0x01, 0x54, 0x62, 0x91, 
            0x2F, 0xE8, 0xBF, 0x0A, 0xF2, 0xFF, 0x58, 0x1E, 0xD4, 0x80, 0x44, 0x6D, 0x46, 0x59, 0x96, 0x20, 
            0xDB, 0xA0, 0x59, 0x31, 0x2E, 0x57, 0x6E, 0x8C, 0xDA, 0xA5, 0x78, 0xC6, 0x98, 0xF5, 0xE3, 0x5C, 
            0x1B, 0xFC, 0x96, 0xCE, 0x42, 0x92, 0xEA, 0x45, 0xEA, 0xBC, 0x04, 0x6B, 0x53, 0x85, 0x92, 0x01, 
            0x69, 0xA5, 0xFB, 0x70, 0x3D, 0xCB, 0x2B, 0xC1, 0xF0, 0xFE, 0xB3, 0xAE, 0x0D, 0x9B, 0x08, 0x33, 
            0x2A, 0x28, 0x15, 0x87, 0x43, 0x8D, 0x37, 0xEA, 0xE5, 0x3B, 0xD3, 0x03, 0x1C, 0xCF, 0x93, 0xA6, 
            0x89, 0x7B, 0xD6, 0x83, 0x01, 0x62, 0x9C, 0xC5, 0x8C, 0x14, 0xE2, 0x8D, 0xD0, 0x4A, 0x10, 0x41, 
            0x56, 0xA5, 0x31, 0x2C, 0x1A, 0xA6, 0xEB, 0x9D, 0xBD, 0x86, 0xBF, 0xC3, 0x2C, 0xEB, 0xFC, 0xD0
        };
    }
}