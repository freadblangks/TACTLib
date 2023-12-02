using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_60038 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = (uint) (header.m_buildVersion * length);
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

            uint kidx = (uint) ((digest[7] + header.m_dataCount) & 511);
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += 3;
                buffer[i] ^= digest[SignedMod(kidx - i, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x5E, 0x3E, 0x8A, 0xFC, 0xC1, 0x27, 0xD1, 0x11, 0xAE, 0x7D, 0x3D, 0x86, 0x94, 0x99, 0x8E, 0xA4, 
            0xE3, 0x0D, 0xAB, 0x20, 0xF7, 0xA3, 0x55, 0x9A, 0xC1, 0x1F, 0x00, 0xF6, 0xF4, 0x51, 0x66, 0x50, 
            0xD9, 0xF6, 0xC4, 0x81, 0xEE, 0x73, 0x6A, 0xE7, 0xC4, 0x2F, 0x97, 0x82, 0x17, 0xF4, 0xDF, 0xDB, 
            0xE3, 0xB7, 0xF0, 0xC2, 0x20, 0x12, 0x48, 0xD2, 0x8B, 0xBC, 0x73, 0x1C, 0xD9, 0x3B, 0x8C, 0xEC, 
            0x26, 0x67, 0x96, 0x81, 0x34, 0xF6, 0x13, 0x92, 0x84, 0x2D, 0xB9, 0xCE, 0xF5, 0x81, 0x75, 0xB1, 
            0x2B, 0x51, 0x26, 0xDB, 0x3D, 0x7D, 0x3B, 0x06, 0x2C, 0xBC, 0xFF, 0x18, 0x57, 0x42, 0x93, 0x44, 
            0xAA, 0x57, 0x54, 0x3C, 0x06, 0xB0, 0x7F, 0x24, 0x11, 0x39, 0x18, 0x3B, 0x03, 0x75, 0xEC, 0x49, 
            0x42, 0x20, 0xF0, 0x0A, 0xEC, 0x61, 0x33, 0x6D, 0xDA, 0xE9, 0x0E, 0x75, 0x51, 0x67, 0xE2, 0x7F, 
            0x55, 0xD2, 0x2F, 0xD2, 0x02, 0xE5, 0x57, 0x26, 0xBC, 0x58, 0x7B, 0x3F, 0x9F, 0x48, 0xE6, 0x1F, 
            0x2F, 0xD1, 0xC5, 0x0D, 0xEE, 0x53, 0xC5, 0xD2, 0x6D, 0x24, 0x0A, 0x1C, 0x34, 0x4E, 0xF9, 0x9C, 
            0xAC, 0x6B, 0x29, 0x74, 0xC9, 0x87, 0x84, 0x83, 0x89, 0x20, 0x40, 0xD8, 0x0A, 0x7C, 0xF3, 0xC5, 
            0x8B, 0x59, 0x43, 0xA5, 0x32, 0x22, 0x2A, 0xDB, 0x8D, 0x78, 0x09, 0xF1, 0xA7, 0x32, 0x77, 0x80, 
            0x78, 0x7D, 0x22, 0x23, 0xC2, 0x50, 0xEE, 0x01, 0x15, 0x42, 0xB2, 0xD0, 0x11, 0x13, 0x4C, 0x42, 
            0x6C, 0x5B, 0x52, 0x22, 0xA1, 0x07, 0xBE, 0xF0, 0xA7, 0xD2, 0xF3, 0x28, 0xC6, 0xEF, 0xF8, 0xB0, 
            0x4D, 0x05, 0x2B, 0x04, 0xAE, 0xC9, 0x12, 0x53, 0xC7, 0x8B, 0x75, 0x14, 0xE4, 0xED, 0x53, 0xF1, 
            0x88, 0xFA, 0xB5, 0xA5, 0xA2, 0x14, 0xD8, 0xE7, 0xBA, 0xE6, 0x2C, 0x2F, 0x27, 0xA6, 0x08, 0x93, 
            0x3B, 0x13, 0x96, 0x74, 0xE4, 0x0D, 0xA2, 0xD5, 0xC8, 0x14, 0xD4, 0x8D, 0x2D, 0x70, 0xDE, 0xE8, 
            0x64, 0x2A, 0x8D, 0x4E, 0x9B, 0xF8, 0xF4, 0x75, 0x14, 0xBE, 0xD2, 0x62, 0xBE, 0x94, 0x97, 0xFB, 
            0x5F, 0x9B, 0x28, 0x62, 0xBA, 0x42, 0x6B, 0x75, 0xD2, 0xD6, 0xCD, 0x30, 0xB7, 0x62, 0x15, 0x18, 
            0xB4, 0x0A, 0x2A, 0xBE, 0x4F, 0x8E, 0x3B, 0x8B, 0xD1, 0x81, 0x2F, 0x37, 0x6B, 0x70, 0x2A, 0x0D, 
            0xD9, 0x84, 0xF0, 0x9D, 0xC8, 0x3E, 0x88, 0x5C, 0x80, 0xDE, 0xF3, 0x8F, 0x9C, 0xBC, 0x37, 0x4A, 
            0x06, 0x84, 0xC2, 0x49, 0xAB, 0xED, 0xCC, 0xCA, 0x9A, 0x95, 0x5C, 0x50, 0x36, 0x32, 0x85, 0x2C, 
            0x5F, 0x06, 0xCE, 0x33, 0x00, 0x26, 0xA8, 0x1E, 0x97, 0x50, 0x08, 0x9E, 0x71, 0x44, 0xAD, 0xB5, 
            0x52, 0x39, 0xA7, 0xB0, 0x62, 0x94, 0xFC, 0x3E, 0x75, 0xD9, 0x56, 0x02, 0xE9, 0x5A, 0x5F, 0x43, 
            0x4E, 0xC7, 0x94, 0xD9, 0x64, 0xCF, 0x1C, 0x3F, 0x0C, 0x6A, 0x78, 0x1B, 0xB4, 0xB3, 0x75, 0x7A, 
            0x3D, 0xB7, 0x3F, 0x65, 0xA5, 0x0C, 0x11, 0xAA, 0xEC, 0xC6, 0xB4, 0x96, 0x61, 0xFB, 0x38, 0x33, 
            0x4C, 0x0D, 0xD9, 0x37, 0x71, 0x2B, 0xC4, 0x46, 0x90, 0x65, 0xC3, 0x2A, 0x2E, 0xB3, 0x4E, 0x79, 
            0x00, 0x34, 0xC5, 0x09, 0x83, 0xD2, 0x00, 0x8E, 0x39, 0x56, 0x89, 0x70, 0x31, 0x7A, 0x9D, 0x37, 
            0x4D, 0x95, 0xA3, 0x93, 0x11, 0x3A, 0x9B, 0xF7, 0xA3, 0x3F, 0x34, 0xAA, 0x65, 0x55, 0x18, 0x01, 
            0xBB, 0xF9, 0x3E, 0xCF, 0x63, 0xA8, 0x32, 0x7D, 0x3F, 0xC1, 0x3A, 0x1A, 0x60, 0xB6, 0xD0, 0x69, 
            0xCD, 0x12, 0xCA, 0x65, 0x3C, 0x2E, 0xFC, 0x05, 0xA5, 0x5A, 0xD8, 0xA2, 0x2B, 0x2A, 0xC4, 0xE5, 
            0x2A, 0x9A, 0x96, 0x7A, 0x8F, 0xC2, 0x80, 0x57, 0x42, 0xAC, 0xBC, 0x6C, 0xE1, 0x30, 0x3C, 0x14
        };
    }
}