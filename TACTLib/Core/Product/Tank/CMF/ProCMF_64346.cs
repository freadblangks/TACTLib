using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_64346 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];
            uint kidx = Keytable[(uint) header.m_dataCount & 511];
            for (uint i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx -= header.m_buildVersion & 511;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];
            uint okidx;
            uint kidx = okidx = Keytable[(uint) header.m_dataCount & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += okidx % 13;
                buffer[i] ^= digest[SignedMod(kidx + header.GetNonEncryptedMagic(), SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x5C, 0x47, 0xC8, 0x5A, 0x8A, 0x36, 0x48, 0xAE, 0xDC, 0x79, 0xA7, 0xC0, 0xDD, 0xD1, 0xF0, 0x3E,
            0xED, 0x54, 0x09, 0xE7, 0xAC, 0xAE, 0xE4, 0x36, 0x07, 0xDE, 0x41, 0xEB, 0x76, 0x4E, 0x43, 0x3B,
            0xE8, 0xB7, 0x37, 0x12, 0x69, 0x68, 0x81, 0x02, 0x88, 0x30, 0xCB, 0xFC, 0xB5, 0x5E, 0x5F, 0xA1,
            0x2F, 0x17, 0x4C, 0x48, 0xB8, 0x05, 0xDF, 0x34, 0xE3, 0x2A, 0x97, 0x37, 0x97, 0x41, 0xD0, 0x0B,
            0x7E, 0xE2, 0xA8, 0x30, 0x23, 0xF6, 0x6B, 0x31, 0x4E, 0xDB, 0x0A, 0xC7, 0xBC, 0xA8, 0xC8, 0x5F,
            0x49, 0x06, 0xAE, 0x73, 0x8C, 0xA8, 0xB3, 0x3E, 0x02, 0xA5, 0x6E, 0x55, 0x4C, 0x9C, 0xD2, 0xF9,
            0x04, 0x3F, 0xD2, 0x66, 0x65, 0x26, 0xBF, 0x90, 0xB9, 0x89, 0xAC, 0xE9, 0xED, 0x4A, 0x2D, 0x69,
            0xDD, 0x16, 0x96, 0x5F, 0x01, 0x22, 0x3F, 0x25, 0x6B, 0x16, 0xD6, 0x32, 0xE7, 0x77, 0x95, 0xA4,
            0xAB, 0xDE, 0x89, 0xDA, 0x5B, 0xB4, 0xFA, 0x3E, 0x46, 0x2D, 0x98, 0xE3, 0x80, 0x25, 0x11, 0xDE,
            0xA8, 0x6B, 0x0F, 0x9C, 0xA0, 0x71, 0x7A, 0x4A, 0x37, 0x97, 0x2F, 0xD0, 0xBF, 0x84, 0x35, 0xFD,
            0x06, 0xE9, 0x5C, 0x1A, 0x20, 0x09, 0x7B, 0x36, 0x4B, 0x35, 0x4E, 0x9B, 0x08, 0x24, 0xC1, 0x35,
            0xD4, 0xA0, 0xA6, 0xD7, 0xBA, 0x59, 0xF7, 0x5C, 0xB5, 0xBD, 0x61, 0x33, 0x55, 0xF0, 0x12, 0x70,
            0x7B, 0x35, 0x74, 0xC5, 0x72, 0x91, 0x42, 0xB4, 0xBD, 0xAA, 0x58, 0xD2, 0xF3, 0x5C, 0x4B, 0xC4,
            0xD0, 0xFF, 0xA5, 0xD9, 0x3A, 0xD0, 0x68, 0x94, 0x3C, 0x4E, 0x8E, 0xC2, 0xB7, 0x2D, 0x19, 0x04,
            0x69, 0x6A, 0xAA, 0x52, 0x60, 0xD6, 0x3B, 0xE6, 0xDF, 0x0C, 0x88, 0xF8, 0x76, 0x4A, 0xE8, 0x0A,
            0x7C, 0x35, 0x35, 0x13, 0x5B, 0xA7, 0x73, 0x05, 0xA2, 0x28, 0x69, 0x6D, 0x4A, 0xCD, 0x3A, 0x7E,
            0x7B, 0x02, 0xCE, 0xF5, 0x60, 0x49, 0x55, 0x82, 0x1B, 0xFC, 0xCE, 0xB7, 0xD1, 0x73, 0xC7, 0x98,
            0xAA, 0x2A, 0xAE, 0xF3, 0xF3, 0x77, 0x76, 0x82, 0xD7, 0xAB, 0xBC, 0x96, 0x98, 0xA1, 0x2C, 0x64,
            0x82, 0x97, 0x79, 0x27, 0x1B, 0x05, 0x54, 0x0E, 0x39, 0xF2, 0xEB, 0x9C, 0x40, 0xC5, 0x41, 0x3A,
            0xC5, 0x17, 0xB5, 0xC6, 0x83, 0x55, 0x9E, 0x49, 0xF3, 0xEF, 0x3E, 0x5B, 0x1A, 0xE3, 0x8E, 0xDB,
            0x73, 0x98, 0x9C, 0xEA, 0xDE, 0x3A, 0x6A, 0xEC, 0xF0, 0x22, 0xDC, 0x20, 0x8E, 0x54, 0x10, 0xE6,
            0x14, 0x87, 0x91, 0xD9, 0x1B, 0xEB, 0x96, 0x36, 0xBD, 0xD3, 0xE9, 0x87, 0xD7, 0x9D, 0x94, 0x45,
            0x3D, 0xA2, 0x06, 0xD4, 0xD5, 0x60, 0x1B, 0xFA, 0x9E, 0xDD, 0xD4, 0x9F, 0x30, 0x27, 0x46, 0x34,
            0x9B, 0x1C, 0x33, 0x85, 0x35, 0x17, 0xDF, 0x8D, 0xF4, 0x87, 0xF6, 0x95, 0x6F, 0x98, 0xAD, 0xB1,
            0x37, 0x07, 0x97, 0xCC, 0xF2, 0xCF, 0x47, 0xC1, 0xBD, 0xAE, 0x78, 0x33, 0x71, 0x7B, 0x25, 0x6C,
            0x38, 0x9D, 0x7A, 0x65, 0xEE, 0xA1, 0x2D, 0x50, 0xC6, 0x82, 0xF3, 0xC4, 0x6E, 0x9F, 0x36, 0xBE,
            0x34, 0xA9, 0xEC, 0x43, 0x04, 0xC6, 0xA0, 0x15, 0x2D, 0x68, 0x79, 0xAD, 0x55, 0x22, 0x84, 0xD1,
            0x3E, 0xA3, 0x15, 0xFD, 0x35, 0xF9, 0xBF, 0x19, 0xE2, 0xFB, 0x6B, 0x95, 0x11, 0x0A, 0x68, 0xA8,
            0xBA, 0x42, 0xF6, 0xC1, 0x5D, 0x5E, 0x76, 0x20, 0x05, 0xC4, 0x86, 0x11, 0x3E, 0xBC, 0x22, 0xAC,
            0x44, 0x33, 0x30, 0x82, 0xB1, 0xC1, 0xA1, 0xC1, 0x8F, 0xB9, 0x38, 0x82, 0x78, 0x66, 0x10, 0x17,
            0x69, 0x4B, 0xBB, 0x9B, 0x60, 0x0F, 0x06, 0xF4, 0x3F, 0xC0, 0x56, 0x52, 0xC5, 0xE9, 0x3E, 0x6D,
            0xFC, 0x64, 0x24, 0x09, 0x8F, 0xC1, 0xB9, 0x24, 0x1D, 0x3C, 0x2A, 0x25, 0x17, 0x04, 0x3A, 0xAE
        };
    }
}
