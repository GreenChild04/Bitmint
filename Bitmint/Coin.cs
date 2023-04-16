namespace bitmint
{
    public class Coin {
        public string id;
        public byte[] contents; // Contents of the coin for validation

        public Coin(Mint mint, string id) {
            this.id = id;
            byte[] signed = encryption.Asymmetric.sign(System.Text.Encoding.UTF8.GetBytes(id), mint.privateKey);
            this.contents = encryption.Asymmetric.encrypt(signed, mint.defaultOwner);
        }
    }
}