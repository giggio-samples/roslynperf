# Roslyn Perf

Just a quick check on what is the best way to cast in Roslyn.

## Results

````
IsKind Match: 42.21
IsKind No Match: 42.50
RawKind.Equals: 44.79
is Class: 29.53
is Struct (no match): 30.72
is interface (no match): 40.39
as Class: 25.92
as Struct (no match): 26.47
as interface (no match): 39.91
if then direct cast: 60.57
````

See `program.cs` to understand what each run does.

## Contact

* [Giovanni Bassi](http://blog.lambda3.com.br/L3/giovannibassi/), aka Giggio, [Lambda3](http://www.lambda3.com.br), [@giovannibassi](https://twitter.com/giovannibassi)

## License

This software is open source, licensed under the Apache License, Version 2.0.
See [LICENSE.txt](https://github.com/giggio/roslynperf/blob/master/LICENSE.txt) for details.
Check out the terms of the license before you contribute, fork, copy or do anything
with the code. If you decide to contribute you agree to grant copyright of all your contribution to this project, and agree to
mention clearly if do not agree to these terms. Your work will be licensed with the project at Apache V2, along the rest of the code.