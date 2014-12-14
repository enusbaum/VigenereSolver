VigenereSolver
==============

A Vigenere Cipher Solver written in .NET

I had fun coding this and learning more about Vigenere Ciphers, hope someone else finds this as useful.

Cheers!

#Supported Attacks

##Kasiski / Babbage Attack
The implementation of the Kasiski/Babbage attack is a clone of the Python example written by Al Sweigart.
You can find the original Python source code here: http://inventwithpython.com/hacking/chapter19.html

Individual Kasiski analysis operations are implemented in VigenereSolver.Library.Analysis.Kasiski

##Known Plaintext Attack
I've implemented a simple Knwon Plaintext attack which only supports Vigenere ciphers with spacing to denote individual words.