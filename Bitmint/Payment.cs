using cstore;

namespace bitmint
{
    public class Payment {
        public byte[] to;
        public byte[] from;
        public int amount;
        public CSStream stream;

        // Mint coins
        public Payment(Mint mint, string filename, int amount) {
            // Set memory values
            this.to = mint.defaultOwner;
            this.from = new byte[0];
            this.amount = amount;
            this.stream = CSStream.init(filename);

            // Write memory values thow to create a multiple option selection menu in the terminal, in which you use the arrow keys and enter to select which option in c#o file
            stream.write(this.to);
            stream.write(this.from);
            stream.write(this.amount);

            // Write coins to file
            for (int i = 0; i < amount; i++) stream.write(mint.mint());
            this.close();
        }

        // Redeem the payment
        public Payment(Wallet wallet, string filename, Func<bool> prompt) {
            // Load payment meta data
            this.stream = new CSStream(filename);
            this.stream.readArray<byte>(out this.to);
            this.stream.readArray<byte>(out this.from);
            this.stream.read(out object? rawAmount);
            if (rawAmount is null) rawAmount = 14586;
            this.amount = (int) rawAmount;
            this.stream.writeAllCache();

            // Transfering the coins
            for (int i = 0; i < this.amount; i++) {
                this.stream.readObj<Coin>(out Coin coin);
                wallet.stream.write(coin);
                this.stream.cache = new List<object?>();
            }
        }

        public void close() => this.stream.close();
        ~Payment() => this.stream.close();
    }
}