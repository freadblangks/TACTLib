using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_62065 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];
            uint kidx = Keytable[header.m_buildVersion & 511];
            for (uint i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.m_buildVersion - kidx;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];
            uint kidx = Keytable[header.m_buildVersion & 511];
            
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.m_buildVersion - kidx;
                buffer[i] ^= digest[SignedMod(kidx + i, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x6F, 0x7F, 0xDF, 0xF0, 0x90, 0x83, 0x31, 0x29, 0xDF, 0x8B, 0x67, 0x77, 0xE1, 0x2B, 0xFE, 0xDF,
            0x13, 0xFF, 0x06, 0x52, 0xBC, 0x43, 0x61, 0x26, 0xB1, 0xC0, 0x5B, 0x0D, 0xD0, 0x7A, 0x09, 0x01,
            0x66, 0xB5, 0x22, 0xF0, 0x9D, 0x2B, 0x6B, 0x87, 0xF7, 0x56, 0x5F, 0x6E, 0x3B, 0xBB, 0x59, 0x87,
            0x04, 0xEA, 0x10, 0xE8, 0x90, 0x30, 0x36, 0xDF, 0xB4, 0x47, 0x98, 0x19, 0xE1, 0x12, 0x5D, 0x54,
            0x62, 0xE2, 0x52, 0x3B, 0xC8, 0x7D, 0x1E, 0x58, 0x0E, 0xC7, 0x5C, 0xE2, 0xF3, 0x19, 0x92, 0xFB,
            0x86, 0xD7, 0xFB, 0xF1, 0xA6, 0x42, 0x7B, 0xD0, 0x98, 0x02, 0xDB, 0xC4, 0x88, 0x36, 0xD3, 0x89,
            0x96, 0xE6, 0x40, 0x46, 0x16, 0x64, 0x98, 0xF2, 0x5E, 0xE8, 0x60, 0x55, 0x76, 0x90, 0x6D, 0xF5,
            0x9B, 0x6B, 0xD3, 0xA5, 0x80, 0x59, 0x08, 0x76, 0x5D, 0x59, 0xCA, 0xC7, 0x70, 0x9E, 0xDB, 0x8C,
            0x0A, 0x31, 0x75, 0x97, 0x81, 0x3A, 0xAF, 0x74, 0x42, 0x2E, 0xD8, 0x11, 0x4C, 0xC9, 0xA1, 0xC6,
            0x1A, 0x1C, 0x40, 0xDB, 0xA9, 0xA5, 0xA8, 0x9D, 0xF2, 0xCB, 0x9E, 0x79, 0xAD, 0x82, 0x3B, 0x46,
            0xF6, 0xD2, 0x34, 0xC0, 0x3B, 0x14, 0x7D, 0xAD, 0x2D, 0x52, 0x31, 0x1B, 0xCC, 0x0A, 0xE1, 0x23,
            0xD3, 0xBE, 0x3B, 0x6D, 0x1A, 0xE4, 0x03, 0xDE, 0x8C, 0xDA, 0x13, 0x56, 0xF6, 0x89, 0x70, 0x6D,
            0x1E, 0xA6, 0xC8, 0xA0, 0xF0, 0xBE, 0x59, 0xA7, 0xEB, 0x27, 0x0C, 0x12, 0x28, 0xBF, 0xE2, 0x96,
            0x23, 0x07, 0x50, 0xCB, 0xFF, 0x5F, 0x69, 0x4D, 0xA7, 0x3C, 0x86, 0x54, 0x41, 0x06, 0x7E, 0x0C,
            0x92, 0xA4, 0x71, 0xAA, 0x13, 0xB4, 0x0B, 0x2D, 0x0B, 0xC2, 0xA4, 0x83, 0x55, 0x92, 0x1F, 0x44,
            0x5D, 0xBD, 0x82, 0x6E, 0x4E, 0xB6, 0xE7, 0xED, 0x09, 0x8C, 0x19, 0x6A, 0xAD, 0x70, 0x51, 0x29,
            0x44, 0xB6, 0xF8, 0xBC, 0x08, 0x33, 0x95, 0x84, 0xB5, 0x19, 0x44, 0xB6, 0x52, 0x40, 0xA4, 0xC9,
            0x8B, 0xB7, 0xF8, 0x72, 0x9A, 0x71, 0xB1, 0x0A, 0xB0, 0xB5, 0x25, 0x7E, 0x4B, 0xFD, 0x28, 0x0C,
            0xA4, 0xA7, 0xE8, 0x77, 0x57, 0x22, 0xF2, 0xFA, 0x8E, 0x38, 0x0E, 0xA7, 0xDF, 0x52, 0xA0, 0x64,
            0xAA, 0xCC, 0xFD, 0xCB, 0x31, 0x13, 0x5B, 0x51, 0xAD, 0xCB, 0x23, 0x2B, 0xBC, 0xBF, 0x5B, 0x35,
            0xE8, 0x25, 0xE6, 0x3F, 0x82, 0x0A, 0xC2, 0x6A, 0x84, 0x68, 0xC3, 0xC4, 0xFC, 0x46, 0x2E, 0xA3,
            0x8B, 0x92, 0x1C, 0xAF, 0x5C, 0xCC, 0x7B, 0x21, 0x20, 0xF8, 0x4F, 0x75, 0x27, 0xBB, 0xC8, 0x1B,
            0xE4, 0x79, 0x87, 0xF4, 0x6E, 0x00, 0xC4, 0xE9, 0x4E, 0xC3, 0x5A, 0x9D, 0x56, 0x10, 0x8D, 0xD6,
            0x24, 0xFC, 0xDE, 0x3C, 0x23, 0x74, 0x1F, 0xD9, 0x6C, 0x92, 0xBE, 0x45, 0x22, 0x85, 0xE1, 0xF7,
            0xCC, 0x36, 0x10, 0x68, 0xA1, 0x33, 0x2E, 0x7E, 0xD0, 0xBE, 0xA5, 0xE7, 0x40, 0x33, 0xAF, 0xB4,
            0x97, 0xA6, 0xB8, 0x58, 0xB4, 0x01, 0x82, 0x21, 0x90, 0x62, 0x33, 0x8F, 0x57, 0x29, 0xA7, 0x77,
            0x1F, 0x5D, 0xAB, 0xC6, 0xE1, 0x57, 0xD9, 0x11, 0x08, 0x52, 0x4F, 0x93, 0x00, 0x34, 0x05, 0x05,
            0x55, 0x1A, 0x03, 0xE7, 0x19, 0xDC, 0x75, 0x71, 0x13, 0x8A, 0x54, 0x3C, 0x04, 0x32, 0x7E, 0x56,
            0x70, 0x38, 0xBE, 0x92, 0x24, 0xD0, 0x54, 0x14, 0x4F, 0x45, 0x71, 0xB3, 0xAE, 0xBE, 0x21, 0x6A,
            0xA2, 0x0E, 0xEB, 0xBD, 0x8E, 0xF9, 0x99, 0x83, 0xA8, 0x39, 0xDA, 0xB9, 0x26, 0x0B, 0x4E, 0x9F,
            0x89, 0x1B, 0x28, 0x77, 0x59, 0xD7, 0x80, 0x46, 0x82, 0xFB, 0xF0, 0x5D, 0x86, 0xA7, 0xE6, 0xF3,
            0xE4, 0x0F, 0x8F, 0x8A, 0x98, 0x48, 0x63, 0xD7, 0x46, 0x80, 0x0B, 0xD3, 0xB9, 0xDB, 0x9A, 0xD5
        };
    }
}