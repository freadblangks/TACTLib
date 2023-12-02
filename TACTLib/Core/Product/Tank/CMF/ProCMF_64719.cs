using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_64719 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];
            uint kidx;
            kidx = Keytable[length + 256];
            for (uint i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += (uint) (header.m_buildVersion * header.m_dataCount) % 7;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];
            uint okidx;
            uint kidx = okidx = Keytable[(digest[7] * Keytable[0]) & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += (digest[6] & 1) != 0 ? 37 : okidx % 61;
                buffer[i] ^= digest[SignedMod(kidx - i, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0xD0, 0x97, 0x48, 0x18, 0x80, 0x1F, 0x30, 0xCB, 0xBE, 0x2F, 0xF1, 0x9D, 0xD6, 0xAE, 0xC1, 0x22,
            0x4F, 0x34, 0x60, 0x3F, 0xFD, 0xB9, 0x41, 0x4A, 0xBE, 0x40, 0x6F, 0xE7, 0xCC, 0x3B, 0x13, 0x05,
            0x0E, 0x17, 0x2C, 0xF0, 0x36, 0x4B, 0x0F, 0x78, 0x91, 0xB8, 0xBD, 0x6C, 0x87, 0x70, 0xB8, 0xE5,
            0x6D, 0x07, 0xA5, 0xC8, 0x76, 0xD8, 0xAD, 0xC0, 0x2F, 0x77, 0x2B, 0x1A, 0x7C, 0x33, 0x26, 0x46,
            0x70, 0xD1, 0x01, 0x0D, 0x20, 0x56, 0x2A, 0x74, 0x28, 0x16, 0x6D, 0xFF, 0xA5, 0xC0, 0x53, 0x71,
            0x6C, 0x92, 0x3E, 0xBD, 0xBF, 0x9D, 0x95, 0x67, 0x20, 0x13, 0x05, 0xA8, 0x21, 0x2F, 0x5D, 0x48,
            0x79, 0xC8, 0x1D, 0x96, 0x36, 0x21, 0x27, 0xC2, 0x94, 0x9C, 0xC8, 0x2D, 0x3E, 0x06, 0x6D, 0x1F,
            0xAE, 0x61, 0x26, 0x91, 0x21, 0xA8, 0x0D, 0x92, 0x9F, 0xE5, 0x45, 0x9C, 0x0E, 0x41, 0xF0, 0x41,
            0xC6, 0xB8, 0x7E, 0x9B, 0xDC, 0xE6, 0xED, 0xED, 0x15, 0xE6, 0x6E, 0x9D, 0xEC, 0x39, 0xFE, 0x7C,
            0x4C, 0x68, 0x1D, 0x32, 0x89, 0xBD, 0x27, 0xA3, 0x05, 0xF5, 0x7B, 0x8E, 0xB0, 0xBD, 0xC4, 0xF1,
            0xD8, 0xF1, 0xC2, 0x2C, 0xAC, 0x52, 0xFC, 0x07, 0xE4, 0x0A, 0x88, 0x35, 0x0B, 0x39, 0x13, 0xAD,
            0x34, 0x4C, 0xB9, 0xDF, 0xF3, 0x3B, 0x36, 0x12, 0xC0, 0xCC, 0x0F, 0x7B, 0x7D, 0x9D, 0xFB, 0xF4,
            0xC4, 0x78, 0x7B, 0x46, 0xE9, 0x26, 0x2E, 0x25, 0x4F, 0xAB, 0x63, 0xD2, 0x18, 0x9D, 0x2E, 0x51,
            0x94, 0xFD, 0xAE, 0x18, 0x8E, 0x5A, 0xC7, 0x08, 0xDA, 0xC2, 0x01, 0x04, 0xDC, 0xCF, 0xB3, 0x3E,
            0x7B, 0x6D, 0x95, 0x17, 0x78, 0x6E, 0x6C, 0x8F, 0x56, 0x38, 0xE2, 0x40, 0x3C, 0xB8, 0xE1, 0xE1,
            0x69, 0xF0, 0xF5, 0xC0, 0xB6, 0xB7, 0xDD, 0x42, 0x41, 0xD2, 0x4D, 0xD3, 0x2C, 0x11, 0x4F, 0xC0,
            0x9A, 0x9A, 0xA4, 0xD4, 0x3C, 0xB5, 0xCB, 0x97, 0x85, 0x33, 0x22, 0xE3, 0xEE, 0x23, 0xFA, 0xE7,
            0x04, 0x3C, 0x86, 0xE1, 0xD3, 0xA8, 0x84, 0x08, 0x68, 0x29, 0x19, 0x6D, 0x7C, 0xAD, 0x8F, 0xB8,
            0xE9, 0xA2, 0x6A, 0xD5, 0xE9, 0xD8, 0xCD, 0x03, 0x7C, 0x20, 0x29, 0xF2, 0xD7, 0x69, 0x4C, 0x79,
            0x26, 0x43, 0x2A, 0xAD, 0x2A, 0xFD, 0x62, 0xEF, 0x0B, 0xB9, 0xAC, 0xA7, 0x2A, 0x98, 0x7A, 0x6E,
            0x7E, 0x36, 0x5D, 0x03, 0x36, 0x8F, 0x6D, 0x02, 0xB5, 0xAE, 0xD8, 0xAA, 0xB0, 0x72, 0x43, 0x58,
            0xA6, 0x86, 0x8E, 0x0A, 0xEE, 0x0C, 0x6E, 0x15, 0xC9, 0x1E, 0x53, 0xF3, 0x5D, 0xA2, 0x02, 0xBD,
            0x82, 0x88, 0x22, 0xA6, 0x5E, 0xE2, 0x8D, 0x72, 0xF8, 0x93, 0xDF, 0xA8, 0xB8, 0x4F, 0x2D, 0xA9,
            0x68, 0x1B, 0x6D, 0x93, 0x9C, 0x2F, 0x06, 0xC5, 0xB4, 0x8C, 0xFB, 0xC3, 0x31, 0xD4, 0x06, 0x78,
            0xAD, 0x74, 0x78, 0xF3, 0xFF, 0x42, 0x2C, 0xC0, 0xC5, 0x28, 0x79, 0xB1, 0x3E, 0x5F, 0xFE, 0x03,
            0xF0, 0xD6, 0xF2, 0xC7, 0x28, 0x4E, 0xD1, 0xED, 0xD6, 0xE9, 0xBB, 0xAE, 0x15, 0xF4, 0x0E, 0x3C,
            0x59, 0x6C, 0x2C, 0x33, 0x9C, 0x71, 0xE9, 0x8A, 0x62, 0xDF, 0x12, 0x3A, 0x84, 0xFB, 0xC0, 0x34,
            0x7F, 0x05, 0x79, 0x94, 0xCD, 0xB8, 0x44, 0x6D, 0x17, 0xBC, 0x41, 0x54, 0xE4, 0x0B, 0xFD, 0xF8,
            0xFB, 0x98, 0x60, 0x21, 0x5B, 0x77, 0x88, 0x35, 0x7B, 0xEC, 0xB2, 0x85, 0x27, 0xCE, 0xDA, 0x09,
            0xA4, 0x8D, 0x61, 0x81, 0xBF, 0x4F, 0xEB, 0xD7, 0x49, 0xE0, 0xA2, 0x96, 0xCF, 0x40, 0x9C, 0x2B,
            0x44, 0x0C, 0x3D, 0xBA, 0x8A, 0xA2, 0xEC, 0xA7, 0xB1, 0x75, 0x80, 0x5E, 0xFB, 0xC7, 0x18, 0x41,
            0x9B, 0xB8, 0xB5, 0x3B, 0x36, 0x4C, 0xF8, 0xA7, 0xD2, 0x3D, 0x5C, 0x07, 0xF0, 0x05, 0xC8, 0x11
        };
    }
}
