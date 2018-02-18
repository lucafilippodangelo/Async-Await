# Async-Await
Premise: It could be possible run all the methods parallelly by using the simple thread programming but it will block UI and wait to complete all the tasks.

**TEST 001** "compute-bound task .net 4"	
usage of tasks, cancellation token and event delegates 

**TEST 002**
usage of async await with nested thread creation, kick off a timer to make console prints in a main thread and in the mean time calls to async tasks
The execution sequence of the nested threads will be managed using await. Any task will await the nested one to be completed.     
Async and await are the code markers, which marks code positions from where the control should resume after a task completes.

**TEST 003** "compute-bound task .net 4.5"	 
further demo on the use of await and prior establishment with synchronous methods

Overview
https://docs.microsoft.com/en-us/dotnet/csharp/async
Good Lecture
https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
Details
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/