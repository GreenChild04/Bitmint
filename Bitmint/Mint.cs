using encryption;

namespace bitmint
{
    public class Mint {
        public byte[] publicKey;
        public byte[] privateKey;
        public byte[] defaultOwner;
        public long idx = -1;

        public Mint(Wallet defaultOwner) {
            Asymmetric.generateKeys(out this.publicKey, out this.privateKey);
            this.defaultOwner = defaultOwner.publicKey;
        }

        public Coin mint() {
            this.idx++;
            return new Coin(this, this.getID());
        }

        public string getID() => $"GreenChild's Bitmint Coin #{Convert.ToBase64String(BitConverter.GetBytes(this.idx))}";
    }
}