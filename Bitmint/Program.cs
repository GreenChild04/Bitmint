using System.Diagnostics;
using bitmint;
using cstore;

Stopwatch stopwatch = new Stopwatch();
System.Console.WriteLine("Program Started...");
stopwatch.Start();

//===

Wallet wallet = new Wallet("wallet.btmt", "dave");
Mint mint = new Mint(wallet);
CStore.store("mint", mint);
new Payment(mint, "payment.btmt", 100);

//===

stopwatch.Stop();
System.Console.WriteLine($"Program ended at {stopwatch.Elapsed}ms");