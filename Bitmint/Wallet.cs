using encryption;
using cstore;

namespace bitmint
{
    public class Wallet {
        public struct MetaData {
            public byte[] publicKey;
            public byte[][] hash;
            public byte[] e_privateKey;
        }

        public static byte[] privateKey = new byte[0];
        private MetaData metadata;
        public CSStream stream;
        public byte[] publicKey {get => this.metadata.publicKey;}
        private byte[][] hash {get => this.metadata.hash;}
        private byte[] e_privateKey {get => this.metadata.e_privateKey;}

        public Wallet(string filename, string password) {
            this.metadata = new MetaData();
            Asymmetric.generateKeys(out this.metadata.publicKey, out privateKey);
            this.metadata.hash = PassHashing.hash(password);
            this.metadata.e_privateKey = Symmetric.encrypt(privateKey, password);
            this.stream = CSStream.init(filename);
            this.stream.write(this.metadata);
        }

        public Wallet(string filename, string password, Func<bool, Wallet> retry) {
            this.stream = new CSStream(filename);
            this.stream.readObj<MetaData>(out this.metadata);
            this.stream.writeCache();
            if (!PassHashing.hash(password, this.hash[1])[0].SequenceEqual(this.hash[0])) {
                this.stream.close();
                retry(true);
            } Wallet.privateKey = Symmetric.decrypt(this.e_privateKey, password);
        }

        public void close() => this.stream.close();
        ~Wallet() => this.stream.close();
    }
}