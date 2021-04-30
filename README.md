# HartmannPractical

# Added file
### ByteSerializer.cs
IByteSerializer implementation

# Updated code
### 1. IByteSerializer.cs
```
byte[] Serialize<T>(T input) 
```
```
byte[] Serialize<T>(T input) where T : SampleData; 
```
Since the input type **<T>** might has properites that I didn't implement for serialization.
The type that implements serialization are **int**, **string**, **SampleFlags**, and **DateTime**

### 2. Program.cs
```
IByteSerializer serializer = new ByteSerializer(); 
IPracticalTest test = new PracticalTest(serializer);
```
```
IPracticalTest test = serviceProvider.GetRequiredService<PracticalTest>();
```

Add dependency injection
