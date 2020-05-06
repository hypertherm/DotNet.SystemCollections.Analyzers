# Quality (SCA1000-)

Rule ID | Title | Enabled | Severity | CodeFix | Description |
--------|-------|---------|----------|---------|----|
SCA1000 | Do not create fields that return arrays | True | Warning | False | This is when a field has a type of an Array instead of an IReadOnlyList.
SCA1001 | Do not return arrays | True | Warning | False | This is when an array is returned by a method instead of an IReadOnlyList.
SCA1002 | Do not create properties that return arrays | True | Warning | False | This is when a property has a type of an Array instead of an IReadOnlyList.
SCA1003 | Do not use ToArray on a non-generic collection.| True | Warning | False | This is used to indicate that a non-Array IEnumerable is calling the ToArray() method; this should be avoided for lazily constructed collections or enumerations.
SCA1004 | Do not use ToList on a non-generic collection.| True | Warning | False | This is used to indicate that a non-List IEnumerable is calling the ToList() method; this should be avoided for lazily constructed collections or enumerations.
SCA1005 | Do not create non-generic collection fields. | True | Warning | False | This is when a field is an old-style collection such as the ArrayList.
SCA1006 | Do not create methods that return non-generic collection types | True | Warning | False | This is when a method return type is an old-style collection such as the ArrayList.
SCA1007 | Do not create non-generic collection properties.| True | Warning | False | This is when a property is an old-style collection such as the ArrayList.
