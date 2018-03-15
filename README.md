# random-sequence
## Generating an IEnumerable&lt;int> of Infinitely Many Random Numbers

The goal of the provided classes is to generate an infinite sequence of random integers from a given range. The sequence must be isolated, i.e. independent from any global state and from any other sequence in use at the same time. Therefore, the consumer must be safe to instantiate and consume the sequence of random numbers within own thread, without the fear that any other piece of code executing concurrently might interfere with the results it observes.

These are the classes which are supporting generation of random numbers in an infinite sequence:
- **RandomByteSequence** - Implements *IEnumerable<byte>* and generates an infinite stream of uniformly distributed byte values.
- **RandomBitSequence** - Internal class which implements *IEnumerable<uint>* and generates an infinite stream of bits, grouped into chunks of up to 32 bits. Each chunk is represented by a single *uint* value in the resulting sequence.
- **RandomNumbersSequence** - Implements *IEnumerable<int>* and generates an infinite stream of uniformly distributed *int* values. Each value belongs to the specified range, where range is initialized through the sequence's constructor and remains the same for all numbers in the sequence.

## Usage ##

Common use of the library is through the *RandomNumbersSequence*. Sequence is initialized via a public constructor by giving the lower and upper (inclusive) boundaries of the desired range of numbers. That will instantiate the sequence which you can then consume as a common *IEnumerable<int>* sequence:

```
IEnumerable<int> values = new RandomNumbersSequence(-14, 17).Take(1000);
```
