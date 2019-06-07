Module QueueStackSort

    'An assignment during college was to write our own unique sorts,
    'having already learned Selection, Insertion, Merge, Radix, Bubble,
    'making something properly unique was difficult. This was my solution.
    'It's a sort that does logically identical actions to lists of stacks
    'and queues until input is sorted, using the contrast between FIFO 
    'and FILO to sort the input.

    'Code has undergone a minor refactor for clarity, but given the 
    'difference in data types some code couldn't be made generic.

    'I was having problems with generics and offloading functionality 
    'to methods during the refactor, due to inexperience with VB, 
    'so this method is by necessity long and contains repeticious code.
    'In Java I'd probably be able write this code far better.
    'I could write it a lot better using List(Of List)s and simulating 
    'Queues/Stacks, but that would forsake the Sort's namesake.

    Sub Main()
        Dim rand As Random = New Random()

        Dim Array(10) As Integer
        Dim Queuer As List(Of Queue(Of Integer)) = New List(Of Queue(Of Integer))
        Dim Stacker As List(Of Stack(Of Integer)) = New List(Of Stack(Of Integer))

        For i = 0 To Array.Count - 1
            Array(i) = rand.Next(1, 100)
        Next

        PrintArray("Initial", Array)

        Console.WriteLine(vbCrLf & vbCrLf & "Feed into Queue: ")

        'Initalisation stage used to setup the sort, either the 
        'queuer or stacker needs to be set up for the sort to function.
        Queuer.Add(New Queue(Of Integer))
        Queuer.Last.Enqueue(Array(0))

        For Position = 1 To Array.Count - 1
            If Array(Position) < Queuer.Last.Last Then
                Queuer.Add(New Queue(Of Integer))
            End If
            Queuer.Last.Enqueue(Array(Position))
        Next

        'The "Sorted" stage of the sort is when the queuer only
        'has a single queue in it, which will be a sorted list.
        Do Until Queuer.Count = 1
            PrintQueuerState(Queuer)

            'The stack list is currently empty, to swap over all
            'elements from the queue list it requires at least one
            'stack to exist.
            Stacker.Add(New Stack(Of Integer))
            Stacker.Last.Push(Queuer.First.Dequeue())
            If Queuer.First.Count = 0 Then
                Queuer.Remove(Queuer.First)
            End If

            Do
                'If the top of the last stack is smaller than the top
                'element of the first queue, create a new stack.
                'Stacks are ordered in desecending order, 3,2,1
                'with 1 being the top of the stack.
                If Queuer.Item(0).Peek() > Stacker.Last.Peek() Then
                    Stacker.Add(New Stack(Of Integer))
                End If
                Stacker.Last.Push(Queuer.Item(0).Dequeue())
                If Queuer.Item(0).Count = 0 Then
                    Queuer.Remove(Queuer.Item(0))
                End If
            Loop Until Queuer.Count = 0

            PrintStackerState(Stacker)

            'Same code as before but for queues, even the conditional
            'which adds queues only happens when the top of the stack
            'is smaller than the front of the queue.
            Queuer.Add(New Queue(Of Integer))
            Queuer.Last.Enqueue(Stacker.First.Pop())
            If Stacker.First.Count = 0 Then
                Stacker.Remove(Stacker.First)
            End If

            Do
                If Stacker.Item(0).Peek() < Queuer.Last.Peek() Then
                    Queuer.Add(New Queue(Of Integer))
                End If
                Queuer.Last.Enqueue(Stacker.Item(0).Pop())
                If Stacker.Item(0).Count = 0 Then
                    Stacker.Remove(Stacker.Item(0))
                End If
            Loop Until Stacker.Count = 0

        Loop

        For i = 0 To Array.Count - 1
            Array(i) = Queuer.Last.Dequeue
        Next

        Console.WriteLine()
        PrintArray("Final", Array)

        Console.ReadLine()
    End Sub

    Sub PrintArray(ByVal preface, ByRef Array())
        Console.WriteLine(preface + " array: ")
        For i = 0 To Array.Count - 1
            Console.Write(Array(i) & ", ")
        Next
    End Sub

    Sub PrintStackerState(ByRef Stacker)
        Console.WriteLine(vbCrLf & "Stacker")
        For Each elem As Stack(Of Integer) In Stacker
            For Each numb As Integer In elem
                Console.Write(numb & ", ")
            Next
            Console.WriteLine()
        Next
    End Sub

    Sub PrintQueuerState(ByRef Queuer)
        Console.WriteLine(vbCrLf & "Queuer")
        For Each elem As Queue(Of Integer) In Queuer
            For Each numb As Integer In elem
                Console.Write(numb & ", ")
            Next
            Console.WriteLine()
        Next
    End Sub

End Module
