using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_112214 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[length + 256];
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += (header.m_buildVersion * (uint)header.m_dataCount) % 7;
            }
            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[(digest[7] * Keytable[0]) & 511];
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
            0x74, 0xF3, 0x47, 0x06, 0xB0, 0x45, 0x2E, 0x37, 0x06, 0x73, 0xA3, 0x34, 0x09, 0x21, 0x86, 0x65, 
            0x81, 0x41, 0x14, 0x33, 0xF2, 0x12, 0x2D, 0xDB, 0xC5, 0x1F, 0x34, 0x0B, 0x00, 0xDB, 0xFD, 0x0E, 
            0xAD, 0xAF, 0xE4, 0xB1, 0x6A, 0x59, 0xDD, 0x60, 0x86, 0xA2, 0x8B, 0x23, 0x77, 0xB2, 0x76, 0x9C, 
            0x11, 0x62, 0x2A, 0x86, 0x94, 0x4C, 0xD8, 0xCB, 0x5F, 0x9D, 0x64, 0x79, 0xFC, 0xDB, 0x06, 0xE5, 
            0xBE, 0xF7, 0x8D, 0x36, 0x56, 0x33, 0x9B, 0xC6, 0x0E, 0x2F, 0xB5, 0x5D, 0xD4, 0xE1, 0xAC, 0xDA, 
            0xFC, 0x22, 0x52, 0x8E, 0xB5, 0x34, 0xBE, 0xEC, 0x6C, 0x53, 0xE5, 0xE8, 0x6D, 0x3D, 0x9C, 0x1A, 
            0xC0, 0xEB, 0x3B, 0x22, 0xBE, 0x82, 0xBE, 0x0C, 0xD7, 0xA6, 0xB2, 0x7A, 0x17, 0x17, 0x5E, 0xB6, 
            0xC4, 0xCA, 0xB8, 0x78, 0xFC, 0xFF, 0x91, 0x19, 0xCA, 0x64, 0x6F, 0x30, 0xD5, 0x3A, 0x4B, 0x9C, 
            0x7E, 0xF0, 0xB6, 0x6F, 0xF2, 0x33, 0xBA, 0x73, 0xD9, 0xC7, 0x78, 0x11, 0xB0, 0x4A, 0x52, 0xC6, 
            0xEB, 0x9A, 0x73, 0x8A, 0x2C, 0xFD, 0xCB, 0xC3, 0x40, 0xFC, 0x8C, 0xC5, 0xF8, 0xB7, 0xC7, 0xA3, 
            0xEE, 0x8A, 0x5F, 0xF4, 0x1F, 0xFA, 0x65, 0xD7, 0xD4, 0x71, 0x32, 0xF9, 0x7B, 0xF0, 0xF8, 0x97, 
            0x62, 0x46, 0xB1, 0xA5, 0x3C, 0x61, 0x8A, 0x8D, 0x54, 0xC4, 0x52, 0x8B, 0x96, 0xBD, 0x25, 0xC5, 
            0x00, 0x8A, 0xE5, 0x2A, 0x6C, 0xC3, 0x78, 0x3B, 0x9C, 0x8E, 0x07, 0xAE, 0x94, 0x83, 0x2B, 0xA6, 
            0xA2, 0x01, 0x9A, 0x1F, 0xA2, 0x52, 0x89, 0xA8, 0xEE, 0x0D, 0xDF, 0xAE, 0x49, 0x5F, 0xE3, 0xC8, 
            0x6B, 0xDB, 0x5D, 0x0F, 0x53, 0xD0, 0xC8, 0xFE, 0x6A, 0x4E, 0x87, 0xE0, 0xEA, 0x3E, 0xDF, 0x50, 
            0x81, 0xD2, 0xA5, 0x9C, 0x4B, 0x2E, 0x2A, 0x78, 0x12, 0xAF, 0x1D, 0x22, 0x3C, 0x8B, 0x36, 0x7F, 
            0xD0, 0x58, 0x93, 0x7F, 0x64, 0x0B, 0x89, 0xC4, 0x25, 0x30, 0x12, 0xB6, 0x1B, 0x48, 0xD8, 0xE7, 
            0xE4, 0x62, 0x2F, 0x68, 0xAA, 0xD4, 0x7D, 0xEF, 0xC4, 0xDF, 0xD8, 0x01, 0x4E, 0x93, 0x5F, 0xBC, 
            0x77, 0x4F, 0xFD, 0xF9, 0xFF, 0x3C, 0x0C, 0x49, 0x5D, 0x27, 0xB4, 0x51, 0xA8, 0x6B, 0x03, 0xC5, 
            0x7A, 0x3C, 0x51, 0xB5, 0x52, 0xC9, 0xE1, 0x31, 0xEE, 0x54, 0xF0, 0x4C, 0xEB, 0x34, 0x34, 0x9A, 
            0x17, 0xD1, 0x7D, 0xC5, 0xEE, 0x3E, 0x31, 0x6B, 0x39, 0x3A, 0x22, 0xFA, 0x85, 0x2A, 0xD8, 0x67, 
            0x6F, 0x68, 0x5C, 0x26, 0x05, 0x40, 0x28, 0x6A, 0x3E, 0xF7, 0x08, 0xDD, 0x18, 0x35, 0xC1, 0x74, 
            0xA3, 0xE6, 0x30, 0x16, 0x8F, 0x27, 0x16, 0x7F, 0x04, 0x20, 0xF1, 0xA9, 0x51, 0xD3, 0x7C, 0xC0, 
            0xD1, 0x35, 0xC3, 0x3E, 0x99, 0xFB, 0xB8, 0x15, 0xB3, 0xFC, 0xB9, 0x15, 0x6B, 0xDB, 0x63, 0x41, 
            0xCD, 0x7B, 0xE0, 0x75, 0x68, 0x0B, 0x3A, 0x5E, 0x07, 0x78, 0xF0, 0xF8, 0x21, 0xFA, 0x32, 0xB9, 
            0x90, 0x3C, 0x4E, 0x18, 0x2A, 0x17, 0x05, 0x31, 0x57, 0x3D, 0xC4, 0x32, 0xB1, 0xBE, 0xBA, 0xB7, 
            0xF2, 0x97, 0x18, 0x16, 0x1D, 0x5F, 0x8D, 0x15, 0x16, 0xD0, 0x53, 0x69, 0xC9, 0xF2, 0xCA, 0x0C, 
            0x0A, 0xED, 0xD8, 0x5B, 0x98, 0x0E, 0x7A, 0x3E, 0x1C, 0x2E, 0xAD, 0x0B, 0xCC, 0x3B, 0x4F, 0xA2, 
            0x90, 0xF4, 0xBC, 0x69, 0xC0, 0xBC, 0x1D, 0x32, 0x41, 0x62, 0x80, 0x89, 0x56, 0x8B, 0xD0, 0xD8, 
            0x76, 0xB4, 0xC3, 0x71, 0xCC, 0x17, 0x21, 0x13, 0x4F, 0x79, 0xD3, 0x3A, 0x52, 0x3A, 0x93, 0x0D, 
            0x47, 0xC6, 0xD0, 0xD6, 0x15, 0x47, 0x95, 0x87, 0x16, 0x1C, 0xEB, 0x89, 0xCE, 0x8E, 0x3B, 0x9F, 
            0xE2, 0x6A, 0x78, 0xDF, 0x7C, 0x84, 0xE4, 0x02, 0xD2, 0xAA, 0xED, 0x95, 0xD5, 0x40, 0xC3, 0x33
        };
    }
}