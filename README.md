# AlpineGPGSharp
Automated encrypt and decrypt using GPG from Alpine mail client.

# Configuration

Set GPGPath to the full (non-escaped) path of gpg2.exe.

# Configure Alpine

In pinerc refer to the following sample:

display-filters=_LEADING("-----BEGIN PGP")_ C:\Users\brooks\Documents\Code\alpine\AlpineGPGSharp-Decrypt.exe
